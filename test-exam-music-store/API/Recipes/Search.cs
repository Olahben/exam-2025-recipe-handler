using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeHandler.Client;

namespace RecipeHandler.API.Recipes;

public partial class RecipesApi
{
    /// <summary>
    /// Searches for recipes based on the specified search term.
    /// </summary>
    /// <param name="searchTerm">The term to search for in recipe names.</param>
    /// <returns>A list of recipes matching the search term.</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(SearchRecipesResponse), 200)]
    public async Task<IActionResult> Search(
        [FromQuery] List<Guid>? recipeIds,
        [FromQuery] List<string>? categories,
        [FromQuery] List<string>? tasteProfiles)
    {
        IRequest<SearchRecipesResponse> query = new RecipeHandler.Infrastructure.Features.Recipes.Search.Command(
            RecipeIds: recipeIds,
            Categories: categories,
            TasteProfiles: tasteProfiles);

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
