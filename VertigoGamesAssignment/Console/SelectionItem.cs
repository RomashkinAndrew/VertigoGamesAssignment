namespace VertigoGamesAssignment.Console;

internal abstract class SelectionItem
{
    public string Text { get; private set; }

    protected SelectionItem(string text)
    {
        Text = text;
    }

    public override string ToString()
    {
        return Text;
    }
}
