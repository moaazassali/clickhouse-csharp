using System.Diagnostics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Benchmarks;

internal static class Program
{
    private static void Main(string[] args)
    {
        var config = DefaultConfig.Instance
            .AddJob(Job
                .MediumRun
                .WithToolchain(InProcessNoEmitToolchain.Instance)
            );
        BenchmarkRunner.Run<InsertBenchmark>();
        // BenchmarkRunner.Run<QueryBenchmark>(config);
    }
}