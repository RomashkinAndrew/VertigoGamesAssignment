using Spectre.Console;
using System.Globalization;
using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
/// <summary>
/// Shopping cart overview
/// </summary>
internal class ShoppingCartMenu : Menu
{
    protected override string Title => 
        $"[lime][bold]==========================Shopping cart==========================[/][/]\n" +
        $"Total: EUR [blue][bold]{shoppingCart.Items.Sum(x=>x.Count*x.Item.Price).ToString("0.00", CultureInfo.InvariantCulture)}[/][/]" +
        (shoppingCart.Items.Length != 0 ? "": $"\n\nYour shopping cart is empty.Add items in the catalogue");

    private readonly ShoppingCart shoppingCart;

    public ShoppingCartMenu(ShoppingCart shoppingCart)
    {
        this.shoppingCart = shoppingCart;
    }

    protected override IEnumerable<SelectionItem> GetSelectionChoices()
    {
        if (shoppingCart.Items.Length != 0)
        {
            int maxItemNameLength = shoppingCart.Items.Select(i => i.Item.Name.Length).Max();

            yield return new CategorySelectionItem("Items in your cart",
                shoppingCart.Items.Select(item => new ChoiceSelectionItem($"{item.Item.Name}".PadRight(maxItemNameLength+3)+
                $" [red]{item.Count} in cart {item.PropertyString}[/]",
                new ItemMenu(shoppingCart, item.Item))));

            yield return new ChoiceSelectionItem("Checkout", NavigationAction.None, ()=>
            {
                if (MiscMenus.Checkout(shoppingCart))
                {
                    shoppingCart.Clear();
                    Back();
                    Back();
                }
                else
                {
                    Show();
                }
            });

            yield return new ChoiceSelectionItem("Clear shopping cart", () =>
            {
                if (AnsiConsole.Confirm("[yellow][bold]Are you sure you want to clear your shopping cart?[/][/]"))
                {
                    shoppingCart.Clear();
                }
                AnsiConsole.Clear();
            });
        }

        yield return new ChoiceSelectionItem("Back to main menu", NavigationAction.Back);
    }
}
