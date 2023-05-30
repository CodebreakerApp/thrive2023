
// readonly struct records are immutable (get and init accessor)
MyRecord1 r1 = new();

// struct records are mutable (get and set accessor)
MyRecord2 r2 = new();

// this is just a struct, but you can use the **with** expression!
MyData mydata1 = new();
MyData mydata2 = mydata1 with { Z = 42 };
Console.WriteLine(r1);
Console.WriteLine(r2);
Console.WriteLine(mydata1);
Console.WriteLine(mydata2);

// positional readonly struct records
public readonly record struct MyRecord1(int X, int Y);

// positional struct records
public record struct MyRecord2(int X, int Y);

// nominal struct records
public record struct MyRecord3
{
    public int X { get; init; }
    public int Y { get; init; }
}

// parameterless constructor with structs!
public struct MyData
{
    public MyData()
    {
        X = -1;
        Y = -1;
        Z = 0; // remove with C# 11
    }

    public int X;
    public int Y;
    public int Z;

    public readonly override string ToString() => $"X: {X}, Y: {Y}, Z: {Z}";
}
