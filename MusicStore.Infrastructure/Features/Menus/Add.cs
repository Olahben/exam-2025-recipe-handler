using MediatR;
using RecipeHandler.Client;
using RecipeHandler.Client.Exceptions;
using RecipeHandler.Infrastructure.MongoDbPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHandler.Infrastructure.Features.Menus;

public static class Add
{
    public record Command(
        string Name,
        List<string> Occasions,
        List<Guid> RecipeIds) : IRequest<AddMenuResponse>;

    public class Handler(
        MenusRepository menusRepository,
        RecipesRepository recipesRepository) : IRequestHandler<Command, AddMenuResponse>
    {
        public async Task<AddMenuResponse> Handle(
            Command cmd,
            CancellationToken cancellationToken)
        {

            var (isValid, errorMessage) = await ValidateRequest(cmd, cancellationToken);
            if (!isValid)
            {
                throw new BadRequestException(errorMessage);
            }

            var menuId = Guid.NewGuid();

            await menusRepository.Insert(
                menuId: menuId,
                name: cmd.Name,
                occasions: cmd.Occasions,
                recipeIds: cmd.RecipeIds,
                cancellationToken: cancellationToken);


            return new AddMenuResponse
            {
                MenuId = menuId
            };
        }

        private async Task<(bool, string)> ValidateRequest(
            Command cmd,
            CancellationToken cancellationToken)
        {
            // The following validation requirements must be met:
            // All the recipes come from different categories
            // All the reciped must have a common taste profile

            var (count, recipes) = await recipesRepository.GetAll(
                recipeIds: cmd.RecipeIds,
                cancellationToken: cancellationToken);

            // All the recipes must come from different categories
            var categories = recipes.Select(x => x.Category).Distinct().ToList();
            if (categories.Count != cmd.RecipeIds.Count)
            {
                return (false, "All recipes must come from different categories.");
            }

            // All the recipes must have a common taste profile
            var commonTasteProfiles = recipes
                .SelectMany(x => x.TasteProfile)
                .GroupBy(x => x)
                .Where(g => g.Count() == cmd.RecipeIds.Count)
                .Select(g => g.Key)
                .ToList();

            if (!commonTasteProfiles.Any())
            {
                return (false, "All recipes must have a common taste profile.");
            }

            return (true, string.Empty);

        }
    }
}