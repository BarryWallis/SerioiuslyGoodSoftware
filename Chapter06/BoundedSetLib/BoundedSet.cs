namespace BoundedSetLib;

/// <summary>
/// A set with a fixed-maximum capacity that keeps track of the order its elements are inserted. Once the capacity is reached, 
/// the oldest element is removed before the new element is added (so the capacity is never exceeded). If a duplicate
/// element is inserted, that element is set as though it were a new element.
/// </summary>
/// <typeparam name="T">The type of elements in the set.</typeparam>
public class BoundedSet<T> where T : IEquatable<T>
{
    private readonly LinkedList<T> _list;

    /// <summary>
    /// The maximum size of the <see cref="BoundedSet{T}"/>.
    /// </summary>
    public int Capacity { get; init; }

    /// <summary>
    /// A read only copy of the list.
    /// </summary>
    public IReadOnlyCollection<T> Content => _list.ToList().AsReadOnly();

    /// <summary>
    /// Create a new <see cref="BoundedSet{T}"/> with the given cpacity.
    /// </summary>
    /// <param name="capacity"></param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> must be positive.</exception>
    public BoundedSet(int capacity)
    {
        _list = new();
        Capacity = capacity <= 0 ? throw new ArgumentOutOfRangeException(nameof(capacity), "Must be positive.") : capacity;
    }

    /// <summary>
    /// Copy the given <see cref="BoundedSet{T}"/>.
    /// </summary>
    /// <param name="other">The <see cref="BoundedSet{T}"/> to copy.</param>
    public BoundedSet(BoundedSet<T> other)
    {
        _list = new(other._list);
        Capacity = other.Capacity;
    }

    /// <summary>
    /// Add the given <paramref name="element"/>, If the addition brings the size beyond capacity, remove 
    /// the oldest <paramref name="element"/>. If the given <paramref name="element"/>/> is already present, 
    /// make the <paramref name="element"/> the newest one.
    /// </summary>
    /// <param name="element">The element to add.</param>
    /// <returns>
    /// isRemoved: <see langword="true"/> if an element was removed due to the sie being at capacity; 
    /// othrwise <see langword="false"/>.
    /// <para> 
    /// removedElement: The element that was removed (only valid if isRemoved is <see langword="true"/>).
    /// </para>
    /// </returns>
    public (bool isRemoved, T? removedElement) Add(T element)
    {
#if DEBUG
        BoundedSet<T> copy = new(this);
#endif
        bool isRemoved = false;
        T? removedElement = default;
        _ = _list.Remove(element);
        if (_list.Count >= Capacity)
        {
            isRemoved = true;
            Debug.Assert(_list.Count > 0);
            removedElement = _list.First!.Value;
            _list.RemoveFirst();
        }

        _ = _list.AddLast(element);

        Debug.Assert(PostconditionAdd(copy, element));
        Debug.Assert(CheckInvariants());

        return (isRemoved, removedElement);
    }

    /// <summary>
    /// Verify the following postconditions:
    /// 1) <paramref name="newElement"/> must be at the front of the list.
    /// 2) Remove the <paramref name="newElement"/> from both old and new lists. If the old list was full, drop the oldest 
    /// element. Verify that all remaining objects should be the same in the same order.
    /// </summary>
    /// <param name="oldSet"></param>
    /// <param name="newElement"></param>
    /// <returns></returns>
    private bool PostconditionAdd(BoundedSet<T> oldSet, T newElement)
    {
        if (_list.Last is null)
        {
            throw new InvalidOperationException($"{nameof(_list)} is empty.");
        }

        if (!_list.Last.Value.Equals(newElement))
        {
            return false;
        }

        IList<T> copyOfCurrentList = new List<T>(_list);
        bool wasRemoveSuccessful = copyOfCurrentList.Remove(newElement);
        Debug.Assert(wasRemoveSuccessful);
        _ = oldSet._list.Remove(newElement);
        if (oldSet._list.Count == Capacity)
        {
            oldSet._list.RemoveFirst();
        }

        return Enumerable.SequenceEqual(oldSet._list, copyOfCurrentList);
    }

    /// <summary>
    /// Does the <see cref="BoundedSet{T}"/> contain the given <paramref name="element"/>?
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns><see langword="true"/> if the <see cref="BoundedSet{T}"/> contain the given <paramref name="element"/>; 
    /// otherwise <see langword="false"/>.</returns>
    public bool Contains(T element) => _list.Contains(element);

    /// <summary>
    /// Check that the invariants still hold:
    /// 1) The length of the list should never be more than the capacity.
    /// 2) The list should contain no duplicates.
    /// </summary>
    /// <returns><see langword="true"/> if the invariants hold; otherwise <see langword="false"/>.</returns>
    private bool CheckInvariants()
    {
        if (_list.Count > Capacity)
        {
            return false;
        }

        ISet<T> eleements = new HashSet<T>();
        foreach (T element in _list)
        {
            if (!eleements.Add(element))
            {
                return false;
            }
        }

        return true;
    }
}
