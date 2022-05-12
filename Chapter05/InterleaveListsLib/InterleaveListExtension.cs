namespace InterleaveListLib;

/// <summary>
/// Extension to inteleave two lists (e.g., create a new list with elements alternating from each source list).
/// </summary>
public static class InterleaveListExtension
{
    /// <summary>
    /// Extension to inteleave two lists (e.g., create a new list with elements alternating from each source list).
    /// </summary>
    /// <typeparam name="T">The type of object in the lists.</typeparam>
    /// <param name="first">The first list.</param>
    /// <param name="second">The second list</param>
    /// <returns>A new list with the two list elements interleaved.
    /// <exception cref="ArgumentException">Both lists must be the same length.</exception>
    public static IList<T> InterleaveList<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second)
    {
        if (first.Count != second.Count)
        {
            throw new ArgumentException("Both lists must be the same length.", $"{nameof(first)} and {nameof(second)}");
        }

        IList<T> result = new List<T>();
        for (int i = 0; i < first.Count; i++)
        {
            result.Add(first[i]);
            result.Add(second[i]);
        }

        Debug.Assert(PostconditionInterleaveList(first, second, result));

        return result;
    }

    /// <summary>
    /// Check the postconditions for <see cref="InterleaveList{T}(IReadOnlyList{T}, IReadOnlyList{T})"/>.
    /// </summary>
    /// <returns><see langword="true"/> if the postconditions are met; otherwise <see langword="false"/>.</returns>
    private static bool PostconditionInterleaveList<T>(IReadOnlyList<T> first, IReadOnlyList<T> second, IList<T> result)
    {
        IReadOnlyList<T> expected = first.Zip(second)
                                         .SelectMany(x => new T[] { x.First, x.Second })
                                         .Distinct()
                                         .ToList();
        return expected.SequenceEqual(result);
    }
}
