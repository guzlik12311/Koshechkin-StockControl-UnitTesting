using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockControl;

namespace StockControl.Tests;

[TestClass]
public sealed class StockItemTests
{
    [TestMethod]
    public void Constructor_TrimsNameAndSetsInitialQuantity()
    {
        var item = new StockItem("  Бумага  ", 12);
        Assert.AreEqual("Бумага", item.Name);
        Assert.AreEqual(12, item.Quantity);
    }

    [TestMethod]
    public void Add_PositiveAmount_IncreasesQuantity()
    {
        var item = new StockItem("Маркер", 4);
        item.Add(3);
        Assert.AreEqual(7, item.Quantity);
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-2)]
    public void Add_NonPositiveAmount_Throws(int amount)
    {
        var item = new StockItem("Маркер", 4);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => item.Add(amount));
        Assert.AreEqual(4, item.Quantity);
    }

    [TestMethod]
    public void Remove_WithinAvailableQuantity_DecreasesQuantity()
    {
        var item = new StockItem("Папка", 9);
        item.Remove(5);
        Assert.AreEqual(4, item.Quantity);
    }

    [TestMethod]
    public void Remove_MoreThanAvailable_ThrowsAndKeepsQuantity()
    {
        var item = new StockItem("Папка", 3);
        Assert.ThrowsException<InvalidOperationException>(() => item.Remove(4));
        Assert.AreEqual(3, item.Quantity);
    }

    [TestMethod]
    public void Constructor_EmptyName_Throws()
    {
        Assert.ThrowsException<ArgumentException>(() => new StockItem("  "));
    }

    [TestMethod]
    public void Constructor_NegativeInitialQuantity_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StockItem("Карандаш", -1));
    }
}

