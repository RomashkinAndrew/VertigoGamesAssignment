namespace VertigoGamesAssignment.Models;
internal class ShoppingCart
{
    readonly List<ShoppingCartItem> items = new();
    public ShoppingCartItem[] Items => items.ToArray();

    public void Add(ShoppingCartItem item)
    {
        items.Add(item);
    }

    public void Remove(ShoppingCartItem item)
    {
        items.Remove(item);
    }

    public void Clear()
    {
        items.Clear();
    }
}
