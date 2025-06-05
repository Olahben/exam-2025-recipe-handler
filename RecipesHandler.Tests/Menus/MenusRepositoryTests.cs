using RecipeHandler.Infrastructure.MongoDbPersistence;
using RecipesHandler.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesHandler.Tests.Menus;

[Collection("Global Collection")]
public class MenusRepositoryTests(TestFixture fix)
{
    [Fact]
    public async Task Create_menu_should_work()
    {
        var menusRepository = fix.Get<MenusRepository>();

        var recipe1 = RecipeFactory.CreateRecipe();
        var recipe2 = RecipeFactory.CreateRecipe();

        var recipeIds = new List<Guid>
        {
            recipe1.RecipeId,
            recipe2.RecipeId
        };

        var menu = MenuFactory.CreateMenu(recipeIds);

        await menusRepository.Insert(
            menuId: menu.MenuId,
            name: menu.Name,
            occasions: menu.Occasions,
            recipeIds: menu.RecipeIds);

        var (count, menus) = await menusRepository.GetAll();

        Assert.Equal(1, count);
    }
}
