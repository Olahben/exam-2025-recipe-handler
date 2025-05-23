using MediatR;
using MusicStore.Client;
using MusicStore.Infrastructure.MongoDbPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Infrastructure.Features.Playlists;

public static class Search
{
    public record Query(List<Guid>? PlaylistIds, List<string>? Names, List<string>? Occasions) : IRequest<GetAllPlaylistsResponse>;

    public class Handler(
        PlaylistsRepository playlistsRepository,
        IMediator mediator) : IRequestHandler<Query, GetAllPlaylistsResponse>
    {
        public async Task<GetAllPlaylistsResponse> Handle(
            Query query,
            CancellationToken cancellationToken)
        {
            var (count, playlists) = await playlistsRepository.GetAll(
                playlistIds: query.PlaylistIds,
                names: query.Names,
                occasions: query.Occasions,
                cancellationToken: cancellationToken);

            return new GetAllPlaylistsResponse
            {
                Count = count,
                Playlists = playlists.Select(x => new PlaylistResponse
                {
                    PlaylistId = x.PlaylistId,
                    Name = x.Name,
                    SongIds = x.SongIds,
                    Occasions = x.Occasions,
                    Created = x.Created,
                    ModifiedAt = x.ModifiedAt
                }).ToList()
            };
        }
    }
}
