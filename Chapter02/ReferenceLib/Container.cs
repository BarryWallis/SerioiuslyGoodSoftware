namespace ReferenceLib;

/// <summary>
/// A water container.
/// </summary>
public class Container : IContainer
{
    /// <summary>
    /// <see cref="Container"/>s connected to this one.
    /// </summary>
    private ISet<Container> _containers = new HashSet<Container>();

    /// <inheritdoc/>
    public double Amount { get; private set; }

    /// <summary>
    /// Initializes a new empty <see cref="Container"/>.
    /// </summary>
    public Container() => _ = _containers.Add(this); // Start with this cotainer.

    /// <inheritdoc/>
    public void ConnectTo(Container other)
    {
        // If the two Containers are already connected, don't do anything.
        if (_containers.SetEquals(other._containers))
        {
            return;
        }

        // Compute the new amount of water in each container
        int size1 = _containers.Count;
        int size2 = other._containers.Count;
        double total1 = Amount * size1;
        double total2 = other.Amount * size2;
        double newAmount = (total1 + total2) / (size1 + size2);

        // Merge the two Containers.
        _containers.UnionWith(other._containers);

        // Update the Containers
        foreach (Container container in other._containers)
        {
            container._containers = new HashSet<Container>(_containers);
        }

        // Update amount of all newly connected Containers.
        foreach (Container container1 in _containers)
        {
            container1.Amount = newAmount;
        }
    }

    /// <inheritdoc/>
    public void AddWater(double amount)
    {
        double amountPerContainer = amount / _containers.Count;
        foreach (Container container in _containers)
        {
            container.Amount += amountPerContainer;
        }
    }
}
