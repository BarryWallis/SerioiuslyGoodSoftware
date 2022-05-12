
using ContainerSystemLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerSystemTests;

[TestClass]
public class ContainerSystemTests
{
    [TestMethod]
    public void Ctor_Default_ReturnsZero()
    {
        ContainerSystem containerSystem = new(1);

        double actual = containerSystem.GetAmount(0);

        Assert.AreEqual(0.0, actual);
    }

    [TestMethod]
    public void AddWater_AddWaterToOneContainer_ReturnsTheAmountAdded()
    {
        const float amountToAdd = 5.5F;
        ContainerSystem containerSystem = new(1);

        ContainerSystem result = containerSystem.AddWater(0, amountToAdd);

        Assert.AreEqual(amountToAdd, result.GetAmount(0));
    }

    [TestMethod]
    public void AddWater_AddNegativeAmountOfWater_ReturnsTheAmountAddedMinusTheNegativeAmount()
    {
        const float amountToAdd = 5.5F;
        const float amountToSubtract = -.5F;
        ContainerSystem containerSystem = new(1);

        ContainerSystem result = containerSystem.AddWater(0, amountToAdd).AddWater(0, amountToSubtract);

        Assert.AreEqual(amountToAdd + amountToSubtract, result.GetAmount(0));
    }

    [TestMethod]
    public void ConnectTo_ConnectTwoContainers_ReturnsAmountSplitBetweenTheContainers()
    {
        const float amountToAdd = 12.0F;
        ContainerSystem containerSystem = new(2);
        ContainerSystem result = containerSystem.AddWater(0, amountToAdd).Connect(0, 1);

        Assert.AreEqual(amountToAdd / 2, result.GetAmount(0));
        Assert.AreEqual(amountToAdd / 2, result.GetAmount(1));
    }

    [TestMethod]
    public void ConnectTo_ConnectThreeContainers_ReturnsSameAmountInAllThree()
    {
        const float amountToAdd = 12.0F;
        ContainerSystem containerSystem = new(3);
        ContainerSystem result = containerSystem.AddWater(0, amountToAdd).Connect(0, 1).Connect(1, 2);

        Assert.AreEqual(amountToAdd / 3, result.GetAmount(0));
        Assert.AreEqual(amountToAdd / 3, result.GetAmount(1));
        Assert.AreEqual(amountToAdd / 3, result.GetAmount(2));
    }

    [TestMethod]
    public void ConnectTo_ConnectFourContainers_ReturnsSameAmountInAllFour()
    {
        const float amountToAddToContainer1 = 12.0F;
        const float amountToAddToContainer4 = 8.0F;
        const float totalAmount = amountToAddToContainer1 + amountToAddToContainer4;
        ContainerSystem containerSystem = new(4);
        ContainerSystem result = containerSystem.AddWater(0, amountToAddToContainer1)
                                                .AddWater(3, amountToAddToContainer4)
                                                .Connect(0, 1)
                                                .Connect(1, 2)
                                                .Connect(1, 3);

        Assert.AreEqual(totalAmount / 4, result.GetAmount(0));
        Assert.AreEqual(totalAmount / 4, result.GetAmount(1));
        Assert.AreEqual(totalAmount / 4, result.GetAmount(2));
        Assert.AreEqual(totalAmount / 4, result.GetAmount(3));
    }

    [TestMethod]
    public void UseCaseTest()
    {
        ContainerSystem containerSystem = new(4);

        ContainerSystem result = containerSystem.AddWater(0, 12).AddWater(3, 8).Connect(0, 1).Connect(1, 2);

        Assert.AreEqual(4, result.GetAmount(0));
        Assert.AreEqual(4, result.GetAmount(1));
        Assert.AreEqual(4, result.GetAmount(2));
        Assert.AreEqual(8, result.GetAmount(3));
    }
}
