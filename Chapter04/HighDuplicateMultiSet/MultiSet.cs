using System.Diagnostics;

namespace HighDuplicateMultiSetLib;

/// <summary>
/// A set that can hold duplicate copies of an element.
/// </summary>
/// <remarks>
/// This is a space-efficient implementation that is most efficient when the number of duplicates is at least seven.
/// </remarks>
/// <typeparam name="T">The type of elements in the <see cref="MultiSet{T}"/>.</typeparam>
public class MultiSet<T>
{
    private readonly List<T> _elements = new();
    private readonly List<long> _repetitions = new();


    /// <summary>
    /// Insert <paramref name="element"/> to the <see cref="MultiSet{T}"/>.
    /// </summary>
    /// <param name="element">The element to add.</param>
    public void Add(T element)
    {
        int index = _elements.IndexOf(element);
        if (index == -1)
        {
            _elements.Add(element);
            _repetitions.Add(1L);
        }
        else if (index >= 0)
        {
            _repetitions[index] += 1;
        }
        else
        {
            Debug.Fail($"IndexOf returned invalid result: {index}");
        }
    }

    /// <summary>
    /// Return the number of occurerences of <paramref name="element"/> in the <see cref="MultiSet{T}"/>.
    /// </summary>
    /// <param name="element">The <paramref name="element"/> to add.</param>
    /// <returns>The number of occurrences of the element.</returns>
    public long Count(T element)
    {
        if (_elements.Any())
        {
            int index = _elements.Select((e, i) => e!.Equals(element) ? i : -1).Max();
            return index >= 0 ? _repetitions[index] : 0;
        }
        else
        {
            return 0;
        }
    }
}
