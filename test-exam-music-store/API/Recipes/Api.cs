namespace RecipeHandler.API.Recipes;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/recipes")]
[ApiController]
public partial class RecipesApi : ControllerBase
{
    public IMediator _mediator;

    public RecipesApi(
        IMediator mediator)
    {
        _mediator = mediator;
    }
}

