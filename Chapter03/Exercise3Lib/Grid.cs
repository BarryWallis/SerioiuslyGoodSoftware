using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3Lib;
public class Grid
{
    private readonly HashSet<Appliance> _grid = new();

    public uint ResidualPower { get; private set; }

    /// <summary>
    /// Determine if the <see cref="Appliance"/> is on the <see cref="Grid"/>.
    /// </summary>
    /// <param name="appliance">The appliance to check.</param>
    /// <returns><see langword="true"/> if the appliance is on the grid; otherwise false.</returns>
    public bool ApplianceIsOnGrid(Appliance appliance) => _grid.Contains(appliance);

    /// <summary>
    /// Create a new <see cref="Grid"/>.
    /// </summary>
    /// <param name="power">The power the <see cref="Grid"/> can provide.</param>
    public Grid(uint power) => ResidualPower = power;

    /// <summary>
    /// Add the given appliance to the <see cref="Grid"/>. 
    /// </summary>
    /// <param name="appliance">The appliance to add.</param>
    /// <remarks>
    /// Appliance should never attempt to plug into the same grid twice.
    /// </remarks>
    internal void AddApplianceToGrid(Appliance appliance)
    {
        Debug.Assert(!_grid.Contains(appliance));
        CheckForOverload(appliance);
        AddValidApplianceToGrid(appliance);
    }

    private void AddValidApplianceToGrid(Appliance appliance)
    {
        bool success = _grid.Add(appliance);
        Debug.Assert(success is true);
        if (appliance.IsOn)
        {
            Debug.Assert(appliance.Power <= ResidualPower);
            ResidualPower -= appliance.Power;
        }
    }

    internal void ApplianceIsTurnedOn(Appliance appliance)
    {
        Debug.Assert(_grid.Contains(appliance));
        CheckForOverload(appliance);
        ResidualPower -= appliance.Power;
    }

    internal void ApplianceIsTurnedOff(Appliance appliance)
    {
        Debug.Assert(_grid.Contains(appliance));
        ResidualPower += appliance.Power;
    }

    /// <summary>
    /// Check if the appliance would overload the grid.
    /// </summary>
    /// <param name="appliance">The appliance to check</param>
    /// <exception cref="InvalidOperationException">
    /// Appliance would draw more power than is available in grid.
    /// </exception>
    private void CheckForOverload(Appliance appliance)
    {
        if (appliance.IsOn && appliance.Power > ResidualPower)
        {
            throw new InvalidOperationException("Appliance would draw more power than is available in grid.");
        }
    }

    /// <summary>
    /// Remove the given <see cref="Appliance"/> from the <see cref="Grid"/>.
    /// </summary>
    /// <param name="appliance">The <see cref="Appliance"/> to remove.</param>
    internal void RemoveApplianceFromGrid(Appliance appliance)
    {
        bool success = _grid.Remove(appliance);
        Debug.Assert(success is true);
        ResidualPower += appliance.Power;
    }

}
