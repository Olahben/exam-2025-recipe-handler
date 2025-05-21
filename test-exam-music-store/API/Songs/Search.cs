using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Client.Songs;
using MusicStore.Infrastructure.Features.Songs;

namespace test_exam_music_store.API.Songs;

public partial class SongsApi
{
    [HttpGet]
    [ProducesResponseType(typeof(GetAllResponse), 200)]
    public async Task<IActionResult> Search(
        [FromQuery] List<Guid>? SongIds,
        [FromQuery] List<string>? Genres,
        [FromQuery] List<string>? Tags,
        [FromQuery] int Skip = 0,
        [FromQuery] int Limit = 100)
    {
        var query = new Search.Query(
            SongIds: SongIds,
            Genres: Genres,
            Tags: Tags);

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
