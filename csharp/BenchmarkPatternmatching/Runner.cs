using System.Numerics;

namespace PatternMatching;
public class Runner
{
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
