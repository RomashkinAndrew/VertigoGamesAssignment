namespace VertigoGamesAssignment.Models;

[Serializable]
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
