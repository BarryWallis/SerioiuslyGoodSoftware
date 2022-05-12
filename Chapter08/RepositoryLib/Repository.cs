namespace RepositoryLib;

/// <summary>
/// A thread-safe repository of <typeparamref name="T"/> items.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Repository<T>
{
    private readonly IList<T?> _items;
    private readonly IList<object> _locks;

    /// <summary>
    /// Create a repository with <paramref name="n"/> cells. Initially all cells hold the default value.
    /// </summary>
    /// <param name="n">The number of cells in the respository.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="n"/> must be positive.</exception>
    public Repository(int n)
    {
        if (n <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Must be positive");
        }

        _items = new List<T?>(n);
        _locks = new List<object>(n);

        for (int i = 0; i < n; i++)
        {
            _items.Add(default);
            _locks.Add(new object());
        }
    }

    /// <summary>
    /// Insert an <paramref name="item"/> int the <paramref name="i"/>th cell and return the previous contents 
    /// of the cell. Cell numbering starts with zero.
    /// </summary>
    /// <param name="i">The cell to insert the <paramref name="item"/> into.</param>
    /// <param name="item">The item to insert into the cell.</param>
    /// <returns>The previous value of cell <paramref name="i"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="i"/> must be non-negative and less than the number of cells in the repository. 
    /// </exception>
    public T? Set(int i, T? item)
    {
        if (i < 0 || i >= _items.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(i),
                "Must be non-negative and less than the number of cells in the repository");
        }

        lock (_locks[i])
        {
            T? result = _items[i];
            _items[i] = item;
            return result;
        }
    }

    /// <summary>
    /// Return the contents of cell <paramref name="i"/>.
    /// </summary>
    /// <param name="i">The cell to return the contents of.</param>
    /// <returns>The contents of cell <paramref name="i"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="i"/> must be non-negative and less than the number of cells in the repository. 
    /// </exception>
    public T? Get(int i)
    {
        if (i < 0 || i >= _items.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(i),
                "Must be non-negative and less than the number of cells in the repository");
        }

        lock (_locks[i])
        {
            return _items[i];
        }
    }

    /// <summary>
    /// Swap the contents of cell <paramref name="i"/> and cell <paramref name="j"/>. Cell numbering starts 
    /// with zero.
    /// </summary>
    /// <param name="i">The first cell to swap.</param>
    /// <param name="j">The second cell to swap.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="i"/> and <paramref name="j"/> must be non-negative and less than the number of cells 
    /// in the repository. 
    /// </exception>
    public void Swap(int i, int j)
    {
        if (i < 0 || i >= _items.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(i),
                "Must be non-negative and less than the number of cells in the repository");
        }
        if (j < 0 || j >= _items.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(j),
                "Must be non-negative and less than the number of cells in the repository");
        }

        if (i == j)
        {
            return;
        }


        if (i > j)
        {
            (j, i) = (i, j);
        }

        lock (_locks[i])
        {
            lock (_locks[j])
            {
                (_items[j], _items[i]) = (_items[i], _items[j]);
            }
        }
    }
}
