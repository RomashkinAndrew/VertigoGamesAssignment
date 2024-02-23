using Spectre.Console;

namespace VertigoGamesAssignment.Console;

internal abstract class Menu
{
    protected SelectionPrompt<SelectionItem> prompt;
    protected abstract string Title { get; }

    public Menu()
    {
        prompt = new SelectionPrompt<SelectionItem>();
    }

    public void Show()
    {
        prompt.Title = Title;
        SelectionItem selectionItem;
        do
        {
            selectionItem = AnsiConsole.Prompt(prompt);
        } while (selectionItem.Action == null);

        selectionItem.Action.Invoke();
    }
}
