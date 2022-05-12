namespace Speed1Lib;

public class Container
{
    private class Group
    {
        private static readonly int _lastId = 0;
        public readonly int _id;

        private double _amountPerContainer = 0;
        /// <inheritdoc/>
        public double AmountPerContainer
        {
            get => _amountPerContainer;
            set => _amountPerContainer =
                value < 0
                ? throw new ArgumentOutOfRangeException(nameof(AmountPerContainer), "Must be non-negative.")
                : value;
        }

        /// <summary>
        /// The set of all connected <see cref="Container"/>s.
        /// </summary>
        public ISet<Container> _members = new HashSet<Container>();

        public Group() => _id = Interlocked.Increment(ref _lastId);
    }

    private Group _group = new();

    public int GroupSize => _group._members.Count;

    /// <inheritdoc/>
    public double Amount
    {
        get
        {
            lock (_group)
            {
                return _group.AmountPerContainer;
            }
        }
    }

    /// <summary>
    /// Create a new <see cref="Container"/> instance.
    /// </summary>
    public Container() => _group._members.Add(this);

    /// <inheritdoc/>
    public void AddWater(double amount)
    {
        while (true)
        {
            object groupLock = _group;
            lock (groupLock)
            {
                if (groupLock == _group)
                {
                    double amountPerContainer = amount / _group._members.Count;
                    _group.AmountPerContainer += amountPerContainer;
                    return;
                }
            }
        }
    }

    /// <inheritdoc/>
    public void ConnectTo(Container other)
    {
        while (true)
        {
            if (_group == other._group)
            {
                return;
            }

            object firstLock = _group._id < other._group._id ? _group : other;
            object secondlock = _group._id > other._group._id ? _group : other._group;

            lock (firstLock)
            {
                lock (secondlock)
                {
                    if ((firstLock == _group && secondlock == other._group)
                        || (secondlock == _group && firstLock == other._group))
                    {
                        // Compute new amount of water container
                        int size1 = _group._members.Count;
                        int size2 = other._group._members.Count;
                        double total1 = _group.AmountPerContainer * size1;
                        double total2 = other._group.AmountPerContainer * size2;
                        _group.AmountPerContainer = (total1 + total2) / (size1 + size2);

                        // Merge two containers
                        _group._members.UnionWith(other._group._members);

                        foreach (Container container in other._group._members)
                        {
                            container._group = _group;
                        }

                        return;
                    }
                }
            }
        }
    }

    public void Flush()
    {
        foreach (Container container in _group._members)
        {
            container._group.AmountPerContainer = 0;
        }
    }
}
