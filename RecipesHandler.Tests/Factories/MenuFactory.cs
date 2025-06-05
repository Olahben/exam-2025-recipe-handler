using RecipeHandler.Infrastructure.MongoDbPersistence.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesHandler.Tests.Factories;

public static class MenuFactory
{
    public static MenuDocument CreateMenu(List<Guid> recipeIds)
    {
        var rndom = new Random();

        var menuId = Guid.NewGuid();
        var name = $"Menu {rndom.Next(1, 1000)}";
        var occasions = new List<string>
        {
            "Lunch",
            "Dinner"
        };



        return new MenuDocument
        {
            Id = MongoDB.Bson.ObjectId.GenerateNewId(),
            MenuId = menuId,
            Name = name,
            Occasions = occasions,
            RecipeIds = recipeIds
        };
    }
}
