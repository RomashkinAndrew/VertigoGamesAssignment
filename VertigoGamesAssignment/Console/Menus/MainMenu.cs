using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
/// <summary>
/// Root menu
/// </summary>
internal class MainMenu : Menu
{
    private readonly Catalogue catalogue;
    private readonly ShoppingCart shoppingCart;

    public MainMenu(Catalogue catalogue, ShoppingCart shoppingCart)
    {
        this.catalogue = catalogue;
        this.shoppingCart = shoppingCart;
    }

    protected override string Title => "[lime][bold]==========================Welcome to our shop!==========================[/][/]";

    protected override IEnumerable<SelectionItem> GetSelectionChoices()
    {
        yield return new ChoiceSelectionItem("Catalogue", new CatalogueMenu(catalogue, shoppingCart));
        yield return new ChoiceSelectionItem("Shopping cart", new ShoppingCartMenu(shoppingCart));
        yield return new ChoiceSelectionItem("Exit", () => MiscMenus.Exit());
    }
}
