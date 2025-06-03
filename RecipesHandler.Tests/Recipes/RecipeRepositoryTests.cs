using RecipeHandler.Infrastructure.MongoDbPersistence;
using RecipesHandler.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesHandler.Tests.Recipes;

[Collection("Global Collection")]
public class RecipeRepositoryTests(TestFixture fix)
{
    [Fact]
    public async Task Create_recipe_should_work()
    {
        var recipeRepository = fix.Get<RecipesRepository>();

        var recipe = RecipeFactory.CreateRecipe();

        await recipeRepository.Insert(
            recipeId: recipe.RecipeId,
            name: recipe.Name,
            category: recipe.Category,
            tasteProfile: recipe.TasteProfile,
            ingredients: recipe.Ingredients,
            instructions: recipe.Instructions,
            preparationTime: recipe.PreparationTime);

        var (count, recipes) = await recipeRepository.GetAll();

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task Get_recipes_with_paging_should_work()
    {
        var recipeRepository = fix.Get<RecipesRepository>();

        for (int i = 0; i < 5; i++)
        {
            var recipe = RecipeFactory.CreateRecipe();

            await recipeRepository.Insert(
                recipeId: recipe.RecipeId,
                name: recipe.Name,
                category: recipe.Category,
                tasteProfile: recipe.TasteProfile,
                ingredients: recipe.Ingredients,
                instructions: recipe.Instructions,
                preparationTime: recipe.PreparationTime);
        }

        var (count, recipes) = await recipeRepository.GetAll(skip: 0, limit: 5);

        Assert.Equal(5, count);
        Assert.Equal(5, recipes.Count);
    }
}
