using Newtonsoft.Json;

namespace VertigoGamesAssignment.Models;

/// <summary>
/// Stores information about categories and items
/// </summary>
internal class Catalogue
{
    public Category[] Categories { get; }
    public Item[] Items { get; }

    [JsonConstructor]
    internal Catalogue(Category[] categories, Item[] items)
    {
        Categories = categories;
        Items = items;
    }

    /// <summary>
    /// Loads catalogue from file
    /// </summary>
    public static Catalogue Load()
    {
        Catalogue? catalogue = JsonConvert.DeserializeObject<Catalogue>(File.ReadAllText(Constants.CatalogueFileName), new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        
        if (catalogue == null)
        {
            throw new FormatException("Unable to parse catalogue file");
        }
        
        return catalogue;
    }
}
