using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exercise3Lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise3Tests;
[TestClass]
public class UseCaseTests
{
    [TestMethod]
    public void UseCaseTest()
    {
        Appliance tv = new(150);
        Appliance radio = new(30);
        Grid grid = new(3_000);

        tv.PlugInto(grid);
        radio.PlugInto(grid);
        Assert.AreEqual((uint)3_000, grid.ResidualPower);

        tv.On();
        Assert.AreEqual((uint)2850, grid.ResidualPower);

        radio.On();
        Assert.AreEqual((uint)2820, grid.ResidualPower);
    }
}
