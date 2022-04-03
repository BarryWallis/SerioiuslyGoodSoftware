namespace Reference;
public static class Exercises
{
    public static int[,] IdentityMatrix1(int n)
    {
        if (n <= 0)
        {
            throw new ArgumentException(null, nameof(n));
        }

        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    matrix[i, j] = 1;
                }
            }
        }

        return matrix;
    }

    public static int[,] IdentityMatrix2(int n)
    {
        if (n <= 0)
        {
            throw new ArgumentException(null, nameof(n));
        }

        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            matrix[i, i] = 1;
        }

        return matrix;
    }
}
