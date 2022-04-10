
using BenchmarkDotNet.Attributes;

using ReferenceLib;

namespace ReferenceApp;
public class Benchmarks
{
    [Params(32000)]
    public int _n;

    [Benchmark]
    public void IdentityMatrix1Benchmark() => Exercises.IdentityMatrix1(_n);

    [Benchmark]
    public void IdentityMatrix2Benchmark() => Exercises.IdentityMatrix2(_n);
}
