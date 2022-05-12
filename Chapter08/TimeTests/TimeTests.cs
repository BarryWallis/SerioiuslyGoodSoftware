using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TimeLib;

namespace TimeTests;

[TestClass]
public class TimeTests
{
    [TestMethod]
    [DataRow(-1, 0, 0)]
    [DataRow(24, 0, 0)]
    [DataRow(23, -1, 0)]
    [DataRow(23, 60, 0)]
    [DataRow(0, 0, -1)]
    [DataRow(0, 59, 60)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Ctor_IllegalArgument_ThrowArgumentOutOfRangeException(int hours, int minutes, int seconds)
    {
        Time _ = new(hours, minutes, seconds);
    }

    [TestMethod]
    public void Ctor_LegalArguments_ValidTime()
    {
        const int hours = 1;
        const int minutes = 2;
        const int seconds = 3;

        Time actual = new(hours, minutes, seconds);

        Assert.AreEqual(hours, actual.Hours);
        Assert.AreEqual(minutes, actual.Minutes);
        Assert.AreEqual(seconds, actual.Seconds);
    }

    [TestMethod]
    public void AddNoWrapping_NoWrapping_AddsDeltaTimeStopsBeforeMidnight()
    {
        const int hours = 1;
        const int minutes = 2;
        const int seconds = 3;
        Time time = new(hours, minutes, seconds);
        const int delta = 10;
        Time deltaTime = new(delta, delta, delta);

        Time actual = time.AddNoWrapping(deltaTime);

        Assert.AreEqual(new Time(hours + delta, minutes + delta, seconds + delta), actual);
    }

    [TestMethod]
    public void AddAndWrapAround_Wrapping_AddsDeltaTimeWrapsPastMidnight()
    {
        const int hours = 23;
        const int minutes = 59;
        const int seconds = 50;
        Time time = new(hours, minutes, seconds);
        Time deltaTime = new(0, 0, 10);

        Time actual = time.AddAndWrapAround(deltaTime);

        Assert.AreEqual(new Time(0, 0, 0), actual);
    }

    [TestMethod]
    public void SubtractNoWrapping_NoWrapping_SubtractsDeltaTimeStopsAtMidnight()
    {
        const int hours = 1;
        const int minutes = 2;
        const int seconds = 3;
        Time time = new(hours, minutes, seconds);
        Time deltaTime = new(1, 2, 4);

        Time actual = time.SubtractNoWrapping(deltaTime);

        Assert.AreEqual(new(0, 0, 0), actual);
    }

    [TestMethod]
    public void SubtractAndWrpAround_NoWrapping_SubtractsDeltaTimeWrpasAroundPastMidnight()
    {
        const int hours = 1;
        const int minutes = 2;
        const int seconds = 3;
        Time time = new(hours, minutes, seconds);
        Time deltaTime = new(1, 2, 4);

        Time actual = time.SubtractAndWrapAround(deltaTime);

        Assert.AreEqual(new(23, 59, 59), actual);
    }
}
