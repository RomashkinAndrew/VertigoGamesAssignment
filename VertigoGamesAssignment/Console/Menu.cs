using Spectre.Console;

namespace VertigoGamesAssignment.Console;

internal abstract class Menu
{
    protected abstract string Title { get; }
    protected abstract IEnumerable<SelectionItem> GetSelectionChoices();

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

    static readonly List<Menu> breadCrumbs = new();

    void MoveTo(Menu menu)
    {
        breadCrumbs.Add(this);
        menu.Show();
    }

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
