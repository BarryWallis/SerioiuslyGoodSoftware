using System.Diagnostics;

namespace ContractsLib;

/// <summary>
/// A water container.
/// </summary>
public class Container
{
    /// <value>
    /// All the <see cref="Container"/>s connected to this one.
    /// </value>
    private ISet<Container> _group = new HashSet<Container>();

    /// <value>
    /// The amount of water in this <see cref="Container"/>.
    /// </value>
    public double Amount { get; private set; }

    /// <summary>
    /// Initializes a new empty <see cref="Container"/>.
    /// </summary>
    public Container() => _ = _group.Add(this); // Start with this cotainer.

    /// <summary>
    /// Connect this <see cref="Container"/> to the given <see cref="Container"/>.
    /// </summary>
    /// <param name="other">The <see cref="Container"/> to connect to this one.</param>
    public void ConnectTo(Container other)
    {
        // If the two Containers are already connected, don't do anything.
        if (_group.SetEquals(other._group))
        {
            return;
        }

#if DEBUG
        ConnectToPostconditionData connectToPostconditionData = SaveConnectPostConditionData(other);
#endif

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

        Debug.Assert(PostConditionConnectTo(connectToPostconditionData));
        Debug.Assert(InvariantsArePreservedByConnectTo(other));
    }

    /// <summary>
    /// Verify that all invariants are preserved.
    /// </summary>
    /// <param name="other">The other <see cref="Container"/> to connect to.</param>
    /// <returns><see langword="true"/> if all invariants are preserved; otherwise <see langword="false"/>.</returns>
    private bool InvariantsArePreservedByConnectTo(Container other)
        => _group.SetEquals(other._group)
           && IsGroupNonNegative()
           && IsGroupBalanced()
           && IsGroupConsistent();

    /// <summary>
    /// Veify that all <see cref="Container"/>s in the group are non-negative.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if all <see cref="Container"/>s in the group are non-begative; 
    /// otherwise <see langword="false"/>.
    /// </returns>
    private bool IsGroupNonNegative() => _group.All(c => c.Amount >= 0);

    /// <summary>
    /// Check postconditions for <see cref="ConnectTo(Container)"/>.
    /// </summary>
    /// <param name="connectPostconditionData">The data from the start of the method.</param>
    /// <returns><see langword="true"/> if all postcondition checks pass; otherwise <see langword="false"/>.</returns>
    private bool PostConditionConnectTo(ConnectToPostconditionData connectPostconditionData)
        => AreGroupMembersCorrect(connectPostconditionData)
           && IsGroupAmountCorrect(connectPostconditionData)
           && IsGroupBalanced()
           && IsGroupConsistent();

    /// <summary>
    /// Verify that the total amount in the new group is the sum of the amounts in the original groups.
    /// </summary>
    /// <param name="connectPostconditionData">The original group data.</param>
    /// <returns><see langword="true"/> if the amounts are equal; otherwise <see langword="false"/>.</returns>
    private bool IsGroupAmountCorrect(ConnectToPostconditionData connectPostconditionData)
        => Amount * _group.Count
        == (connectPostconditionData._group1.First().Amount * connectPostconditionData._group1.Count)
           + (connectPostconditionData._group2.First().Amount * connectPostconditionData._group2.Count);

    /// <summary>
    /// Verify that the new grouo is the union of the two original groups.
    /// </summary>
    /// <param name="connectPostconditionData">The original group data.</param>
    /// <returns><see langword="true"/> if the group membership is correct; otherwise <see langword="false"/>.</returns>
    private bool AreGroupMembersCorrect(ConnectToPostconditionData connectPostconditionData)
        => connectPostconditionData._group1.IsSubsetOf(_group)
           && connectPostconditionData._group2.IsSubsetOf(_group)
           && _group.Count == connectPostconditionData._group1.Count + connectPostconditionData._group2.Count;

    /// <summary>
    /// Verify that each <see cref="Container"/> in the new group points to that group
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if each <see cref="Container"/> in the group points to this group;
    /// otherwise <see langword="false"/>. 
    /// </returns>
    private bool IsGroupConsistent() => _group.All(c => c._group == _group);

    /// <summary>
    /// Save starting data of <see cref="ConnectTo(Container)"/>.
    /// </summary>
    /// <param name="other">The <see cref="Container"/> to connect to.</param>
    /// <returns>The saved data.</returns>
    private ConnectToPostconditionData SaveConnectPostConditionData(Container other) => new()
    {
        _group1 = new HashSet<Container>(_group),
        _group2 = new HashSet<Container>(other._group),
        _amount1 = Amount,
        _amount2 = other.Amount
    };

    /// <summary>
    /// Add water to the <see cref="Container"/>.
    /// </summary>
    /// <param name="amount">The amount of water to add.</param>
    public void AddWater(double amount)
    {
        double amountPerContainer = amount / _group.Count;

        #region Preconditions
        if (Amount + amountPerContainer < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),
                                                  $"Not enough water to match the {nameof(AddWater)} request.");
        }
        #endregion

#if DEBUG
        double oldTotal = GroupAmount();
#endif

        foreach (Container container in _group)
        {
            container.Amount += amountPerContainer;
        }

        Debug.Assert(PostconditionAddWater(oldTotal, amount));
        Debug.Assert(InvariantsArePreservedByAddWater());
    }

    /// <summary>
    /// Verify that all invariants are preserved.
    /// </summary>
    /// <returns><see langword="true"/> if all invariants are preserved; otherwise <see langword="false"/>.</returns>
    private bool InvariantsArePreservedByAddWater() => IsGroupNonNegative() && IsGroupBalanced();

    /// <summary>
    /// Data needed to check the postconditions.
    /// </summary>
    private class ConnectToPostconditionData
    {
        public ISet<Container> _group1 = new HashSet<Container>();
        public ISet<Container> _group2 = new HashSet<Container>();
        public double _amount1;
        public double _amount2;
    }

    /// <summary>
    /// Check the following preconditions:
    /// <para>1) All the <see cref="Container"/>s in the current group hold the same amount of water.</para>
    /// <para>2) The total amount of water in the group is equal to the old amount plus the added ammount.</para>
    /// </summary>
    /// <param name="oldTotal">The original amount in all the <see cref="Container"/>s.</param>
    /// <param name="addedAmount">The amount of water added.</param>
    /// <returns><see langword="true"/> if all of the postconditions are met; otherwise <see langword="false"/>.</returns>
    private bool PostconditionAddWater(double oldTotal, double addedAmount)
        => IsGroupBalanced() && AlmostEqual(GroupAmount(), oldTotal + addedAmount);

    /// <summary>
    /// Check equality while including a tolerance for rounding errors.
    /// </summary>
    /// <param name="x">The first number to compare.</param>
    /// <param name="y">The second number to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two numbers are approxiamtely equal; otherwise <see langword="false"/>.
    /// </returns>
    private static bool AlmostEqual(double x, double y)
    {
        const double Epsilon = 10E-4;
        return Math.Abs(x - y) < Epsilon;
    }

    /// <summary>
    /// Check that all <see cref="Container"/>s in the group have the same amount.
    /// </summary>
    /// <returns><see langword="true"/> if all the <see cref="Container"/>s in the group have the same amount; 
    /// othrwise <see langword="false"/>.</returns>
    private bool IsGroupBalanced() => _group.All(c => c.Amount == Amount);

    /// <summary>
    /// Return the total amount of water in the group.
    /// </summary>
    /// <returns>The total amount of water in the group.</returns>
    private double GroupAmount() => _group.Sum(c => c.Amount);
}
