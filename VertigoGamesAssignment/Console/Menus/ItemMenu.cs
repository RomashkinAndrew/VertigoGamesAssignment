using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
internal class ItemMenu : Menu
{
    protected override string Title => $"[Blue][bold]{item.Name}[/][/]\n{item.Description}";

    readonly Item item;

    public ItemMenu(Catalogue catalogue, ShoppingCart shoppingCart, Item item)
    {
        this.item = item;
        ShoppingCartItem shoppingCartItem = shoppingCart.Items.Where(x => x.Item == item).FirstOrDefault() ?? new ShoppingCartItem(item);
        prompt.AddChoice(new SelectionItem("Add to cart", () =>
        {
            shoppingCart.Add(shoppingCartItem);
            Back(catalogue, shoppingCart, item);
        }));
        prompt.AddChoice(new SelectionItem("Back", () => Back(catalogue, shoppingCart, item)));
    }

    private static void Back(Catalogue catalogue, ShoppingCart shoppingCart, Item item)
    {
        new CatalogueMenu(catalogue, shoppingCart, item.Category).Show();
    }
}
