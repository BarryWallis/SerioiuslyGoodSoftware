namespace ContainerSystemLib;

/// <summary>
/// Immutable version of Container adapted from Memory3 in Chapter04.
/// </summary>
public record ContainerSystem
{
    private readonly int[] _group;
    private readonly double[] _amount;

    private ContainerSystem(ContainerSystem oldContainerSystem, int newContainerCount)
    {
        _group = new int[newContainerCount];
        oldContainerSystem._group.CopyTo(_group, 0);

        _amount = new double[newContainerCount];
        oldContainerSystem._amount.CopyTo(_amount, 0);
    }

    /// <summary>
    /// Initialize the <see cref="ContainerSystem"/> with the total number of containers in the system.
    /// <para>
    /// Each container is assigned a sequential container id between 0 and    
    /// <paramref name="containerCount"/> - 1 by which it is referenced.
    /// </para>
    /// 
    /// </summary>
    /// <param name="containerCount">
    /// The total number of containers in the <see cref="ContainerSystem"/>
    /// </param>.
    public ContainerSystem(int containerCount)
    {
        _amount = new double[containerCount];
        _group = new int[containerCount];
        for (int i = 0; i < containerCount; i++)
        {
            _group[i] = i;
        }
    }

    /// <summary>
    /// Return the amount in the given container.
    /// </summary>
    /// <param name="containerId">The container id to return the amount for.</param>
    /// <returns>The amount in the given container.</returns>
    public double GetAmount(int containerId)
    {
        int groupId = _group[containerId];
        return _amount[groupId];
    }

    /// <summary>
    /// Add a new container to the <see cref="ContainerSystem"/>.
    /// </summary>
    /// <returns>The new <see cref="ContainerSystem"/>.</returns>
    public ContainerSystem AddContainer()
    {
        int containerCount = _group.Length;
        ContainerSystem result = new(this, containerCount + 1);
        result._group[containerCount] = containerCount;
        return result;
    }

    /// <summary>
    /// Add the amount of water to the given container.
    /// </summary>
    /// <param name="containerId">The id of the container to add water to.</param>
    /// <param name="amount">The amount of water to add to the container.</param>
    /// <returns>A new <see cref="ContainerSystem"/> with the amount of water added.</returns>
    public ContainerSystem AddWater(int containerId, double amount)
    {
        if (amount == 0)
        {
            return this;
        }

        ContainerSystem result = new(this, _group.Length);
        int groupId = _group[containerId];
        int groupSize = GroupSize(groupId);
        result._amount[groupId] += amount / groupSize;
        return result;
    }

    private int GroupSize(int groupId) => _group.Where(g => g == groupId).Count();

    public ContainerSystem Connect(int containerId1, int containerId2)
    {
        int groupID1 = _group[containerId1];
        int groupID2 = _group[containerId2];
        if (groupID1 == groupID2)
        {
            return this;
        }

        ContainerSystem result = new(this, _group.Length);
        int size1 = GroupSize(groupID1);
        int size2 = GroupSize(groupID2);
        double amount1 = _amount[groupID1] * size1;
        double amount2 = _amount[groupID2] * size2;
        result._amount[groupID1] = (amount1 + amount2) / (size1 + size2);

        for (int i = 0; i < _group.Length; i++)
        {
            if (_group[i] == groupID2)
            {
                result._group[i] = groupID1;
            }
        }

        return result;
    }
}
