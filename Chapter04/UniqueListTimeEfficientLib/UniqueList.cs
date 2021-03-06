namespace UniqueListTimeEfficientLib;

/// <summary>
/// A fixed-sized zero-based indexed list without duplicates.
/// </summary>
/// <remarks>
/// This version is implemented to be space-efficient.
/// </remarks>
public class UniqueList<T>
{
    //private class DataEqualityComprarer : IEqualityComparer<Data>
    //{
    //    public bool Equals(UniqueList<T>.Data? x, UniqueList<T>.Data? y)
    //        => (x is null && y is null) || (x is not null && y is not null && x.Index == y.Index);

    //    public int GetHashCode([DisallowNull] UniqueList<T>.Data obj) => obj.Index.GetHashCode;
    //}

    private readonly Dictionary<int, T?> _elements;
    private readonly Dictionary<int, bool> _isPresent;

    /// <summary>
    /// Create an empty <see cref="UniqueList"/> with the given capacity.
    /// </summary>
    /// <remarks>
    /// The list cannot be resized.
    /// </remarks>
    /// <param name="capacity">The list capacity.</param>
    public UniqueList(int capacity)
    {
        _elements = new(capacity);
        _isPresent = new(capacity);
        for (int i = 0; i < capacity; i++)
        {
            _elements[i] = default;
            _isPresent[i] = false;
        }
    }

    /// <summary>
    /// Insert the <paramref name="element"/> at the given <paramref name="index"/>. 
    /// </summary>
    /// <param name="index">The zero-based index.</param>
    /// <param name="element">The element to add.</param>
    /// <returns>
    /// <see langword="true"/> if the index is within the range 0 and capacity -1 and the element is not already 
    /// present; otherwise return <see langword="false"/> and the list is unchanged.
    /// </returns>
    public bool Set(int index, T element)
    {
        if (index < 0
            || index >= _elements.Count
            || _isPresent[index])
        {
            return false;
        }

        _elements[index] = element;
        _isPresent[index] = true;
        return true;
    }

    /// <summary>
    /// Return the element at the given <paramref name="index"/> or <see langword="null"/> if the <paramref name="index"/> is 
    /// invalid or unassigned.
    /// </summary>
    /// <param name="index">The index for the element to return.</param>
    /// <returns>
    /// The element at the given index or <see langword="null"/> if the element is not in the list or the index is invalid or 
    /// unassigned.
    /// </returns>
    public (bool success, T? element) Get(int index)
        => index < 0 || index >= _elements.Count || !_isPresent[index]
            ? (false, default(T))
            : (true, _elements[index]);
}
