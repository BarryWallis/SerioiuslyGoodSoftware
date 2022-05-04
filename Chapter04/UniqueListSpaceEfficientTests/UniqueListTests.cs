
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UniqueListSpaceEfficientLib;

namespace UniqueListSpaceEfficientTests;

[TestClass]
public class UniqueListTests
{
    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void Ctor_CapacityTwo_GetReturnsFalseForAllIndicesIncludingOutOfBounds(int index)
    {
        UniqueList<int> uniqueList = new(2);

        (bool success, int _) = uniqueList.Get(index);

        Assert.IsFalse(success);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(2)]
    public void Set_IndexIsOutOfBounds_ReturnsFalse(int index)
    {
        UniqueList<int> uniqueList = new(2);

        bool success = uniqueList.Set(index, index);

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void Set_UniqueItem_ReturnsTrueAndItemIsInList()
    {
        UniqueList<string> uniqueList = new(2);

        const string element = "1";
        const int index = 1;
        bool success = uniqueList.Set(index, element);

        Assert.IsTrue(success);
        Assert.AreEqual((true, element), uniqueList.Get(index));
    }

    [TestMethod]
    public void Set_DuplicateElement_ReturnsFalse()
    {
        UniqueList<int> uniqueList = new(2);

        const int element = 123;
        bool success1 = uniqueList.Set(1, element);

        bool success2 = uniqueList.Set(0, element);

        Assert.IsTrue(success1);
        Assert.IsFalse(success2);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(2)]
    public void Get_IndexIsOutOfBounds_ReturnsFalse(int index)
    {
        UniqueList<int> uniqueList = new(2);

        (bool success, int _) = uniqueList.Get(index);

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void Get_UninitiaizedIndex_ReturnsFalse()
    {
        UniqueList<int> uniqueList = new(2);

        (bool success, int _) = uniqueList.Get(0);

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void Get_ValidIndex_ReturnsTrueAndElement()
    {
        UniqueList<int> uniqueList = new(2);
        const int element = 1;
        const int index = 1;
        _ = uniqueList.Set(index, element);

        (bool success, int actual) = uniqueList.Get(index);

        Assert.IsTrue(success);
        Assert.AreEqual(element, actual);
    }
}
