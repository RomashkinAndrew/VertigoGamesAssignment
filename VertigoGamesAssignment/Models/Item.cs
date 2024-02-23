namespace VertigoGamesAssignment.Models;

internal class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public string? Description { get; set; }
    public Property[] Properties { get; set; }

    public class Property
    {
        public string Name { get; set; }
        public string[] PossibleValues { get; set; }

        public Property(string name, params string[] possibleValues)
        {
            Name = name;
            PossibleValues = possibleValues;
        }
    }

    public Item(int id, string name, Category category, IEnumerable<Property>? properties = null, string? description = null)
    {
        Id = id;
        Name = name;
        Category = category;
        Description = description;
        Properties = (properties ?? Array.Empty<Property>()).ToArray();
    }
}
