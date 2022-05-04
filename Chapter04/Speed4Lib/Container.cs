using System.Diagnostics;

namespace Speed4Lib;

public record Container : IContainer
{
    private static int _nextId = 0;
#pragma warning disable IDE0052 // Remove unread private members
    private readonly int _id; // Added to make identifying individual nodes easier for debugging
#pragma warning restore IDE0052 // Remove unread private members
    private Container _parent;
    private int _dataIndex;

    private bool IsRoot() => _parent == this;

    private struct Data
    {
        public int _size;
        public double _amount;
    }

    private static Data[] _dataItems = Array.Empty<Data>();

    public Container()
    {
        // You can tell the root because it is its own parent.
        _parent = this;
        _id = _nextId++;
        Array.Resize(ref _dataItems, _dataItems.Length + 1);
        Data data = new() { _size = 1, _amount = 0.0 };
        _dataItems[^1] = data;
        _dataIndex = _dataItems.Length - 1;
    }

    /// <inheritdoc/>
    public double Amount
    {
        get
        {
            Container root = FindRootAndCompress();
            Debug.Assert(root._dataIndex >= 0);
            return _dataItems[root._dataIndex]._amount;
        }

        private set
        {
            Container root = FindRootAndCompress();
            Debug.Assert(root._dataIndex >= 0);
            _dataItems[root._dataIndex]._amount = value;
        }
    }

    public int GroupSize => _dataItems[FindRootAndCompress()._dataIndex]._size;


    /// <inheritdoc/>
    public void AddWater(double amount) => Amount += amount;

    /// <inheritdoc/>
    public void ConnectTo(Container other)
    {
        Container root = FindRootAndCompress();
        Container otherRoot = other.FindRootAndCompress();
        if (root == otherRoot)
        {
            return;
        }

        int size = _dataItems[root._dataIndex]._size;
        int otherSize = _dataItems[otherRoot._dataIndex]._size;
        double newAmount = ((root.Amount * size) + (otherRoot.Amount * otherSize)) / (size + otherSize);
        if (size < otherSize)
        {
            root._parent = otherRoot;
            otherRoot.Amount = newAmount;
            _dataItems[otherRoot._dataIndex]._size += size;
            root._dataIndex = -1;
        }
        else
        {
            otherRoot._parent = root;
            root.Amount = newAmount;
            _dataItems[root._dataIndex]._size += otherSize;
            otherRoot._dataIndex = -1;
        }
    }

    public void Flush() => FindRootAndCompress().Amount = 0;

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
