namespace ReferenceLib;

/// <summary>
/// A water container.
/// </summary>
public class Container
{
    /// <value>
    /// <see cref="Container"/>s connected to this one.
    /// </value>
    private ISet<Container> _group = new HashSet<Container>();

    /// <value>
    /// The water amount in this container.
    /// </value>
    public double Amount { get; private set; }

    /// <summary>
    /// Initializes a new empty <see cref="Container"/>.
    /// </summary>
    public Container() => _ = _group.Add(this); // Start with this cotainer.

    /// <summary>
    /// Connect this <see cref="Container"/> to the given <see cref="Container"/>.
    /// </summary>
    /// <param name="container">The <see cref="Container"/> to connect to this one.</param>
    public void ConnectTo(Container other)
    {
        // If the two Containers are already connected, don't do anything.
        if (_group.SetEquals(other._group))
        {
            return;
        }

        // Compute the new amount of water in each container
        int size1 = _group.Count;
        int size2 = other._group.Count;
        double total1 = Amount * size1;
        double total2 = other.Amount * size2;
        double newAmount = (total1 + total2) / (size1 + size2);

        // Merge the two Containers.
        _group.UnionWith(other._group);

        // Update the Containers
        foreach (Container container in other._group)
        {
            //container._group = new HashSet<Container>(_group);
            container._group = _group;
        }

        // Update amount of all newly connected Containers.
        foreach (Container container1 in _group)
        {
            container1.Amount = newAmount;
        }
    }

    /// <summary>
    /// Add water to the container.
    /// </summary>
    /// <param name="amount">The amount of water to add.</param>
    public void AddWater(double amount)
    {
        double amountPerContainer = amount / _group.Count;
        foreach (Container container in _group)
        {
            container.Amount += amountPerContainer;
        }
    }
}
