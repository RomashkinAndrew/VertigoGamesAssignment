using Spectre.Console;

namespace VertigoGamesAssignment.Console;
/// <summary>
/// Basic class for a menu
/// </summary>
internal abstract class Menu
{
    protected abstract string Title { get; }
    protected abstract IEnumerable<SelectionItem> GetSelectionChoices();

    /// <summary>
    /// Shows current menu
    /// </summary>
    public void Show()
    {
        SelectionPrompt<SelectionItem> prompt = new();
        prompt.Title = Title;
        SelectionItem[] selectionItems = GetSelectionChoices().ToArray();
        foreach (SelectionItem selectionItem in selectionItems)
        {
            if (selectionItem is ChoiceSelectionItem choiceSelectionItem)
            {
                prompt.AddChoice(choiceSelectionItem);
            }
            if (selectionItem is CategorySelectionItem categorySelectionItem)
            {
                prompt.AddChoiceGroup(categorySelectionItem, categorySelectionItem.Choices);
            }
        }

        ChoiceSelectionItem selectedItem = (ChoiceSelectionItem)AnsiConsole.Prompt(prompt);
        selectedItem.Action?.Invoke();

        switch (selectedItem.NavigationAction)
        {
            case NavigationAction.Back:
                Back();
                break;
            case NavigationAction.Stay:
                Show();
                break;
            case NavigationAction.Next:
                MoveTo(selectedItem.NextMenu!);
                break;
        }
    }

    #region Navigation

    /// <summary>
    /// Stores menu hierarchy
    /// </summary>
    static readonly List<Menu> breadCrumbs = new();

    /// <summary>
    /// Move to the next menu
    /// </summary>
    void MoveTo(Menu menu)
    {
        breadCrumbs.Add(this);
        menu.Show();
    }

    /// <summary>
    /// Move to the previous menu
    /// </summary>
    protected static void Back()
    {
        if (breadCrumbs.Count == 0)
        {
            throw new InvalidOperationException("Unable to go back from the root menu");
        }
        Menu menu = breadCrumbs.Last();
        breadCrumbs.Remove(menu);
        menu.Show();
    }
    #endregion Navigation
}
