using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiSetLib;

namespace MultiSetTests;

[TestClass]
public class MultiSetTests
{
    [TestMethod]
    public void Count_EmptyMultiSet_ReturnsZero()
    {
        MultiSet<char> set = new();
        long actual = set.Count('a');

        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void Add_SingleElement_ReturnsOne()
    {
        MultiSet<char> set = new();

        set.Add('a');

        Assert.AreEqual(1, set.Count('a'));
    }

    [TestMethod]
    public void Count_DuplicateElement_ReturnsTwo()
    {
        MultiSet<char> set = new();
        set.Add('a');
        set.Add('b');
        set.Add('b');

        Assert.AreEqual(2, set.Count('b'));
    }
}
