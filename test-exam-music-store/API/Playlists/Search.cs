using Microsoft.AspNetCore.Mvc;
using MusicStore.Client;
using MusicStore.Infrastructure.Features.Playlists;

namespace test_exam_music_store.API.Playlists;

public partial class PlaylistsApi
{
    /// <summary>
    /// Searches for playlists based on the specified criteria.
    /// </summary>
    /// <param name="PlaylistIds"></param>
    /// <param name="Names"></param>
    /// <param name="Occasions"></param>
    /// <param name="Skip"></param>
    /// <param name="Limit"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetAllPlaylistsResponse), 200)]
    public async Task<IActionResult> Search(
        [FromQuery] List<Guid>? PlaylistIds,
        [FromQuery] List<string>? Names,
        [FromQuery] List<string>? Occasions,
        [FromQuery] int Skip = 0,
        [FromQuery] int Limit = 100)
    {
        var query = new Search.Query(
            PlaylistIds: PlaylistIds,
            Names: Names,
            Occasions: Occasions);

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
