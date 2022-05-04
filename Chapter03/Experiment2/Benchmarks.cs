
using BenchmarkDotNet.Attributes;

namespace Experiment2;

public class Benchmarks
{
    [Params(5_000)]
    public int _n;

    [Benchmark]
    public void ReferenceBenchmarkRunner() => ReferenceBenchmark(_n);

    [Benchmark]
    public void Speed1BenchmarkRunner() => Speed1BenchmarkRunner(_n);


    [Benchmark]
    public void Speed2BenchmarkRunner() => Speed2BenchmarkRunner(_n);

    [Benchmark]
    public void Speed3BenchmarkRunner() => Speed3BenchmarkRunner(_n);

    private static void Speed1BenchmarkRunner(int n)
    {
        // Create n containers and add water to each one
        Speed1.Container[] containers = new Speed1.Container[n];
        for (int i = 0; i < containers.Length; i++)
        {
            containers[i] = new();
            containers[i].AddWater(10);
        }

        // Connect the containers in pairs, add water to each pair and query the amount of water in each pair
        for (int i = 0; i < containers.Length; i += 2)
        {
            containers[i].ConnectTo(containers[i + 1]);
            containers[i].AddWater(10);
        }

        // Coonect the pairs of containers together into a single group. After each connection add water and query the amount in the group
        for (int i = 2; i < containers.Length; i += 2)
        {
            containers[i - 1].ConnectTo(containers[i]);
            containers[i - 1].AddWater(10);
        }
        _ = containers[0].Amount;
    }


    private static void Speed2BenchmarkRunner(int n)
    {
        // Create n containers and add water to each one
        Speed2.Container[] containers = new Speed2.Container[n];
        for (int i = 0; i < containers.Length; i++)
        {
            containers[i] = new();
            containers[i].AddWater(10);
        }

        // Connect the containers in pairs, add water to each pair and query the amount of water in each pair
        for (int i = 0; i < containers.Length; i += 2)
        {
            containers[i].ConnectTo(containers[i + 1]);
            containers[i].AddWater(10);
        }

        // Coonect the pairs of containers together into a single group. After each connection add water and query the amount in the group
        for (int i = 2; i < containers.Length; i += 2)
        {
            containers[i - 1].ConnectTo(containers[i]);
            containers[i - 1].AddWater(10);
        }

        _ = containers[0].Amount;
    }

    private static void Speed3BenchmarkRunner(int n)
    {
        // Create n containers and add water to each one
        Speed3.Container[] containers = new Speed3.Container[n];
        for (int i = 0; i < containers.Length; i++)
        {
            containers[i] = new();
            containers[i].AddWater(10);
        }

        // Connect the containers in pairs, add water to each pair and query the amount of water in each pair
        for (int i = 0; i < containers.Length; i += 2)
        {
            containers[i].ConnectTo(containers[i + 1]);
            containers[i].AddWater(10);
        }

        // Coonect the pairs of containers together into a single group. After each connection add water and query the amount in the group
        for (int i = 2; i < containers.Length; i += 2)
        {
            containers[i - 1].ConnectTo(containers[i]);
            containers[i - 1].AddWater(10);
        }

        _ = containers[0].Amount;
    }
    private static void ReferenceBenchmark(int n)
    {
        // Create n containers and add water to each one
        ReferenceLib.Container[] containers = new ReferenceLib.Container[n];
        for (int i = 0; i < containers.Length; i++)
        {
            containers[i] = new();
            containers[i].AddWater(10);
        }

        // Connect the containers in pairs, add water to each pair and query the amount of water in each pair
        for (int i = 0; i < containers.Length; i += 2)
        {
            containers[i].ConnectTo(containers[i + 1]);
            containers[i].AddWater(10);
        }

        // Coonect the pairs of containers together into a single group. After each connection add water and query the amount in the group
        for (int i = 2; i < containers.Length; i += 2)
        {
            containers[i - 1].ConnectTo(containers[i]);
            containers[i - 1].AddWater(10);
        }

        _ = containers[0].Amount;
    }
}
