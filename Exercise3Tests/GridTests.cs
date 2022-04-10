using System;

using Exercise3Lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise3Tests;

[TestClass]
public class GridTests
{
    [TestMethod]
    public void Ctor_CreateGridWithPower_GridHasGivenPower()
    {
        uint expected = 3000;

        Grid grid = new(expected);
        ulong actual = grid.ResidualPower;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ResidualPower_CreateGridWithPower_ReturnsPowerGridWasCreatedWith()
    {
        uint expected = 3000;
        Grid grid = new(expected);

        uint actual = grid.ResidualPower;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ApplianceIsOnGrid_ApplianceAddedToGrid_ReturnsTrue()
    {
        Grid grid = new(100);
        Appliance appliance = new(10);

        appliance.PlugInto(grid);
        bool actual = grid.ApplianceIsOnGrid(appliance);

        Assert.IsTrue(actual);
    }

}
