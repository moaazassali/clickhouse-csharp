using BenchmarkDotNet.Running;

namespace ClickHouse.Driver.Benchmarks;

internal static class Program
{
    private static void Main(string[] args)
    {
        BenchmarkRunner.Run<InsertBenchmark>();
    }
}