using VertigoGamesAssignment.Console.Menus;
using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment;

internal class Program
{
    static void Main()
    {
        Catalogue catalogue = new();
        ShoppingCart shoppingCart = new();
        new CatalogueMenu(catalogue, shoppingCart).Show();
    }
}
