namespace VertigoGamesAssignment.Models;

/// <summary>
/// Represents an item in the shopping cart
/// </summary>
internal class ShoppingCartItem
{
    public Item Item { get; set; }
    public int Count { get; set; }
    public Property[] Properties { get; set; }

    public string PropertyString => Properties.Length == 0 ? "" : string.Join(", ", Properties.Select(p => p.Name + ": " + p.Value));

    public ShoppingCartItem(Item item, int count = 1)
    {
        Item = item;
        Count = count;
        Properties = item.Properties.Select(i => new Property(i.Name, i.PossibleValues[0], i.PossibleValues)).ToArray();
    }
}
