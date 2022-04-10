using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReferenceTests;

[TestClass]
public class ExercisesTests
{
    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [ExpectedException(typeof(ArgumentException))]
    public void IdentityMatrix1_ZeroOrNegativeArgument_ThrowsArgumentException(int n) => _ = ReferenceLib.Exercises.IdentityMatrix1(n);

    [TestMethod]
    public void IdentityMatrix1_PositiveArgument_ReturnsIdentityMatrix()
    {
        const int n = 3;

        int[,] actual = ReferenceLib.Exercises.IdentityMatrix1(n);

        VerifyIdentityMatrix(n, actual);
    }
    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [ExpectedException(typeof(ArgumentException))]
    public void IdentityMatrix2_ZeroOrNegativeArgument_ThrowsArgumentException(int n) => _ = ReferenceLib.Exercises.IdentityMatrix2(n);

    [TestMethod]
    public void IdentityMatrix2_PositiveArgument_ReturnsIdentityMatrix()
    {
        const int n = 3;

        int[,] actual = ReferenceLib.Exercises.IdentityMatrix2(n);

        VerifyIdentityMatrix(n, actual);
    }

    private static void VerifyIdentityMatrix(int n, int[,] actual)
    {
        Assert.IsTrue(n > 0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    Assert.AreEqual(1, actual[i, j]);
                }
                else
                {
                    Assert.AreEqual(0, actual[i, j]);
                }
            }
        }
    }
}
