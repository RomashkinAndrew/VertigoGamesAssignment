namespace VertigoGamesAssignment.Console;

internal class SelectionItem
{
    public string Text { get; set; }
    public Action? Action { get; set; } = null;

    public SelectionItem(string text, Action? action = null)
    {
        Text = text;
        Action = action;
    }

    public override string ToString()
    {
        return Text;
    }
}
