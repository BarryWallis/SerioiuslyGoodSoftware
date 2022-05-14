using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GenericLib;
public class UnionFindNode<TValue, TSummary> : IContainer<TValue, UnionFindNode<TValue, TSummary>>
{
    private UnionFindNode<TValue, TSummary> _parent;
    private int _groupSize = 1;
    private readonly IAttribute<TValue, TSummary> _attribute;
    private TSummary _summary;

    public UnionFindNode(IAttribute<TValue, TSummary> dom)
    {
        _parent = this;
        _attribute = dom;
        _summary = dom.Seed;
    }
    
    /// <inheritdoc/>
    public TValue Value 
    { 
        get
        {
            UnionFindNode<TValue, TSummary> root = FindRootAndCompress();
            return _attribute.Report(root._summary);
        }

        set
        {
            UnionFindNode<TValue, TSummary> root = FindRootAndCompress();
            _attribute.Update(root._summary, value);
        } 
    }

    private UnionFindNode<TValue, TSummary> FindRootAndCompress()
    {
        if (!_parent.IsRoot())
        {
            _parent = _parent.FindRootAndCompress();
        }

        return _parent;

    }

    private bool IsRoot() => _parent == this;

    /// <inheritdoc/>
    public void ConnectTo(UnionFindNode<TValue, TSummary> other)
    {
        UnionFindNode<TValue, TSummary> root1 = FindRootAndCompress();
        UnionFindNode<TValue, TSummary> root2 = other.FindRootAndCompress();
        if (root1 == root2)
        {
            return;
        }

        int size1 = root1._groupSize;
        int size2 = root2._groupSize;
        TSummary newSummary = _attribute.Merge(root1._summary, root2._summary);

        if (size1 <= size2)
        {
            root1._parent = root2;
            root2._summary = newSummary;
            root2._groupSize += size1;
        }
        else
        {
            root2._parent = root1;
            root1._summary = newSummary;
            root1._groupSize += size2;
        }
    }
}
