
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Memory4Lib.Container;

namespace Memory4Tests;

[TestClass]
public class ContainerTests
{
    [TestMethod]
    public void Ctor_Default_ReturnsZero()
    {
        int container = NewContainer();

        float actual = GetAmount(container);

        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void GetAmount_DefaultContainer_ReturnsZero()
    {
        int container = NewContainer();

        float actual = GetAmount(container);

        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void AddWater_AddWaterToOneContainer_ReturnsTheAmountAdded()
    {
        const float amountToAdd = 5.5F;
        int container = NewContainer();

        AddWater(container, amountToAdd);

        Assert.AreEqual(amountToAdd, GetAmount(container));
    }

    [TestMethod]
    public void AddWater_AddNegativeAmountOfWater_ReturnsTheAmountAddedMinusTheNegativeAmount()
    {
        const float amountToAdd = 5.5F;
        const float amountToSubtract = -.5F;
        int container = NewContainer();

        AddWater(container, amountToAdd);
        AddWater(container, amountToSubtract);

        Assert.AreEqual(amountToAdd + amountToSubtract, GetAmount(container));
    }

    [TestMethod]
    public void ConnectTo_ConnectTwoContainers_ReturnsAmountSplitBetweenTheContainers()
    {
        const float amountToAdd = 12.0F;
        int container1 = NewContainer();
        int container2 = NewContainer();
        AddWater(container1, amountToAdd);

        Connect(container1, container2);

        Assert.AreEqual(amountToAdd / 2, GetAmount(container1));
        Assert.AreEqual(amountToAdd / 2, GetAmount(container2));
    }

    [TestMethod]
    public void ConnectTo_ConnectThreeContainers_ReturnsSameAmountInAllThree()
    {
        const float amountToAdd = 12.0F;
        int container1 = NewContainer();
        int container2 = NewContainer();
        int container3 = NewContainer();
        AddWater(container1, amountToAdd);

        Connect(container1, container2);
        Connect(container2, container3);

        Assert.AreEqual(amountToAdd / 3, GetAmount(container1));
        Assert.AreEqual(amountToAdd / 3, GetAmount(container2));
        Assert.AreEqual(amountToAdd / 3, GetAmount(container3));
    }

    [TestMethod]
    public void ConnectTo_ConnectFourContainers_ReturnsSameAmountInAllFour()
    {
        const float amountToAddToContainer1 = 12.0F;
        const float amountToAddToContainer4 = 8.0F;
        const float totalAmount = amountToAddToContainer1 + amountToAddToContainer4;
        int container1 = NewContainer();
        int container2 = NewContainer();
        int container3 = NewContainer();
        int container4 = NewContainer();
        AddWater(container1, amountToAddToContainer1);
        AddWater(container4, amountToAddToContainer4);

        Connect(container1, container2);
        Connect(container2, container3);
        Connect(container2, container4);

        Assert.AreEqual(totalAmount / 4, GetAmount(container1));
        Assert.AreEqual(totalAmount / 4, GetAmount(container2));
        Assert.AreEqual(totalAmount / 4, GetAmount(container3));
        Assert.AreEqual(totalAmount / 4, GetAmount(container4));
    }

    [TestMethod]
    public void UseCaseTest()
    {
        int containerA = NewContainer();
        int containerB = NewContainer();
        int containerC = NewContainer();
        int containerD = NewContainer();

        AddWater(containerA, 12);
        AddWater(containerD, 8);
        Connect(containerA, containerB);
        Connect(containerB, containerC);

        Assert.AreEqual(4, GetAmount(containerA));
        Assert.AreEqual(4, GetAmount(containerB));
        Assert.AreEqual(4, GetAmount(containerC));
        Assert.AreEqual(8, GetAmount(containerD));
    }
}
