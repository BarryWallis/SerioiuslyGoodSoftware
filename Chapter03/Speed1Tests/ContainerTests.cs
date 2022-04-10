using Microsoft.VisualStudio.TestTools.UnitTesting;

using Speed1;

namespace Speed1Tests;

[TestClass]
public class ContainerTests
{
    [TestMethod]
    public void Ctor_Default_ReturnsZero()
    {
        Container container = new();

        double actual = container.Amount;

        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void GetAmount_DefaultContainer_ReturnsZero()
    {
        Container container = new();

        double actual = container.Amount;

        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void GroupSize_GroupOfThreeContainers_ReturnsThree()
    {
        Container container1 = new();
        Container container2 = new();
        Container container3 = new();
        const int expected = 3;
        container1.ConnectTo(container2);
        container2.ConnectTo(container3);

        int actual1 = container1.GroupSize;
        int actual2 = container2.GroupSize;
        int actual3 = container3.GroupSize;

        Assert.AreEqual(expected, actual1);
        Assert.AreEqual(expected, actual2);
        Assert.AreEqual(expected, actual3);
    }

    [TestMethod]
    public void AddWater_AddWaterToOneContainer_ReturnsTheAmountAdded()
    {
        const double amountToAdd = 5.5;
        Container container = new();

        container.AddWater(amountToAdd);

        Assert.AreEqual(amountToAdd, container.Amount);
    }

    [TestMethod]
    public void AddWater_AddNegativeAmountOfWater_ReturnsTheAmountAddedMinusTheNegativeAmount()
    {
        const double amountToAdd = 5.5;
        const double amountToSubtract = -.5;
        Container container = new();

        container.AddWater(amountToAdd);
        container.AddWater(amountToSubtract);

        Assert.AreEqual(amountToAdd + amountToSubtract, container.Amount);
    }

    [TestMethod]
    public void ConnectTo_ConnectTwoContainers_ReturnsAmountSplitBetweenTheContainers()
    {
        const double amountToAdd = 12.0;
        Container container1 = new();
        Container container2 = new();
        container1.AddWater(amountToAdd);

        container1.ConnectTo(container2);

        Assert.AreEqual(amountToAdd / 2, container1.Amount);
        Assert.AreEqual(amountToAdd / 2, container2.Amount);
    }

    [TestMethod]
    public void ConnectTo_ConnectThreeContainers_ReturnsSameAmountInAllThree()
    {
        const double amountToAdd = 12.0;
        Container container1 = new();
        Container container2 = new();
        Container container3 = new();
        container1.AddWater(amountToAdd);

        container1.ConnectTo(container2);
        container2.ConnectTo(container3);

        Assert.AreEqual(amountToAdd / 3, container1.Amount);
        Assert.AreEqual(amountToAdd / 3, container2.Amount);
        Assert.AreEqual(amountToAdd / 3, container3.Amount);
    }

    [TestMethod]
    public void ConnectTo_ConnectFourContainers_ReturnsSameAmountInAllFour()
    {
        const double amountToAddToContainer1 = 12.0;
        const double amountToAddToContainer4 = 8.0;
        const double totalAmount = amountToAddToContainer1 + amountToAddToContainer4;
        Container container1 = new();
        Container container2 = new();
        Container container3 = new();
        Container container4 = new();
        container1.AddWater(amountToAddToContainer1);
        container4.AddWater(amountToAddToContainer4);

        container1.ConnectTo(container2);
        container2.ConnectTo(container3);
        container2.ConnectTo(container4);

        Assert.AreEqual(totalAmount / 4, container1.Amount);
        Assert.AreEqual(totalAmount / 4, container2.Amount);
        Assert.AreEqual(totalAmount / 4, container3.Amount);
        Assert.AreEqual(totalAmount / 4, container4.Amount);
    }

    [TestMethod]
    public void Flush_AnyContainer_NoWaterInAnyContainersInGroup()
    {
        Container container1 = new();
        Container container2 = new();
        Container container3 = new();
        const double expected = 0.0;
        container1.AddWater(10);
        container1.ConnectTo(container2);
        container2.ConnectTo(container3);

        container2.Flush();
        double actual1 = container1.Amount;
        double actual2 = container2.Amount;
        double actual3 = container3.Amount;

        Assert.AreEqual(expected, actual1);
        Assert.AreEqual(expected, actual2);
        Assert.AreEqual(expected, actual3);
    }

    [TestMethod]
    public void UseCaseTest()
    {
        Container containerA = new();
        Container containerB = new();
        Container containerC = new();
        Container containerD = new();

        containerA.AddWater(12);
        containerD.AddWater(8);
        containerA.ConnectTo(containerB);
        containerB.ConnectTo(containerC);

        Assert.AreEqual(4, containerA.Amount);
        Assert.AreEqual(4, containerB.Amount);
        Assert.AreEqual(4, containerC.Amount);
        Assert.AreEqual(8, containerD.Amount);
    }
}
