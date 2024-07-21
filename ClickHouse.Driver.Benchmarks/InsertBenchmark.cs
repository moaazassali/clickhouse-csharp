using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using ClickHouse.Driver.Columns;
using ChDriver = ClickHouse.Driver;
using ChAdo = ClickHouse.Ado;
using ChClient = ClickHouse.Client;

namespace ClickHouse.Driver.Benchmarks;

[MemoryDiagnoser]
// [NativeMemoryProfiler]
[WarmupCount(0)]
[IterationCount(1)]
public class InsertBenchmark
{
    private ChDriver.ClickHouseConnection ChDriverConnection;
    private ChAdo.ClickHouseConnection ChAdoConnection;
    private ChClient.ADO.ClickHouseConnection ChClientConnection;


    [GlobalSetup]
    public void Setup()
    {
        var options = new ChDriver.ClickHouseClientOptions { Host = "localhost" };
        ChDriverConnection = new ChDriver.ClickHouseConnection(options);

        ChAdoConnection = new ChAdo.ClickHouseConnection("Host=localhost;Port=9000;User=default;Password=");
        ChAdoConnection.Open();

        ChClientConnection = new ChClient.ADO.ClickHouseConnection();
        ChClientConnection.Open();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        ChDriverConnection.Execute("CREATE DATABASE IF NOT EXISTS test");
        ChDriverConnection.Execute("DROP TABLE IF EXISTS test.test");
        ChDriverConnection.Execute(
            "CREATE TABLE test.test (ts DateTime64(3), id UInt32, pressure Float64) ENGINE=MergeTree ORDER BY ts");
    }

    private ClickHouseBlock Block;
    [IterationSetup(Target = nameof(ChDriverInsert100M))]
    public void IterationSetupChDriverInsert100M()
    {
        var ts = new Column<ChDateTime64>();
        var id = new Column<ChUInt32>();
        var pressure = new Column<ChFloat64>();

        for (var i = 0; i < 100_000; i++)
        {
            ts.Add(DateTime.Now.Ticks);
            id.Add((uint)i);
            pressure.Add(1000.0 + i);
        }

        Block = new ClickHouseBlock();
        Block.AppendColumn("ts", ts);
        Block.AppendColumn("id", id);
        Block.AppendColumn("pressure", pressure);
    }

    [Benchmark(Description = "ClickHouse.Driver: Insert 100M")]
    public void ChDriverInsert100M()
    {
        ChDriverConnection.Insert("test.test", Block);
    }

    [IterationCleanup(Target = nameof(ChDriverInsert100M))]
    public void IterationCleanupChDriverInsert100M()
    {
        Console.WriteLine("Cleanup: " + Block.Columns.Count);
        foreach (var column in Block.Columns)
        {
            column.Dispose();
        }
        // Block.Dispose();
    }

    // [Benchmark(Description = "ClickHouse.Ado: Insert 100M")]
    // public void ChAdoInsert100M()
    // {
    //     var list = new ChAdoList
    //     {
    //         ts = [],
    //         id = [],
    //         pressure = []
    //     };
    //
    //     for (var i = 0; i < 1_000_000; i++)
    //     {
    //         list.ts.Add(DateTime.Now);
    //         list.id.Add((uint)i);
    //         list.pressure.Add(1000.0 + i);
    //     }
    //
    //     var cmd = ChAdoConnection.CreateCommand();
    //     cmd.CommandText = "INSERT INTO test.test (ts, id, pressure) VALUES @bulk";
    //     cmd.Parameters.Add(new ChAdo.ClickHouseParameter
    //     {
    //         ParameterName = "bulk",
    //         Value = list
    //     });
    //     cmd.ExecuteNonQuery();
    // }

    private class ChAdoList : ChAdo.IBulkInsertEnumerable
    {
        public List<DateTime> ts;
        public List<uint> id;
        public List<double> pressure;

        public IEnumerable<object> GetColumnData(int colNumber, string columnName, string schemaType)
        {
            return colNumber switch
            {
                0 => ts.Cast<object>(),
                1 => id.Cast<object>(),
                2 => pressure.Cast<object>(),
                _ => throw new ArgumentOutOfRangeException(nameof(colNumber))
            };
        }
    }

    // [Benchmark(Description = "ClickHouse.Client: Insert 100M")]
    // public void ChClientInsert100M()
    // {
    //     var x = 1;
    //     var bulkCopy = new ChClient.Copy.ClickHouseBulkCopy(ChClientConnection)
    //     {
    //         DestinationTableName = "test.test",
    //         ColumnNames = new[] { "ts", "id", "pressure" },
    //         BatchSize = 100_000
    //     };
    //
    //     Task.Run(() => bulkCopy.InitAsync()).GetAwaiter().GetResult();
    //     var values = Enumerable.Range(0, 100_000)
    //         .Select(i => new object[] { DateTime.Now, (uint)i, 1000.0 + i }); // Example generated data
    //     Task.Run(() => bulkCopy.WriteToServerAsync(values)).GetAwaiter().GetResult();
    // }
}