using MediatR;
using MusicStore.Client.Songs;
using MusicStore.Infrastructure.MongoDbPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Infrastructure.Features.Songs;

public static class Search
{
    public record Query(
        List<Guid>? SongIds,
        List<string>? Genres,
        List<string>? Tags) : IRequest<GetAllResponse>;

    public class Handler(
        SongsRepository songsRepository,
        IMediator mediator) : IRequestHandler<Query, GetAllResponse>
    {
        public async Task<GetAllResponse> Handle(
            Query query,
            CancellationToken cancellationToken)
        {
            var (count, songs) = await songsRepository.GetAll(
                songIds: query.SongIds,
                genres: query.Genres,
                tags: query.Tags,
                cancellationToken: cancellationToken);

            return new GetAllResponse
            {
                Count = count,
                Songs = songs.Select(x => new SongResponse
                {
                    SongId = x.SongId,
                    Genre = x.Genre,
                    Tags = x.Tags,
                    Created = x.Created,
                    ModifiedAt = x.ModifiedAt
                }).ToList()
            };
        }
    }
}