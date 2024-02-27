namespace VertigoGamesAssignment.Models;

internal class Item
{
    public string Name { get; private set; }
    public Category Category { get; private set; }
    public string? Description { get; private set; }
    public double Price { get; private set; }
    public Property[] Properties { get; private set; }

    public class Property
    {
        public string Name { get; private set; }
        public string[] PossibleValues { get; private set; }

        public Property(string name, params string[] possibleValues)
        {
            Name = name;
            PossibleValues = possibleValues;
        }
    }

    public Item(string name, Category category, double price, IEnumerable<Property>? properties = null, string? description = null)
    {
        Name = name;
        Category = category;
        Price = price;
        Description = description;
        Properties = (properties ?? Array.Empty<Property>()).ToArray();
    }
}
