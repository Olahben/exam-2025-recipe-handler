using Microsoft.AspNetCore.Mvc;
using RecipeHandler.Client;
using RecipeHandler.Infrastructure.Features.Menus;

namespace RecipeHandler.API.Menus;

public partial class MenusApi
{
    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(RecipeHandler.Client.SearchMenusRespone), 200)]
    public async Task<IActionResult> Search(
        [FromQuery] List<Guid>? menuIds = null,
        [FromQuery] List<string>? occasions = null)
    {
        // Explicitly specify the correct type for the query
        var query = new Search.Query(
            MenuIds: menuIds,
            Occasions: occasions);

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
