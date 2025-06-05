using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHandler.Client;

public class SearchMenusRespone
{
    public long Count { get; set; }
    public List<MenuResponse> Menus { get; set; } = new List<MenuResponse>();
}