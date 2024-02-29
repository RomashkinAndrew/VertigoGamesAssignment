namespace VertigoGamesAssignment.Models;

/// <summary>
/// Represents an item from the catalogue
/// </summary>
internal class Item
{
    public string Name { get; }
    public Category? Category { get; }
    public string? Description { get; }
    public double Price { get; }
    public Property[] Properties { get; }

    public class Property
    {
        public string Name { get; }
        public string[] PossibleValues { get; }

        public Property(string name, params string[] possibleValues)
        {
            Name = name;
            PossibleValues = possibleValues;
        }
    }

    public Item(string name, Category? category, double price, IEnumerable<Property>? properties = null, string? description = null)
    {
        Name = name;
        Category = category;
        Price = price;
        Description = description;
        Properties = (properties ?? Array.Empty<Property>()).ToArray();
    }
}
