namespace VertigoGamesAssignment.Models;
internal class ShoppingCart
{
    public ShoppingCartItem[] Items { get; set; } = Array.Empty<ShoppingCartItem>();

    public void Add(ShoppingCartItem item)
    {
        Items = Items.Where(x => x.Item == item.Item).Append(item).ToArray();
    }
}
