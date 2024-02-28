using VertigoGamesAssignment.Models;

namespace UnitTests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void LoadCatalogue()
    {
        Catalogue catalogue = Catalogue.Load();
        Assert.IsNotNull(catalogue);
        Assert.AreNotEqual(0, catalogue.Categories.Length);
        Assert.AreNotEqual(0, catalogue.Items.Length);
        Assert.AreEqual(catalogue.Categories[0], catalogue.Categories.First(c => c.Name == "Jackets").ParentCategory);
        Assert.AreEqual(catalogue.Categories[0], catalogue.Items.First(c => c.Name == "Straw Hat").Category);
        Assert.AreEqual(catalogue.Categories.First(c => c.Name == "Jackets"), catalogue.Items.First(c => c.Name == "Jersey").Category);
        Assert.AreNotEqual(0, catalogue.Items.First(c => c.Name == "Sandals").Price);
        Assert.IsFalse(string.IsNullOrWhiteSpace(catalogue.Items.First(c => c.Name == "Sneakers").Description));
        Assert.AreNotEqual(0, catalogue.Items.First(c => c.Name == "Trench coat").Properties.Length);
        Assert.AreNotEqual(0, catalogue.Items.First(c => c.Name == "Trench coat").Properties[0].PossibleValues);
        Assert.IsFalse(string.IsNullOrWhiteSpace(catalogue.Items.First(c => c.Name == "Trench coat").Properties[0].Name));
        Assert.IsFalse(string.IsNullOrWhiteSpace(catalogue.Items.First(c => c.Name == "Trench coat").Properties[0].PossibleValues[0]));
    }

    [TestMethod]
    public void LoadState()
    {
        Catalogue catalogue = Catalogue.Load();
        ShoppingCart shoppingCart = new();
        ShoppingCartItem item = new(catalogue.Items[0], 10);
        item.Properties[0].Value = item.Properties[0].PossibleValues[1];
        shoppingCart.Add(item);
        shoppingCart = VertigoGamesAssignment.Models.ShoppingCart.Load(catalogue);
        Assert.IsNotNull(shoppingCart);
        Assert.AreNotEqual(0, shoppingCart.Items.Length);
        Assert.AreNotEqual(0, shoppingCart.Items[0].Properties.Length);
        Assert.AreEqual(catalogue.Items[0].Properties[0].PossibleValues[1], shoppingCart.Items[0].Properties[0].Value);
        Assert.AreEqual(10, shoppingCart.Items[0].Count);
    }

    [TestMethod]
    public void ShoppingCart()
    {
        Catalogue catalogue = Catalogue.Load();
        ShoppingCart shoppingCart = new();
        ShoppingCartItem item = new(catalogue.Items[0], 10);
        item.Properties[0].Value = item.Properties[0].PossibleValues[1];
        shoppingCart.Add(item);
        Assert.AreEqual(1, shoppingCart.Items.Length);
        shoppingCart.Remove(item);
        Assert.AreEqual(0, shoppingCart.Items.Length);
        shoppingCart.Add(item);
        shoppingCart.Clear();
        Assert.AreEqual(0, shoppingCart.Items.Length);
    }
}
