using Newtonsoft.Json;

namespace VertigoGamesAssignment.Models;

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
