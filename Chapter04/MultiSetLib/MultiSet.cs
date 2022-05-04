namespace MultiSetLib;

/// <summary>
/// A set that can hold duplicate copies of an element.
/// </summary>
/// <remarks>
/// This is a space-efficient implementation.
/// </remarks>
/// <typeparam name="T">The type of elements in the <see cref="MultiSet{T}"/>.</typeparam>
public class MultiSet<T> where T : IEquatable<T>
{
    private readonly IList<T> _elements = new List<T>();

    /// <summary>
    /// Insert <paramref name="element"/> to the <see cref="MultiSet{T}"/>.
    /// </summary>
    /// <param name="element">The element to add.</param>
    public void Add(T element) => _elements.Add(element);

    /// <summary>
    /// Return the number of occurerences of <paramref name="element"/> in the <see cref="MultiSet{T}"/>.
    /// </summary>
    /// <param name="element">The <paramref name="element"/> to add.</param>
    /// <returns>The number of occurrences of the element.</returns>
    public long Count(T element) => _elements.Where(e => e.Equals(element)).Count();
}
