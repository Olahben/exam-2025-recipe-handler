using MediatR;
using MusicStore.Client;
using MusicStore.Infrastructure.MongoDbPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Client.Exceptions;

namespace MusicStore.Infrastructure.Features.Playlists;

public static class Add
{
    public record Command(string Name, List<Guid> SongIds, List<string> Occasions) : IRequest<AddPlaylistResponse>;
    public class Handler(
        PlaylistsRepository playlistsRepository,
        SongsRepository songsRepository,
        IMediator mediator) : IRequestHandler<Command, AddPlaylistResponse>
    {
        public async Task<AddPlaylistResponse> Handle(
            Command cmd,
            CancellationToken cancellationToken)
        {
            (bool isValid, string message) = await ValidateRequest(cmd, songsRepository, cancellationToken);

            if (!isValid)
            {
                // Throw custom exception
                throw new BadRequestException(message);
            }

            var playlistId = Guid.NewGuid();

            await playlistsRepository.Insert(
                playlistId: playlistId,
                name: cmd.Name,
                songIds: cmd.SongIds,
                occasions: cmd.Occasions,
                cancellationToken: cancellationToken);

            return new AddPlaylistResponse
            {
                PlaylistId = playlistId
            };
        }

        // TODO: Add validation like this: Every song must come from different genres, every song must have a common tag.
        public async static Task<(bool, string)> ValidateRequest(
            Command cmd,
            SongsRepository songsRepository,
            CancellationToken cancellationToken)
        {
            var (count, songs) = await songsRepository.GetAll(
                songIds: cmd.SongIds,
                cancellationToken: cancellationToken);
            
            // If there are no songs with the given IDs, the playlist cannot be created
            if (count == 0)
            {
                return (false, "Did not find any songs that matched these IDs");
            }

            // Checks if all songs comes from different genres by first projecting all the genres into a Enumerable, then returning the distinct values,
            // and if the distinct values doesnt match the count of the song IDs, it means that there are duplicates. Therefore we return not vlaid.
            if (songs
                .Select(x => x.Genre)
                .Distinct()
                .Count() != cmd.SongIds.Count)
            {
                return (false, "All songs must be from different genres");
            }

            // Projects all common tags into a Enumerable, then groups them by the tag name and counts how many times each tag appears.
            // If the count of the tag is equal to the count of the song IDs, it means that all songs have that tag.
            var commonTags = songs
                .SelectMany(x => x.Tags)
                .GroupBy(x => x)
                .Where(x => x.Count() == cmd.SongIds.Count)
                .Select(x => x.Key);

            if (!commonTags.Any())
            {
                return (false, "All songs must have a common tag");
            }

            // All tests passed, so we return valid
            return (true, string.Empty);
        }
    }
}
