﻿namespace VertigoGamesAssignment.Models;

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
            new Item("Shoe 1", categories[1], 1.53),
            new Item("Shoe 2", categories[1], 2.00),
            new Item("Hat 1", categories[0], 3.00, new Item.Property[]{ new Item.Property("Size", "S", "M", "L", "XL"), new Item.Property("Color", "Red", "Blue", "Green") }, "A nice hat"),
            new Item("Jacket 1", categories[2], 11.72),
            new Item("Jacket 2", categories[2], 30.00),
            new Item("Hoodie 1", categories[3], 7.05),
            new Item("Dress 1", categories[4], 5.03),
        };
        Categories = categories.ToArray();
        Items = items.ToArray();
    }
}
