using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3Lib;
public class Appliance
{
    private Grid? _grid = null;


    /// <summary>
    /// The power draw of the <see cref="Appliance"/>.
    /// </summary>
    /// <remarks>
    /// If the <see cref="Appliance"/> is off, this is the power it would draw if it were on.
    /// </remarks>
    public uint Power { get; init; }

    /// <summary>
    /// The appliance is on.
    /// </summary>
    public bool IsOn { get; private set; } = false;

    /// <summary>
    /// The appliance is off.
    /// </summary>
    public bool IsOff => !IsOn;

    /// <summary>
    /// Create a new <see cref="Appliance"/>.
    /// </summary>
    /// <param name="power"></param>
    public Appliance(uint power) => Power = power;

    /// <summary>
    /// Plug the appliance into the <see cref="Grid"/>.
    /// </summary>
    /// <param name="grid">The <see cref="Grid"/> to plug the appliance into.</param>
    public void PlugInto(Grid grid)
    {
        if (_grid is not null)
        {
            _grid.RemoveApplianceFromGrid(this);
        }

        _grid = grid;
        grid.AddApplianceToGrid(this);
    }

    /// <summary>
    /// Turn on the <see cref="Appliance"/> and notify the attached grid (if there is one).
    /// </summary>
    public void On() => IsOn = true;

    /// <summary>
    /// Turn off the <see cref="Appliance"/> and notify the attached grid (if there is one).
    /// </summary>
    public void Off() => IsOn = false;
}
