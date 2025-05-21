using MediatR;
using MusicStore.Client.Songs;
using MusicStore.Infrastructure.MongoDbPersistence;

namespace MusicStore.Infrastructure.Features.Songs;

public static class Add
{
    public record Command(string Genre, List<string> Tags) : IRequest<AddSongResponse>;

    // TODO: Implement logger
    public class Handler(
        SongsRepository songsRepository,
        IMediator mediator) : IRequestHandler<Command, AddSongResponse>
    {
        public async Task<AddSongResponse> Handle(
            Command cmd,
            CancellationToken cancellationToken)
        {
            var songId = Guid.NewGuid();

            await songsRepository.Insert(
                songId: songId,
                genre: cmd.Genre,
                tags: cmd.Tags,
                cancellationToken: cancellationToken);

            return new AddSongResponse
            {
                SongId = songId
            };
        }
    }
}
