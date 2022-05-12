using IntStatsLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntStatsTests;

[TestClass]
public class IntStatsTests
{
    [TestMethod]
    public void Sum_EmptyList_ReturnsZero()
    {
        IntStats intStats = new();

        long sum = intStats.Sum;

        Assert.AreEqual(0, sum);
    }

    [TestMethod]
    public void Sum_InsertFiveInsertThree_SumIsEight()
    {
        IntStats intStats = new();

        intStats.Insert(5);
        intStats.Insert(3);
        long actual = intStats.Sum;

        Assert.AreEqual(8, actual);
    }

    [TestMethod]
    public void Insert_Five_SumIsFive()
    {
        IntStats instats = new();

        instats.Insert(5);
        long actual = instats.Sum;

        Assert.AreEqual(5, actual);
    }

    [TestMethod]
    public void Average_ListOfIntegers_AverageOfList()
    {
        int[] list = new int[] { 1, 2, 3, 4 };
        IntStats intStats = new();
        double expected = list.Average();

        foreach (int i in list)
        {
            intStats.Insert(i);
        }
        double actual = intStats.Average;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Median_ListWithOddNumberOfElements_MiddleNumber()
    {
        int[] list = new int[] { 5, 4, 3, 2, 1 };
        IntStats intStats = new();
        double expected = 3;

        foreach (int i in list)
        {
            intStats.Insert(i);
        }
        double actual = intStats.Median;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Median_ListWithEvenNumberOfElements_AverageOfMiddleNumbers()
    {
        int[] list = new int[] { 4, 3, 2, 1 };
        IntStats intStats = new();
        double expected = 2.5;

        foreach (int i in list)
        {
            intStats.Insert(i);
        }
        double actual = intStats.Median;

        Assert.AreEqual(expected, actual);
    }
}
