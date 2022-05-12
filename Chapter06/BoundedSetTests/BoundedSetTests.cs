using BoundedSetLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoundedSetTests;

[TestClass]
public class BoundedSetTests
{
    private readonly BoundedSet<int> _set = new(3);

    [TestMethod]
    public void Contains_ElementNotPresent_ReturnsFalse()
    {
        BoundedSet<int> ints = new(3);

        bool actual = ints.Contains(0);

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Add_ElementLessThanCapacity_ElementIsPresent()
    {
        BoundedSet<int> ints = new(3);
        const int addedElement = 10;

        (bool isRemoved, int _) = ints.Add(addedElement);

        Assert.IsFalse(isRemoved);
        Assert.IsTrue(ints.Contains(addedElement));
    }

    [TestMethod]
    public void Add_ElementBeyondCapacity_OldestElementRemoved()
    {
        BoundedSet<int> ints = new(3);
        _ = ints.Add(1);
        _ = ints.Add(2);
        _ = ints.Add(3);

        (bool isRemoved, int removedElement) = ints.Add(4);

        Assert.IsFalse(ints.Contains(1));
        Assert.IsTrue(isRemoved);
        Assert.AreEqual(1, removedElement);
    }

    [TestMethod]
    public void Current_EmptySet_ReturnsEmptyCollection()
    {
        BoundedSet<int> ints = new(3);

        IReadOnlyCollection<int> actual = ints.Content;

        Assert.AreEqual(0, actual.Count);
    }

    [TestMethod]
    public void Current_SetWithSomeElements_ReturnsCopyOfSet()
    {
        BoundedSet<int> ints = new(3);
        _ = ints.Add(1);
        _ = ints.Add(2);
        _ = ints.Add(3);

        IReadOnlyCollection<int> actual = ints.Content;

        Assert.AreEqual(3, actual.Count);
        Assert.IsTrue(ints.Contains(1));
        Assert.IsTrue(ints.Contains(2));
        Assert.IsTrue(ints.Contains(3));
    }

    [TestMethod]
    public void Add_Empty_ReturnsFalseWithAddedElement()
    {
        (bool isRemoved, int _) = _set.Add(1);

        Assert.IsFalse(isRemoved);
        Assert.IsTrue(_set.Content.Contains(1));
        Assert.AreEqual(1, _set.Content.Count);
    }

    [TestMethod]
    public void Add_NotEmptyNotFull_ReturnsFalseWithAddeElement()
    {
        _ = _set.Add(1);

        (bool isRemoved, _) = _set.Add(2);

        Assert.IsFalse(isRemoved);
        Assert.AreEqual(2, _set.Content.Count);
        Assert.IsTrue(_set.Content.Contains(1));
        Assert.IsTrue(_set.Content.Contains(2));
    }
}
