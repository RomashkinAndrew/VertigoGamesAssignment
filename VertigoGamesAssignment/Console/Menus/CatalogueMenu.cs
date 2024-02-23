using Spectre.Console;
using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
internal class CatalogueMenu : Menu
{
    protected override string Title => "Select goods from the catalogue";

    public CatalogueMenu(Catalogue catalogue, ShoppingCart shoppingCart, Category? category = null) : base()
    {
        Category[] subcategories = catalogue.Categories.Where(x => x.ParentCategory == category).ToArray();
        if (subcategories.Length != 0)
        {
            prompt.AddChoiceGroup(new SelectionItem("Subcategories"),
                subcategories.Select(subcategory => new SelectionItem(subcategory.Name, () => new CatalogueMenu(catalogue, shoppingCart, subcategory).Show())));
        }

        Item[] items = catalogue.Items.Where(x => x.Category == category).ToArray();
        if (items.Length != 0)
        {
            prompt.AddChoiceGroup(new SelectionItem("Items"),
                items.Select(item =>
                {
                    ShoppingCartItem? shoppingCartItem = shoppingCart.Items.Where(x => x.Item == item).FirstOrDefault();
                    return new SelectionItem($"{item.Name}{(shoppingCartItem == null ? "" : $" [green]{shoppingCartItem.Count} in cart {shoppingCartItem.PropertyString}[/]")}", () => new ItemMenu(catalogue, shoppingCart, item).Show());
                })
            );
        }

        if (subcategories.Length == 0 && items.Length == 0)
        {
            prompt.AddChoiceGroup(new SelectionItem("No items in this category"));
        }

        if (category == null)
        {
            prompt.AddChoice(new SelectionItem("Exit", () =>
            {
                bool exit = AnsiConsole.Confirm("Are you sure you want to exit?");
                AnsiConsole.Clear();
                if (exit)
                {
                    AnsiConsole.MarkupLine("Until next time:hand_with_fingers_splayed:!");
                    AnsiConsole.Console.Input.ReadKey(false);
                }
                else
                {
                    Show();
                }
            }));
        }
        else
        {
            prompt.AddChoice(new SelectionItem("Back", () => new CatalogueMenu(catalogue, shoppingCart, category.ParentCategory).Show()));
        }
    }
}
