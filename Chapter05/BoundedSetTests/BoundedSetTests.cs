
using BoundedSetLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoundedSetTests;

[TestClass]
public class BoundedSetTests
{
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

        ints.Add(addedElement);

        Assert.IsTrue(ints.Contains(addedElement));
    }

    [TestMethod]
    public void Add_ElementBeyondCapacity_OldestElementRemoved()
    {
        BoundedSet<int> ints = new(3);
        ints.Add(1);
        ints.Add(2);
        ints.Add(3);

        ints.Add(4);

        Assert.IsTrue(ints.Contains(2));
        Assert.IsTrue(ints.Contains(3));
        Assert.IsTrue(ints.Contains(4));
        Assert.IsFalse(ints.Contains(1));
    }
}
