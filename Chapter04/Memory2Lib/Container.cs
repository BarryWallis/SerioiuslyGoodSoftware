namespace Memory2Lib;

public class Container : IContainer
{
    private Container[]? _group = null;

    private float _amount;
    public float Amount
    {
        get => _amount;
        private set => _amount = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Amount)) : value;
    }
    public int GroupSize => _group?.Length ?? 1;

    public void AddWater(float amount)
    {
        if (_group is null)
        {
            Amount += amount;
        }
        else
        {
            float amountPerContainer = amount / _group.Length;
            foreach (Container container in _group)
            {
                container.Amount = amountPerContainer;
            }
        }
    }

    public void ConnectTo(Container other)
    {
        _group ??= InitializeGroup(this);
        other._group ??= InitializeGroup(other);

        if (_group == other._group)
        {
            return;
        }

        int size = _group!.Length;
        int otherSize = other._group!.Length;
        float amount = Amount * size;
        float otheramount = other.Amount * otherSize;
        float newAmount = (amount + otheramount) / (size + otherSize);

        Container[] newGroup = new Container[size + otherSize];
        int i = 0;
        foreach (Container container in _group)
        {
            container._group = newGroup;
            container.Amount = newAmount;
            newGroup[i++] = container;
        }

        foreach (Container container1 in other._group)
        {
            container1._group = newGroup;
            container1.Amount = newAmount;
            newGroup[i++] = container1;
        }
    }

    private static Container[] InitializeGroup(Container container) => new Container[1] { container };
}
