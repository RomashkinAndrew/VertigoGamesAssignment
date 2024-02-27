namespace VertigoGamesAssignment.Models;

internal class ShoppingCartItem
{
    public Item Item { get; set; }
    public int Count { get; set; }
    public Property[] Properties { get; set; }

    public string PropertyString => Properties.Length == 0 ? "" : string.Join(", ", Properties.Select(x => x.Name + ": " + x.Value));

    public ShoppingCartItem(Item item, int count = 1)
    {
        Item = item;
        Count = count;
        Properties = item.Properties.Select(x => new Property(x.Name, x.PossibleValues[0], x.PossibleValues)).ToArray();
    }
}
