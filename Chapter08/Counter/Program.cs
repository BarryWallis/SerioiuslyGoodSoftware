// See https://aka.ms/new-console-template for more information

namespace CounterApp;
public class Counter
{
    private const int NumberOfIncrements = 1_000_000;

    private int _n;

    public void Increment() => _n += 1;

    public override string ToString() => $"{_n}";

    public static void Main()
    {
        const int NumberOfThreads = 5;
        Counter counter = new();
        Thread[] threads = new Thread[NumberOfThreads];

        for (int i = 0; i < NumberOfThreads; i++)
        {
            threads[i] = new(ExecuteIncrements);
            threads[i].Start(counter);
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine($"{counter} vs {NumberOfThreads * NumberOfIncrements}");
    }

    public static void ExecuteIncrements(object? c)
    {
        Counter? counter = c as Counter;
        Debug.Assert(counter is not null);
        for (int i = 0; i < NumberOfIncrements; i++)
        {
            counter.Increment();
        }
    }
}
