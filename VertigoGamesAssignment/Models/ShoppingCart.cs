using Newtonsoft.Json;

namespace VertigoGamesAssignment.Models;
internal class ShoppingCart
{
    readonly List<ShoppingCartItem> items = new();
    public ShoppingCartItem[] Items => items.ToArray();

    public void Add(ShoppingCartItem item)
    {
        items.Add(item);
        Save();
    }

    public void Remove(ShoppingCartItem item)
    {
        items.Remove(item);
        Save();
    }

    public void Clear()
    {
        items.Clear();
        Save();
    }

    #region Serialization/deserialization

    private class State
    {
        public class Item
        {
            public string Name { get; set; }
            public int Count { get; set; }
            public Dictionary<string,string> Properties { get; set; }

            public Item(string name, int count, Dictionary<string, string> properties)
            {
                Name = name;
                Count = count;
                Properties = properties;
            }
        }

        public Item[] Items { get; set; }

        public State(Item[] items)
        {
            Items = items;
        }
    }

    public void Save()
    {
        State state = new(
            items.Select(
                i => new State.Item(
                    i.Item.Name,
                    i.Count,
                    new Dictionary<string, string>(i.Properties.Select(p => new KeyValuePair<string, string>(p.Name, p.Value)))))
            .ToArray());

        string json = JsonConvert.SerializeObject(state);
        File.WriteAllText(Constants.StateFileName, json);
    }

    public static ShoppingCart Load(Catalogue catalogue)
    {
        ShoppingCart shoppingCart = new();
        try
        {
            State state = JsonConvert.DeserializeObject<State>(File.ReadAllText(Constants.StateFileName))!;

            foreach (State.Item stateItem in state.Items)
            {
                Item? catalogueItem = catalogue.Items.FirstOrDefault(i => stateItem.Name == i.Name);
                if (catalogueItem == null)
                {
                    continue;
                }

                if (stateItem.Properties.Keys.Except(catalogueItem.Properties.Select(p=>p.Name)).Any())
                {
                    continue;
                }

                if (stateItem.Properties.Any(p1 => !catalogueItem.Properties.First(p2=>p1.Key == p2.Name).PossibleValues.Contains(p1.Value)))
                {
                    continue;
                }

                ShoppingCartItem newItem = new(catalogueItem, stateItem.Count);
                foreach (string key in stateItem.Properties.Keys)
                {
                   newItem.Properties.First(p=>p.Name == key).Value = stateItem.Properties[key];
                }
                shoppingCart.items.Add(newItem);
            }
        }
        catch { }

        shoppingCart.Save();

        return shoppingCart;
    }

    #endregion Serialization/deserialization
}
