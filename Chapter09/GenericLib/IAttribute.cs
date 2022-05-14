using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLib;

/// <summary>
/// The generic interface for container properties.
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TSummary"></typeparam>
public interface IAttribute<TValue, TSummary>
{
    /// <value>The inital summary.</value>
    public TSummary Seed { get; init; }

    /// <summary>
    /// Update the <paramref name="summary"/> with the <paramref name="value"/>.
    /// </summary>
    /// <param name="summary">The summary.</param>
    /// <param name="value">The value.</param>
    public void Update(TSummary summary, TValue value);

    /// <summary>
    /// Merge two summaries.
    /// </summary>
    /// <param name="summary1">The first summary.</param>
    /// <param name="summary2">The second summary.</param>
    /// <returns>The merged summary.</returns>
    public TSummary Merge(TSummary summary1, TSummary summary2);

    /// <summary>
    /// Unwrap a summary.
    /// </summary>
    /// <param name="summary">The summary.</param>
    /// <returns>The unwrapped value.</returns>
    public TValue Report(TSummary summary);
}
