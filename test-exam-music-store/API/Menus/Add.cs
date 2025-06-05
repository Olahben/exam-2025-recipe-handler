using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeHandler.Client;
using RecipeHandler.Client.Exceptions;
using RecipeHandler.Infrastructure.Features.Menus;

namespace RecipeHandler.API.Menus;

public partial class MenusApi
{
    [HttpPost]
    [Route("add")]
    [ProducesResponseType(typeof(RecipeHandler.Client.AddMenuResponse), 200)]
    public async Task<IActionResult> Add(
        [FromBody] AddMenuRequest input)
    {
        IRequest<RecipeHandler.Client.AddMenuResponse> command = new Add.Command(
            Name: input.Name,
            Occasions: input.Occasions,
            RecipeIds: input.RecipeIds);

        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
