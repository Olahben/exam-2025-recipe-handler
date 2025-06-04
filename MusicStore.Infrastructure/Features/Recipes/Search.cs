using MediatR;
using RecipeHandler.Client;
using RecipeHandler.Infrastructure.MongoDbPersistence;

namespace RecipeHandler.Infrastructure.Features.Recipes;

public static class Search
{
    public record Query(
        List<Guid>? RecipeIds = null,
        List<string>? Categories = null,
        List<string>? TasteProfiles = null) : IRequest<SearchRecipesResponse>;

    public class Handler(
        RecipesRepository recipesRepository) : IRequestHandler<Query, SearchRecipesResponse>
    {
        public async Task<SearchRecipesResponse> Handle(
            Query cmd,
            CancellationToken cancellationToken)
        {
            var (count, recipes) = await recipesRepository.GetAll(
                recipeIds: cmd.RecipeIds,
                categories: cmd.Categories,
                tasteProfiles: cmd.TasteProfiles,
                skip: 0,
                limit: 100,
                cancellationToken: cancellationToken);

            return new SearchRecipesResponse
            {
                Count = count,
                Recipes = recipes.Select(x => new RecipeResponse
                {
                    RecipeId = x.RecipeId,
                    Name = x.Name,
                    Category = x.Category,
                    TasteProfile = x.TasteProfile,
                    Ingredients = x.Ingredients,
                    Instructions = x.Instructions,
                    PreparationTime = x.PreparationTime
                }).ToList()
            };
        }
    }
}