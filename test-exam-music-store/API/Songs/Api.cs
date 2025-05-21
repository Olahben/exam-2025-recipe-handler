namespace test_exam_music_store.API.Songs;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/songs")]
[ApiController]
public partial class SongsApi : ControllerBase
{
    public IMediator _mediator;

    public SongsApi(
        IMediator mediator)
    {
        _mediator = mediator;
    }
}
