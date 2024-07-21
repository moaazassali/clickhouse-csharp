using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using ChClient = ClickHouse.Client;

namespace ClickHouse.Driver.Benchmarks;

class Program
{
    static async Task Main(string[] args)
    {
        var config = DefaultConfig.Instance
            .AddJob(Job.Default.WithToolchain(InProcessNoEmitToolchain.Instance));
        var results = BenchmarkRunner.Run<InsertBenchmark>(config);
        // InternalBenchmarks.InsertBulk.Run();


        // var chClientConnection = new ChClient.ADO.ClickHouseConnection();
        // chClientConnection.Open();
        //
        // using var bulkCopy = new ChClient.Copy.ClickHouseBulkCopy(chClientConnection)
        // {
        //     DestinationTableName = "test.test",
        //     ColumnNames = new[] { "ts", "id", "pressure" },
        //     BatchSize = 100000
        // };
        //
        // await bulkCopy.InitAsync();
        // var values = Enumerable.Range(0, 100_000)
        //     .Select(i => new object[] { DateTime.Now, (uint)i, 1000.0 + i }); // Example generated data
        // await bulkCopy.WriteToServerAsync(values);
    }
}