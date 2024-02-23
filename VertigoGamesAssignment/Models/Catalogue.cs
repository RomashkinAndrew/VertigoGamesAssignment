namespace VertigoGamesAssignment.Models;

internal class Catalogue
{
    public Category[] Categories { get; private set; }
    public Item[] Items { get; private set; }

    public Catalogue()
    {
        List<Category> categories = new();
        categories.Add(new Category(0, "Clothes"));
        categories.Add(new Category(1, "Shoes"));
        categories.Add(new Category(2, "Jackets", categories[0]));
        categories.Add(new Category(3, "Hoodies", categories[0]));
        categories.Add(new Category(4, "Dresses", categories[0]));

        Item[] items = new Item[] {
            new Item(0, "Shoe 1", categories[1]),
            new Item(1, "Shoe 1", categories[1]),
            new Item(2, "Hat 1", categories[0], new Item.Property[]{ new Item.Property("Size", "S", "M", "L", "XL"), new Item.Property("Color", "Red", "Blue", "Green") }, "A nice hat"),
            new Item(3, "Jacket 1", categories[2]),
            new Item(4, "Jacket 2", categories[2]),
            new Item(5, "Hoodie 1", categories[3]),
            new Item(6, "Dress 1", categories[4]),
        };
        Categories = categories.ToArray();
        Items = items.ToArray();
    }
}
