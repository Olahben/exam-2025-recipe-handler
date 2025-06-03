using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Client;

namespace RecipeHandler.API.Recipes;

public partial class RecipesApi
{
    /// <summary>
    /// Creates a new recipe with the specified name and ingredients.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AddRecipeResponse), 200)]
    public async Task<IActionResult> Add([FromBody] AddRecipeRequest input)
    {
        IRequest<AddRecipeResponse> command = new MusicStore.Infrastructure.Features.Recipes.Add.Command(
            Name: input.Name,
            Category: input.Category,
            TasteProfile: input.TasteProfile,
            Instructions: input.Instructions,
            PreparationTime: input.PreparationTime,
            Ingredients: input.Ingredients);

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}
