using RecipeHandler.Infrastructure.Features.Recipes.Models;
using RecipeHandler.Infrastructure.MongoDbPersistence.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesHandler.Tests.Factories;

public static class RecipeFactory
{
    public static RecipeDocument CreateRecipe()
    {
        var rnd = new Random((int)DateTime.Now.Ticks);

        var RecipeId = Guid.NewGuid();
        var name = $"Recipe {rnd.Next(1, 1000)}";
        var category = $"Category {rnd.Next(1, 10)}";
        var tasteProfile = new List<string> { "Sweet", "Savory", "Spicy" };
        var ingredients = new List<string> { "Ingredient 1", "Ingredient 2", "Ingredient 3" };
        var instructions = new List<string> { "Step 1", "Step 2", "Step 3" };
        var preparationTime = new TimeOnly(rnd.Next(0, 2), rnd.Next(0, 60));

        return new RecipeDocument
        {
            Id = MongoDB.Bson.ObjectId.GenerateNewId(),
            RecipeId = RecipeId,
            Name = name,
            Category = category,
            TasteProfile = tasteProfile,
            Ingredients = ingredients,
            Instructions = instructions,
            PreparationTime = preparationTime,
            Created = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };
    }
}