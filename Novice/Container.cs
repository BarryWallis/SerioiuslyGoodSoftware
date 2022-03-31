namespace Novice;
public class Container
{
    /// <summary>
    /// The group of connected containers.
    /// </summary>
    private readonly Container[] _g;

    /// <summary>
    /// The actual size of the group.
    /// </summary>
    private int _n;

    /// <summary>
    /// The water amount in this container.
    /// </summary>
    public double Amount { get; private set; }

    /// <summary>
    /// Initializes a new empty <see cref="Container"/>.
    /// </summary>
    public Container()
    {
        // Look: a magic number
        _g = new Container[1_000];

        // Put this container in its group
        _g[0] = this;
        _n = 1;
        Amount = 0;
    }

    /// <summary>
    /// Add water to the container.
    /// </summary>
    /// <param name="x">The amount of water to add.</param>
    public void AddWater(double x)
    {
        double y = x / _n;
        for (int i = 0; i < _n; i++)
        {
            _g[i].Amount = _g[i].Amount + y;
        }
    }

    /// <summary>
    /// Connect this <see cref="Container"/> to the given <see cref="Container"/>.
    /// </summary>
    /// <param name="c">The <see cref="Container"/> to connect to this one.</param>
    public void ConnectTo(Container c)
    {
        // Amount per container
        double z = ((Amount * _n) + (c.Amount * c._n)) / (_n + c._n);

        // For each container _g[i] in first group
        for (int i = 0; i < _n; i++)
        {
            // For each container _g[j] in the second group
            for (int j = 0; j < c._n; j++)
            {
                // Append c._g[j] to group _g[i]
                _g[i]._g[_n + j] = c._g[j];

                // Append _g[i] to group c._g[j]
                c._g[j]._g[c._n + i] = _g[i];
            }
        }

        _n += c._n;

        // Update sizes and amounts
        for (int i = 0; i < _n; i++)
        {
            _g[i]._n = _n;
            _g[i].Amount = z;
        }
    }
}
