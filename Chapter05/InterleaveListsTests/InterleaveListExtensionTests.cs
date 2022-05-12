using InterleaveListLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterleaveListsTests;

[TestClass]
public class InterleaveListExtensionTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InterleaveList_FirstListIsShorterThanSecondList_ThrowsArgumentException()
    {
        List<int> list1 = new(Enumerable.Range(1, 4));
        List<int> list2 = new(Enumerable.Range(1, 5));

        _ = list1.InterleaveList(list2);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InterleaveList_FirstListIsLongerThanSecondList_ThrowsArgumentException()
    {
        List<int> list1 = new(Enumerable.Range(1, 4));
        List<int> list2 = new(Enumerable.Range(1, 3));

        _ = list1.InterleaveList(list2);
    }

    [TestMethod]
    public void InterleaveList_TwoEqualSizedLists_ReturnsInterleavedList()
    {
        List<int> list1 = new() { 1, 3, 5 };
        List<int> list2 = new() { 2, 4, 6 };
        List<int> expected = new(Enumerable.Range(1, 6));

        IList<int> actual = list1.InterleaveList(list2);

        Assert.IsTrue(actual.SequenceEqual(expected));
    }
    [TestMethod]
    public void InterleaveList_TwoEmptyLists_ReturnsAnEmptyList()
    {
        List<int> list1 = new();
        List<int> list2 = new();

        IList<int> actual = list1.InterleaveList(list2);

        Assert.AreEqual(0, actual.Count);
    }
}
