using MediatR;
using RecipeHandler.Client.Exceptions;
using RecipeHandler.Client;
using RecipeHandler.Infrastructure.MongoDbPersistence;

namespace RecipeHandler.Infrastructure.Features.Recipes;

public static class Add
{
    public record Command(
        string Name,
        string Category,
        List<string> TasteProfile,
        List<string> Ingredients,
        List<string> Instructions,
        TimeOnly PreparationTime) : IRequest<AddRecipeResponse>;
    public class Handler(
        RecipesRepository recipesRepository,
        IMediator mediator) : IRequestHandler<Command, AddRecipeResponse>
    {
        public async Task<AddRecipeResponse> Handle(
            Command cmd,
            CancellationToken cancellationToken)
        {
            var recipeId = Guid.NewGuid();

            await recipesRepository.Insert(
                recipeId: recipeId,
                name: cmd.Name,
                category: cmd.Category,
                tasteProfile: cmd.TasteProfile,
                ingredients: cmd.Ingredients,
                instructions: cmd.Instructions,
                preparationTime: cmd.PreparationTime,
                cancellationToken: cancellationToken);

            return new AddRecipeResponse
            {
                RecipeId = recipeId
            };
        }
    }
}