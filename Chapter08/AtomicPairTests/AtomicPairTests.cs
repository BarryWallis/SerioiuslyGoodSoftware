using AtomicPairLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtomicPairTests;

[TestClass]
public class AtomicPairTests
{
    [TestMethod]
    public void SetBoth_ValidArguments_SetsTheCorrectValues()
    {
        const int expected1 = 1;
        const int expected2 = 2;
        AtomicPair<int, int> actual = new();

        actual.SetBoth(expected1, expected2);

        Assert.AreEqual(expected1, actual.First);
        Assert.AreEqual(expected2, actual.Second);
    }
}
