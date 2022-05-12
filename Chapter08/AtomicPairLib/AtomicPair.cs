namespace AtomicPairLib;

/// <summary>
/// A thread-safe class to hold two objects.
/// </summary>
/// <typeparam name="TFirst">The type of the first object.</typeparam>
/// <typeparam name="TSecond">The type of the second object.</typeparam>
public class AtomicPair<TFirst, TSecond>
{
    private readonly object _firstLock = new();
    private readonly object _secondLock = new();

    private TFirst? _first;
    /// <value>The first object.</value>
    public TFirst? First
    {
        get => _first;
        private set
        {
            lock (_firstLock)
            {
                _first = value;
            }
        }
    }

    private TSecond? _second;
    /// <value>The second object.</value>
    public TSecond? Second
    {
        get => _second;
        private set
        {
            lock (_secondLock)
            {
                _second = value;
            }
        }
    }

    /// <summary>
    /// Set values for both objects.
    /// </summary>
    /// <param name="first">The first object's new value.</param>
    /// <param name="second">The second object's new value.</param>
    public void SetBoth(TFirst? first, TSecond? second)
    {
        lock (_firstLock)
        {
            lock (_secondLock)
            {
                _first = first;
                _second = second;
            }
        }
    }
}
