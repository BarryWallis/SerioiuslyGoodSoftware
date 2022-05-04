using System.Text.RegularExpressions;

namespace Memory1Lib;

public class Container : IContainer
{
    private List<Container>? _group = null;

    private float _amount = 0.0F;
    /// <inheritdoc/>
    public float Amount
    {
        get => _amount;
        private set => _amount = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Amount)) : value;
    }

    /// <summary>
    /// The size of the group this <see cref="Container"/> belongs to.
    /// </summary>
    public int GroupSize => _group?.Count ?? 1;

    /// <inheritdoc/>
    public void AddWater(float amount)
    {
        if (_group is null)
        {
            Amount += amount;
        }
        else
        {
            float amountPerContainer = amount / _group.Count;
            foreach (Container container in _group)
            {
                container.Amount = amountPerContainer;
            }
        }
    }

    /// <inheritdoc/>
    public void ConnectTo(Container other)
    {
        _group ??= InitializeGroup(this);
        other._group ??= InitializeGroup(other);
        if (_group == other._group)
        {
            return;
        }

        int size = _group.Count;
        int otherSize = other._group.Count;
        float total = Amount * size;
        float otherTotal = other.Amount * otherSize;
        float newAmount = (total + otherTotal) / (size + otherSize);

        _group.AddRange(other._group);

        foreach (Container container in other._group)
        {
            container._group = _group;
        }

        foreach (Container container1 in _group)
        {
            container1.Amount = newAmount;
        }
    }

    /// <summary>
    /// Initialize the group via lizay initialization.
    /// </summary>
    /// <param name="group">The <see cref="Group"/> to initialize.</param>
    private static List<Container> InitializeGroup(Container container) => new() { container };
}
