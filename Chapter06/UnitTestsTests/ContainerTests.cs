using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTestsLib;

namespace UnitTestsTests;

[TestClass]
public class ContainerTests
{
    private readonly Container _containerA = new();
    private readonly Container _containerB = new();

    [TestMethod]
    public void Ctor_NewContainer_IsEmpty()
    {
        double expected = 0.0;

        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AddWater_AddValidNegativeAmountToConnectedContainers_AmountIsSubtractedFromContainers()
    {
        _containerA.ConnectTo(_containerB);
        _containerA.AddWater(10.0);

        _containerA.AddWater(-4.0);

        Assert.AreEqual(3.0, _containerA.Amount);
    }

    [TestMethod]
    public void AddWater_AddPositiveAmountToIsolatedContainer_AmountIsAddedToContainer()
    {
        double expected = 1.0;

        _containerA.AddWater(expected);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AddWater_AddZeroAmountToIsolatedContainer_AmountIsAddedToContainer()
    {
        double expected = 0.0;

        _containerA.AddWater(expected);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AddWater_AddValidNegativeAmountToIsolatedContainer_AmountIsSubtractedFromContainer()
    {
        double initialAmount = 10.5;
        double addedAmount = -2.5;
        double expected = initialAmount + addedAmount;
        _containerA.AddWater(initialAmount);

        _containerA.AddWater(addedAmount);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddWater_AddInvalidNegativeAmountToIsolatedContainer_ThrowsArgumentOutOfRange()
        => _containerA.AddWater(-1.0);

    [TestMethod]
    public void ConnectTo_ConnectIsolatedContainerToDifferentIsolatedContainer_AmountIsDistributedEqually()
    {
        double expected = 1.0;

        _containerA.ConnectTo(_containerB);
        _containerA.AddWater(2);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ConnectTo_ConnectMultipleContainersToDifferentIsolatedContainer_AmountIsDistributedEqually()
    {
        double amountToAdd = 3.0;
        int containersConnected = 3;
        double expected = amountToAdd / containersConnected;
        Container containerC = new();
        _containerA.ConnectTo(_containerB);

        _containerA.ConnectTo(containerC);
        _containerA.AddWater(amountToAdd);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ConnectTo_IsolatedContainerToDifferentMultipleContainers_AmountIsDistributedEqually()
    {
        double amountToAdd = 3.0;
        int containersConnected = 3;
        double expected = amountToAdd / containersConnected;
        Container containerC = new();
        _containerB.ConnectTo(containerC);

        _containerA.ConnectTo(_containerB);
        _containerA.AddWater(amountToAdd);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ConnectTo_MultipleContainersToDifferentMultipleContainers_AmountIsDistributedEqually()
    {
        double amountToAdd = 4.0;
        int containersConnected = 4;
        double expected = amountToAdd / containersConnected;
        Container containerC = new();
        Container containerD = new();
        _containerA.ConnectTo(_containerB);
        containerC.ConnectTo(containerD);

        _containerA.ConnectTo(containerC);
        _containerA.AddWater(amountToAdd);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ConnectTo_MultipleContainersToTheSameMultipleContainers_AmountIsUnchanged()
    {
        double amountToAdd = 4.0;
        int containersConnected = 2;
        double expected = amountToAdd / containersConnected;

        _containerA.ConnectTo(_containerB);
        _containerA.AddWater(amountToAdd);
        _containerB.ConnectTo(_containerA);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ConnectTo_ConnectIsolatedContainerToItself_AmountIsUnchanged()
    {
        double expected = 1.0;

        _containerA.AddWater(1.0);
        _containerA.ConnectTo(_containerA);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ConnectTo_MultipleContainersToThemselves_AmountIsUnchanged()
    {
        double amountToAdd = 4.0;
        int containersConnected = 2;
        double expected = amountToAdd / containersConnected;

        _containerA.ConnectTo(_containerB);
        _containerA.AddWater(amountToAdd);
        _containerA.ConnectTo(_containerA);
        double actual = _containerA.Amount;

        Assert.AreEqual(expected, actual);
    }
}
