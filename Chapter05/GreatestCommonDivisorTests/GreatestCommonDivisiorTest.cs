using System;

using GreatestCommonDivisorLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreatestCommonDivisorTests;

[TestClass]
public class GreatestCommonDivisiorTest
{
    [TestMethod]
    [DynamicData(nameof(GetGCDData), DynamicDataSourceType.Method)]
    public void GCDTest(int data1, int data2) => _ = GreatestCommonDivisorClass.GreatestCommonDivisor(data1, data2);

    private static System.Collections.Generic.IEnumerable<object[]> GetGCDData()
    {
        Random random = new();
        for (int i = 0; i < 1000; i++)
        {
            yield return new GetGCDDataType
            {
                data1 = random.Next(1000),
                data2 = random.Next(1000)
            }.ToObjectArray();
        }
    }

    private struct GetGCDDataType
    {
        public int data1 { get; set; }
        public int data2 { get; set; }

        public object[] ToObjectArray() => new object[] { data1, data2 };
    }
}
