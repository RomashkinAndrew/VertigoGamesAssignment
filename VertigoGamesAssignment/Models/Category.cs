namespace VertigoGamesAssignment.Models;

/// <summary>
/// Represents item category
/// </summary>
internal class Category
{
    public string Name { get; }
    public Category? ParentCategory { get; }

    public Category(string name, Category? parentCategory = null)
    {
        Name = name;
        ParentCategory = parentCategory;
    }
}
