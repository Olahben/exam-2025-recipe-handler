using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Client;

public class AddRecipeRequest
{
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required List<string> TasteProfile { get; set; }
    public required List<string> Ingredients { get; set; }
    public required List<string> Instructions { get; set; }
    public required TimeOnly PreparationTime { get; set; }
}

public class AddRecipeResponse
{
    public Guid RecipeId { get; set; }
}
