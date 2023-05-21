using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using Microsoft.Diagnostics.Tracing.Parsers.ClrPrivate;

namespace PatternMatching;
public class Runner
{
    private int[] _data = { 1, 2, 3, 5, 6, 7, 8, 9, 11, 22 };


    [Benchmark]
    public void IntWithFor()
    {
        var result = Accumulate1(_data);
    }

    [Benchmark]
    public void IntWithForEach()
    {
        var result = Accumulate2(_data);
    }

    [Benchmark]
    public void GenericWithForeach()
    {
        var result = Accumulate3(_data);
    }

    [Benchmark]
    public void PatternMatching()
    {
        var result = Accumulate4(_data);
    }

    [Benchmark]
    public void PatternMatchingWithSpan()
    {
        var result = Accumulate5(_data.AsSpan());
    }

    static public int Accumulate1(int[] data)
    {
        int sum = 0;
        for (int i = 0; i < data.Length; i++) 
        {
            sum += data[i];
        }
        return sum;
    }

    static public int Accumulate2(int[] data)
    {
        int sum = 0;
        foreach (var item in data)
        {
            sum += item;
        }
        return sum;
    }

    static public T Accumulate3<T>(T[] data)
        where T : INumberBase<T>
    {
        T sum = T.Zero;
        foreach (T item in data)
        {
            sum += item;
        }
        return sum;
    }

    static public T Accumulate4<T>(T[] data)
        where T : INumberBase<T>
    {
        return data switch
        {
            [] => T.Zero,
            [T item, .. var rest] => item + Accumulate4(rest)
        };
    }

    static public T Accumulate5<T>(Span<T> data)
    where T : INumberBase<T>
    {
        return data switch
        {
            [] => T.Zero,
            [T item, .. var rest] => item + Accumulate5(rest)
        };
    }
}
