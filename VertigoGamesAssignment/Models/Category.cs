namespace VertigoGamesAssignment.Models;

internal class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Category? ParentCategory { get; set; } = null;

    public Category(int id, string name, Category? parentCategory = null)
    {
        Id = id;
        Name = name;
        ParentCategory = parentCategory;
    }
}
