using System.Diagnostics;
using System.Numerics;

namespace GreatestCommonDivisorLib;

public static class GreatestCommonDivisorClass
{
    public static int GreatestCommonDivisor(int u, int v)
    {
        const string OverflowErrorMessage = "overflow: gcd is 2^31";

        if (u == 0 || v == 0)
        {
            return u == int.MinValue || v == int.MinValue
                   ? throw new ArithmeticException(OverflowErrorMessage)
                   : Math.Abs(u) + Math.Abs(v);
        }

#if DEBUG
        int originalU = u;
        int originalV = v;
#endif

        if (Math.Abs(u) == 1 || Math.Abs(v) == 1)
        {
            return 1;
        }

        u = u > 0 ? -u : u;
        v = v > 0 ? -v : v;
        int k = 0;
        while ((u & 1) == 0 && (v & 1) == 0 && k < 31)
        {
            u /= 2;
            v /= 2;
            k += 1;
        }

        if (k == 31)
        {
            throw new ArithmeticException(OverflowErrorMessage);
        }

        int t = (u & 1) == 1 ? v : -(u / 2);
        do
        {
            while ((t & 1) == 0)
            {
                t /= 2;
            }

            if (t > 0)
            {
                u = -t;
            }
            else
            {
                v = t;
            }

            t = (v - u) / 2;
        } while (t != 0);

        int result = -u * (1 << k);

        Debug.Assert(PostconditionGreatestCommonDivisor(originalU, originalV, result));
        return result;
    }

    private static bool PostconditionGreatestCommonDivisor(int originalU, int originalV, int result)
        => BigInteger.GreatestCommonDivisor(originalU, originalV) == result;
}
