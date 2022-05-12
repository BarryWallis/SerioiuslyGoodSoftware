using Microsoft.VisualStudio.TestTools.UnitTesting;

using RepositoryLib;

namespace RespositoryTests;

[TestClass]
public class RepositoryTests
{
    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Ctor_WithIllegalNumberOfCells_ThrowsArgumentOutOfRangeException(int n)
        => _ = new Repository<int>(n);

    [TestMethod]
    public void Ctor_WithValidNumberOfCells_AllCellsAreDefaultValue()
    {
        Repository<string> repository = new(2);

        Assert.IsNull(repository.Get(0));
        Assert.IsNull(repository.Get(1));
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(3)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Get_InvalidCell_ThrowsArgumentOutOfRangeException(int i)
    {
        Repository<int> repository = new(3);

        int _ = repository.Get(i);
    }

    [TestMethod]
    public void Get_ValidCell_ReturnsContentsOfCell()
    {
        const int i = 0;
        const int expected = 123;
        Repository<int> repository = new(1);
        _ = repository.Set(i, expected);

        int actual = repository.Get(i);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(3)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Set_InvalidCell_ThrowsArgumentOutOfRangeException(int i)
    {
        Repository<int> repository = new(3);

        int _ = repository.Set(i, 123);
    }

    [TestMethod]
    public void Set_ValidCell_ReturnsPreviousValue()
    {
        const int expected = 123;
        Repository<int> repository = new(1);

        int actual1 = repository.Set(0, expected);
        int actual2 = repository.Set(0, expected + 1);

        Assert.AreEqual(0, actual1);
        Assert.AreEqual(expected, actual2);
        Assert.AreEqual(expected + 1, repository.Get(0));
    }

    [TestMethod]
    [DataRow(-1, 1)]
    [DataRow(3, 1)]
    [DataRow(1, -1)]
    [DataRow(1, 3)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Swap_IllegalCell_ThrowsArgumentOutOfRange(int i, int j)
    {
        Repository<int> repository = new(3);

        repository.Swap(i, j);
    }

    [TestMethod]
    public void Swap_ValidSwap_SwapsValues()
    {
        Repository<int> repository = new(2);
        _ = repository.Set(0, 0);
        _ = repository.Set(1, 1);

        repository.Swap(0, 1);

        Assert.AreEqual(1, repository.Get(0));
        Assert.AreEqual(0, repository.Get(1));
    }
}
