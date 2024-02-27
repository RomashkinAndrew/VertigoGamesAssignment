using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;

internal class PropertyMenu : Menu
{
    protected override string Title => $"[lime][bold]=========================={title}==========================[/][/]";
    private readonly string title = string.Empty;
    private readonly Property property;

    public PropertyMenu(string title, Property property)
    {
        this.title = title;
        this.property = property;
    }

    protected override IEnumerable<SelectionItem> GetSelectionChoices()
    {
        foreach (string possibleValue in property.PossibleValues)
        {
            yield return new ChoiceSelectionItem(possibleValue, NavigationAction.Back, ()=> property.Value = possibleValue);
        }
        yield return new ChoiceSelectionItem("Back", NavigationAction.Back);
    }
}
