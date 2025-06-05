using MediatR;
using RecipeHandler.Client;
using RecipeHandler.Infrastructure.MongoDbPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHandler.Infrastructure.Features.Menus;

public static class Search
{
    public record Query(
        List<Guid>? MenuIds = null,
        List<string>? Occasions = null) : IRequest<SearchMenusRespone>;

    public class Handler(
        MenusRepository menusRepository) : IRequestHandler<Query, SearchMenusRespone>
    {
        public async Task<SearchMenusRespone> Handle(
            Query cmd,
            CancellationToken cancellationToken)
        {
            var (count, menus) = await menusRepository.GetAll(
                menuIds: cmd.MenuIds,
                occasions: cmd.Occasions,
                skip: 0,
                limit: 100,
                cancellationToken: cancellationToken);

            return new SearchMenusRespone
            {
                Count = count,
                Menus = menus.Select(x => new MenuResponse
                {
                    MenuId = x.MenuId,
                    Name = x.Name,
                    Occasions = x.Occasions,
                    RecipeIds = x.RecipeIds,
                    Created = x.Created,
                    ModifiedAt = x.ModifiedAt
                }).ToList()
            };
        }
    }

}
