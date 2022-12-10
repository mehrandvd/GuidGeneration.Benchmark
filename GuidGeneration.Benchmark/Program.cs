using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<GuidGenerationBenchmark>();

public class GuidGenerationBenchmark
{
    private const int Size = 2_000_000;

    [Benchmark]
    public List<Guid> Loop_Simple()
    {
        var list = new List<Guid>();
        for (var i = 1; i <= Size; i++)
            list.Add(Guid.NewGuid());
        return list.ToList();
    }

    [Benchmark]
    public List<Guid> Loop_Fixed_Size()
    {
        var list = new List<Guid>(Size);
        for (var i = 0; i < Size; i++)
            list.Add(Guid.NewGuid());

        return list;
    }

    [Benchmark]
    public List<Guid> Linq_Simple() 
    {
        return Enumerable
            .Range(1, Size)
            .Select(n => Guid.NewGuid())
            .ToList();
    }

    [Benchmark]
    public List<Guid> Linq_AsParallel()
    {
        return Enumerable
            .Range(1, Size)
            .AsParallel()
            .Select(n => Guid.NewGuid())
            .ToList();
    }

    [Benchmark]
    public List<Guid> Linq_Parallel()
    {
        var list = new Guid[Size];
        Parallel.ForEach(list, (_, _, index) =>
        {
            list[index] = Guid.NewGuid();
        });

        return list.ToList();
    }
}



