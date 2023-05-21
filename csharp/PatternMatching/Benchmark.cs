using System.Numerics;

using BenchmarkDotNet.Attributes;

namespace PatternMatching;

[MemoryDiagnoser]
public class Benchmark
{
    private int[] _data = { 1, 2, 3, 5, 6, 7, 8, 9, 11, 22 };

    [Benchmark]
    public void IntWithFor()
    {
        var result = Runner.Accumulate1(_data);
    }

    [Benchmark]
    public void IntWithForEach()
    {
        var result = Runner.Accumulate2(_data);
    }

    [Benchmark]
    public void GenericWithForeach()
    {
        var result = Runner.Accumulate3(_data);
    }

    [Benchmark]
    public void PatternMatching()
    {
        var result = Runner.Accumulate4(_data);
    }

    [Benchmark]
    public void PatternMatchingWithSpan()
    {
        var result = Runner.Accumulate5(_data.AsSpan());
    }


}
