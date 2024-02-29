namespace VertigoGamesAssignment.Console;

/// <summary>
/// Menu item representing a choice
/// </summary>
internal class ChoiceSelectionItem : SelectionItem
{
    public Action? Action { get; private set; } = null;
    public NavigationAction NavigationAction { get; private set; }
    public Menu? NextMenu { get; private set; } = null;

    public ChoiceSelectionItem(string text, Action action) : base(text)
    {
        Action = action;
        NavigationAction = NavigationAction.Stay;
    }

    public ChoiceSelectionItem(string text, NavigationAction navigationAction, Action? action = null) : base(text)
    {
        if (navigationAction == NavigationAction.Next)
        {
            throw new ArgumentException("You have to specify next menu");
        }
        NavigationAction = navigationAction;
        Action = action;
    }

    public ChoiceSelectionItem(string text, Menu nextMenu, Action? action = null) : base(text)
    {
        Action = action;
        NavigationAction = NavigationAction.Next;
        NextMenu = nextMenu;
    }
}
