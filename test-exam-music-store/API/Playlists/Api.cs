using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace test_exam_music_store.API.Playlists;

[Route("api/playlists")]
[ApiController]
public partial class PlaylistsApi : ControllerBase
{
    public IMediator _mediator;

    public PlaylistsApi(
        IMediator mediator)
    {
        _mediator = mediator;
    }
}
