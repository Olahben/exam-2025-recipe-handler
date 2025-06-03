using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHandler.Client;

public class SearchRecipesRequest
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public List<string>? TasteProfile { get; set; }
}

public class SearchRecipesResponse
{
    public long Count { get; set; }
    public List<RecipeResponse> Recipes { get; set; } = new List<RecipeResponse>();
}
