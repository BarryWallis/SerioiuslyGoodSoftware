using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntStatsLib;

/// <summary>
/// Report statstics on a list of integers.
/// </summary>
public class IntStats
{
    private readonly List<int> _numbers = new();

    /// <summary>
    /// Return the average of the list.
    /// </summary>
    public double Average => Sum / (double)_numbers.Count;

    /// <summary>
    /// Return the sum of the list.
    /// </summary>
    public long Sum { get; private set; } = 0L;

    /// <summary>
    /// Return the median of the list.
    /// </summary>
    public double Median
    {
        get
        {
            _numbers.Sort();
            int count = _numbers.Count;
            return count % 2 == 1 ? _numbers[count / 2] : (_numbers[(count / 2) - 1] + _numbers[count / 2]) / 2.0;
        }
    }

    /// <summary>
    /// Add a number to the list.
    /// </summary>
    /// <param name="n">The number to add.</param>
    public void Insert(int n)
    {
        _numbers.Add(n);
        Sum += n;
    }
}
