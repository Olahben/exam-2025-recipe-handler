using Microsoft.AspNetCore.Mvc;
using MusicStore.Client.Songs;
using MediatR;
using MusicStore.Infrastructure.Features.Songs;

namespace test_exam_music_store.API.Songs;

public partial class SongsApi
{
    [HttpPost]
    [ProducesResponseType(typeof(AddSongResponse), 200)]
    public async Task<IActionResult> Add([FromBody] AddSongRequest input)
    {
        IRequest<AddSongResponse> command = new Add.Command(
            Genre: input.Genre,
            Tags: input.Tags);

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}
