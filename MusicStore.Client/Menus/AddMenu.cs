using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHandler.Client;

public class AddMenuRequest
{
    public required string Name { get; set; }
    public required List<string> Occasions { get; set; }
    public required List<Guid> RecipeIds { get; set; }
}

public class AddMenuResponse
{
    public required Guid MenuId { get; set; }
}