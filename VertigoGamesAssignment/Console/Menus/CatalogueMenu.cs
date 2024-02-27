using Spectre.Console;
using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
internal class CatalogueMenu : Menu
{
    private readonly Catalogue catalogue;
    private readonly ShoppingCart shoppingCart;
    private readonly Category? category;

    protected override string Title => category == null ?
        "[lime][bold]=============Select items from the catalogue=============[/][/]" : 
        $"[lime][bold]====================={category.Name}=====================[/][/]";

    public CatalogueMenu(Catalogue catalogue, ShoppingCart shoppingCart, Category? category = null) : base()
    {
        this.catalogue = catalogue;
        this.shoppingCart = shoppingCart;
        this.category = category;
    }

    protected override IEnumerable<SelectionItem> GetSelectionChoices()
    {
        Category[] subcategories = catalogue.Categories.Where(x => x.ParentCategory == category).ToArray();
        if (subcategories.Length != 0)
        {
            yield return new CategorySelectionItem("Subcategories",
                subcategories.Select(subcategory => new ChoiceSelectionItem(subcategory.Name, new CatalogueMenu(catalogue, shoppingCart, subcategory))));
        }

        Item[] items = catalogue.Items.Where(x => x.Category == category).ToArray();
        if (items.Length != 0)
        {
            int maxItemNameLength = items.Select(x => x.Name.Length).Max();

            yield return new CategorySelectionItem("Items",
                items.Select(item =>
                {
                    ShoppingCartItem? shoppingCartItem = shoppingCart.Items.Where(x => x.Item == item).FirstOrDefault();
                    return new ChoiceSelectionItem($"{item.Name}".PadRight(maxItemNameLength + 3) + 
                        $"{(shoppingCartItem == null ? "" : $" [red]{shoppingCartItem.Count} in cart {shoppingCartItem.PropertyString}[/]")}", 
                        new ItemMenu(shoppingCart, item));
                })
            );
        }

        if (subcategories.Length == 0 && items.Length == 0)
        {
            yield return new CategorySelectionItem("No items in this category");
        }

        yield return new ChoiceSelectionItem(category == null ? "Back to main menu" : $"Back to {category.Name}", NavigationAction.Back);
    }
}
