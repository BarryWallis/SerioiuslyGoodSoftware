using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLib;
public interface IContainer<TValue, TOther>
{
    /// <value>The value of the container.</value>
    public TValue Value { get; set; }

    /// <summary>
    /// Connect this container to the <paramref name="other"/> container.
    /// </summary>
    /// <param name="other">The container to connect to this container.</param>
    void ConnectTo(TOther other);
}
