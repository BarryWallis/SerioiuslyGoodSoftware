namespace ReferenceLib;

/// <summary>
/// Interface for all Container objects. 
/// 
/// This interface is for convenience as Container obects in all projects will have the same public methods / properties.
/// </summary>
public interface IContainer
{
    /// <summary>
    /// The water amount in this container.
    /// </summary>
    public double Amount { get; }

    /// <summary>
    /// Add water to the container.
    /// </summary>
    /// <param name="amount">The amount of water to add.</param>
    public void AddWater(double amount);

    /// <summary>
    /// Connect this <see cref="Container"/> to the given <see cref="Container"/>.
    /// </summary>
    /// <param name="container">The <see cref="Container"/> to connect to this one.</param>
    public void ConnectTo(Container container);
}
