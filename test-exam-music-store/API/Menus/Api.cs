using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeHandler.API.Menus;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/menus")]
[ApiController]
public partial class MenusApi : ControllerBase
{
    public IMediator _mediator;

    public MenusApi(
        IMediator mediator)
    {
        _mediator = mediator;
    }
}
