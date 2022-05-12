namespace Speed2;

/// <summary>
/// The water container.
/// </summary>
/// <remarks>
/// The <see cref="Container"/> in a group is circularly linked to the other <see cref="Container"/>s in the group./>
/// </remarks>
public class Container : IContainer
{
    private static int _idCounter = 0;
    private readonly int _id;
    private Container _next;

    private double _amount = 0.0;
    /// <inheritdoc/>
    public double Amount
    {
        get
        {
            UpdateGroup();
            return _amount;
        }
        set => _amount = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Amount)) : value;
    }

    public int GroupSize
    {
        get
        {
            Container current = this;
            int count = 0;
            do
            {
                count += 1;
                current = current._next;
            } while (current != this);

            return count;
        }
    }

    /// <summary>
    /// Calculate the amount of water in each <see cref="Container"/> in the group.
    /// </summary>
    private void UpdateGroup()
    {
        // The first pass gets the total amount of water in the group and the number of groups.
        Container container = this;
        double totalAmount = 0.0;
        int groupSize = 0;
        do
        {
            totalAmount += container._amount;
            groupSize += 1;
            container = container._next;
        } while (container != this);

        // The second pass updates the amount in each Container in the group.
        double newAmount = totalAmount / groupSize;
        container = this;
        do
        {
            container._amount = newAmount;
            container = container._next;
        } while (container != this);
    }

    /// <summary>
    /// Create a new <see cref="Container"/> instance.
    /// </summary>
    public Container()
    {
        _next = this;
        _id = ++_idCounter;
    }

    ///// <inheritdoc/>
    //public void AddWater(double amount)
    //{
    //    double amountPerContainer = amount / _group._members.Count;
    //    _group.AmountPerContainer += amountPerContainer;
    //}

    /// <inheritdoc/>
    public void ConnectTo(Container other) =>
        // Swap the _next fields of this and other.
        (other._next, _next) = (_next, other._next);

    /// <inheritdoc/>
    public void AddWater(double amount) =>
        // Update only the local container
        Amount += amount;

    public void Flush()
    {
        Container current = this;
        do
        {
            current.Amount = 0;
            current = current._next;
        } while (current != this);
    }
}
