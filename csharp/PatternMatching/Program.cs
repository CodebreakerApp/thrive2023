// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using PatternMatching;

var summary = BenchmarkRunner.Run<Runner>();

//if (args.Length == 0)
//{
//	Console.WriteLine("Usage: dotnet run -- int|foreach|array|span");
//    return;
//}

//if (args[0] == "int")
//{
//	Accumulate(data);
//}


