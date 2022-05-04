namespace Memory2Lib;

/// <summary>
/// Interface for all Container objects. 
/// 
/// This interface is for convenience as Container obects in all projects will have the same public methods / properties.
/// </summary>
public interface IContainer
{
    /// <summary>
    /// The size of the group this <see cref="Container"/> belongs to.
    /// </summary>
    public int GroupSize { get; }

    /// <summary>
    /// The water amount in this container.
    /// </summary>
    public float Amount { get; }

    /// <summary>
    /// Add water to the container.
    /// </summary>
    /// <param name="x">The amount of water to add.</param>
    public void AddWater(float amount);

    /// <summary>
    /// Connect this <see cref="Container"/> to the given <see cref="Container"/>.
    /// </summary>
    /// <param name="c">The <see cref="Container"/> to connect to this one.</param>
    public void ConnectTo(Container other);
}
