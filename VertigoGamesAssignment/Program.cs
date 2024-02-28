using Spectre.Console;
using VertigoGamesAssignment.Console.Menus;
using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment;

internal class Program
{
    static void Main()
    {
        try
        {
            Catalogue catalogue = Catalogue.Load();
            ShoppingCart shoppingCart = ShoppingCart.Load(catalogue);
            new MainMenu(catalogue, shoppingCart).Show();
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]Unhandled exception:[/]");
            AnsiConsole.WriteLine(e.Message);
            AnsiConsole.MarkupLine($"[grey]Press any key to exit[/]");
            AnsiConsole.Console.Input.ReadKey(false);
        }
    }
}
