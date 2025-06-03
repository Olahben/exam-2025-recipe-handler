using MediatR;
using MusicStore.Client.Exceptions;
using MusicStore.Client;
using MusicStore.Infrastructure.MongoDbPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Infrastructure.Features.Recipes;

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