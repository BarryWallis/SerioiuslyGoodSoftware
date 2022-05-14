
namespace Speed3;

public record Container : IContainer
{
    //private static int _nextId = 0;
    //private readonly int _id; // Added to make identifying individual nodes easier for debugging
    private int _size = 1;
    private double _amount = 0.0;
    private Container _parent;

    private bool IsRoot() => _parent == this;

    public Container() =>
        // You can tell the root because it is its own parent.
        _parent = this;//_id = _nextId++;

    /// <inheritdoc/>
    public double Amount
    {
        get => FindRootAndCompress()._amount;
        private set => _amount = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Amount)) : value;
    }

    public int GroupSize
    {
        get
        {
            Container root = FindRootAndCompress();
            return root._size;
        }
    }


    /// <inheritdoc/>
    public void AddWater(double amount) => FindRootAndCompress().Amount += amount;

    /// <inheritdoc/>
    public void ConnectTo(Container other)
    {
        Container root = FindRootAndCompress();
        Container otherRoot = other.FindRootAndCompress();
        if (root == otherRoot)
        {
            return;
        }

        int size = root._size;
        int otherSize = otherRoot._size;
        double newAmount = ((root.Amount * size) + (other.Amount * otherSize)) / (size + otherSize);
        if (size < otherSize)
        {
            root._parent = otherRoot;
            otherRoot.Amount = newAmount;
            otherRoot._size += size;
        }
        else
        {
            otherRoot._parent = root;
            root.Amount = newAmount;
            root._size += otherSize;
        }
    }

    public void Flush()
    {
        Container root = FindRootAndCompress();
        root.Amount = 0;
    }

    /// <summary>
    /// Find the root of the tree and point all children to point directly to the root.
    /// </summary>
    /// <returns></returns>
    private Container FindRootAndCompress()
    {
        if (!_parent.IsRoot())
        {
            _parent = _parent.FindRootAndCompress();
        }

        return _parent;
    }
}
