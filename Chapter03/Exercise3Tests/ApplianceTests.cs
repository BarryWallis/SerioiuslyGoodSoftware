using System;

using Exercise3Lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise3Tests;
[TestClass]
public class ApplianceTests
{
    [TestMethod]
    [DataRow((uint)3_000)]
    [DataRow((uint)0)]
    public void Ctor_CreateApplianceWithPower_PowerIsCorrectAndStatusIsOff(uint power)
    {
        uint expected = power;

        Appliance appliance = new(expected);
        uint actual = appliance.Power;

        Assert.AreEqual(expected, actual);
        Assert.IsTrue(appliance.IsOff);
    }

    [TestMethod]
    public void On_TurnApplianceOn_ApplianceIsOn()
    {
        Appliance appliance = new(100);

        appliance.On();

        Assert.IsTrue(appliance.IsOn);
    }

    [TestMethod]
    public void Off_TurnApplianceOff_ApplianceIsOff()
    {
        Appliance appliance = new(100);

        appliance.Off();

        Assert.IsTrue(appliance.IsOff);
    }

    [TestMethod]
    public void IsOff_ApplianceIsOffAndApplianceIsNotOn_ReturnsTrue()
    {
        Appliance appliance = new(100);

        Assert.IsTrue(appliance.IsOff && !appliance.IsOn);
    }

    [TestMethod]
    public void IsOn_ApplianceIsOnAndApplianceIsNotOff_ReturnsTrue()
    {
        Appliance appliance = new(100);

        appliance.On();

        Assert.IsTrue(appliance.IsOn && !appliance.IsOff);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void PlugInto_ApplianceIsOnAndDrawsTooMuchPowerFromGrid_ThrowInvalidOperationException()
    {
        Grid grid = new(100);

        Appliance appliance = new(200);
        appliance.On();
        appliance.PlugInto(grid);
    }

    [TestMethod]
    public void PlugIntoAppliance_ApplianceIsOffAndDrawsTooMuchPowerFromGrid_ApplianceSuccessfullyAddedToGrid()
    {
        uint expected = 100;
        Grid grid = new(expected);

        Appliance appliance = new(200);
        appliance.Off();
        appliance.PlugInto(grid);
        bool actual = grid.ApplianceIsOnGrid(appliance);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void PlugInto_ApplianceIsOnAndDoesNotDrawTooMuchPowerFromGrid_ApplianceSuccessfullyAddedToGrid()
    {
        Grid grid = new(300);

        Appliance appliance = new(200);
        appliance.On();
        appliance.PlugInto(grid);
        bool actual = grid.ApplianceIsOnGrid(appliance);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void PlugInto_ApplianceIsOffAndDrawsTooMuchPowerFromGrid_ApplianceSuccessfullyAddedToGrid()
    {
        Grid grid = new(100);

        Appliance appliance = new(200);
        appliance.Off();
        appliance.PlugInto(grid);
        bool actual = grid.ApplianceIsOnGrid(appliance);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void PlugInto_GridIsNull_AddApplianceToGrid()
    {
        Grid grid = new(300);
        Appliance appliance = new(100);

        appliance.PlugInto(grid);

        Assert.IsTrue(grid.ApplianceIsOnGrid(appliance));
    }

    [TestMethod]
    public void PlugInto_GridIsNotNull_RemoveApplianceFromCurrentGridAndAddApplianceToNewGrid()
    {
        Grid grid1 = new(300);
        Grid grid2 = new(200);
        Appliance appliance = new(100);
        appliance.PlugInto(grid1);

        appliance.PlugInto(grid2);

        Assert.IsFalse(grid1.ApplianceIsOnGrid(appliance));
        Assert.IsTrue(grid2.ApplianceIsOnGrid(appliance));
    }
}
