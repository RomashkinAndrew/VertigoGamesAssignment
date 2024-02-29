using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
/// <summary>
/// Property selection
/// </summary>
internal class PropertyMenu : Menu
{
    protected override string Title => $"[lime][bold]=========================={title}==========================[/][/]";
    private readonly string title = string.Empty;
    private readonly ShoppingCart shoppingCart;
    private readonly Property property;

    public PropertyMenu(string title, ShoppingCart shoppingCart, Property property)
    {
        this.title = title;
        this.shoppingCart = shoppingCart;
        this.property = property;
    }

    protected override IEnumerable<SelectionItem> GetSelectionChoices()
    {
        foreach (string possibleValue in property.PossibleValues)
        {
            yield return new ChoiceSelectionItem(possibleValue, NavigationAction.Back, ()=> { property.Value = possibleValue; shoppingCart.Save(); });
        }
        yield return new ChoiceSelectionItem("Back", NavigationAction.Back);
    }
}
