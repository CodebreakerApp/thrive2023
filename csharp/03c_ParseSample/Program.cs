using Codebreaker.GameAPIs.Models;

using ParseSample;

ReadOnlySpan<char> name = "Andreas Nikolaus Lauda".AsSpan();
if (Person.TryParse(name, null, out Person? p))
{
    Console.WriteLine(p.FirstName);
    Console.WriteLine(p.MiddleName);
    Console.WriteLine(p.LastName);
    Console.WriteLine(p);
}

QueryParam<int> qp1 = new("42", 0);
Console.WriteLine(qp1.Value);
QueryParam<Person> qp2 = new("Bruce Wayne", Person.Empty);
Person p2 = qp2.Value;
Console.WriteLine(p2);

var dataSpan = new char[64].AsSpan();
if (p2.TryFormat(dataSpan, out int chars, "F".AsSpan()))
{
    var first = dataSpan[..chars];
    Console.WriteLine(first.ToString());
}

if (ColorResult.TryParse("1:2", default, out ColorResult result))
{
    Console.WriteLine($"correct: {result.Correct}, wrong position: {result.WrongPosition}");
}

if (ShapeAndColorResult.TryParse("1:2:3", default, out ShapeAndColorResult result2))
{
    Console.WriteLine($"correct: {result2.Correct}, wrong position: {result2.WrongPosition}, color or shape: {result2.ColorOrShape}");
}