using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Client;
using MusicStore.Client.Exceptions;
using MusicStore.Infrastructure.Features.Playlists;

namespace test_exam_music_store.API.Playlists;

public partial class PlaylistsApi
{
    [HttpPost]
    [ProducesResponseType(typeof(AddPlaylistResponse), 200)]
    public async Task<IActionResult> Add([FromBody] AddPlaylistRequest input)
    {
        IRequest<AddPlaylistResponse> command = new Add.Command(
            Name: input.Name,
            SongIds: input.SongIds,
            Occasions: input.Occasions);

        try
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
