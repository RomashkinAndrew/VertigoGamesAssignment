using System.Globalization;
using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
internal class ItemMenu : Menu
{
    protected override string Title => $"[lime][bold]========================{item.Name}========================[/][/]" +
        $"\n\n{item.Description}"+
        $"\n\nEUR [blue][bold]{item.Price.ToString("0.00", CultureInfo.InvariantCulture)}[/][/]";

    private readonly Item item;
    private readonly ShoppingCart shoppingCart;
    private readonly ShoppingCartItem shoppingCartItem;

    public ItemMenu(ShoppingCart shoppingCart, Item item)
    {
        this.shoppingCart = shoppingCart;
        this.item = item;
        shoppingCartItem = shoppingCart.Items.Where(x => x.Item == item).FirstOrDefault() ?? new ShoppingCartItem(item);
    }

    protected override IEnumerable<SelectionItem> GetSelectionChoices()
    {
        yield return new ChoiceSelectionItem($"Count: [red]{shoppingCartItem.Count}[/]", ()=> MiscMenus.ItemCount(shoppingCartItem));

        foreach (Property property in shoppingCartItem.Properties)
        {
            yield return new ChoiceSelectionItem($"{property.Name}: [red][bold]{property.Value}[/][/]", new PropertyMenu($"Select {property.Name}:", shoppingCart, property));
        }

        yield return !shoppingCart.Items.Contains(shoppingCartItem)
            ? new ChoiceSelectionItem("Add to cart", NavigationAction.Back, () => shoppingCart.Add(shoppingCartItem))
            : new ChoiceSelectionItem("Remove from cart", NavigationAction.Back, () => shoppingCart.Remove(shoppingCartItem));

        yield return new ChoiceSelectionItem("Back", NavigationAction.Back);
    }
}
