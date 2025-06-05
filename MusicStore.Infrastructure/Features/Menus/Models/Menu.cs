using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHandler.Infrastructure.Features.Menus.Models;

public class Menu
{
    public Guid MenuId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Occasions { get; set; } = new List<string>();
    public List<Guid> RecipeIds { get; set; } = new List<Guid>();
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
