using BenchmarkDotNet.Attributes;
using ClickHouse.Driver.Columns;
using Dapper;
using ChDriver = ClickHouse.Driver;
using ChAdo = ClickHouse.Ado;
using ChClient = ClickHouse.Client;

namespace ClickHouse.Driver.Benchmarks;

[MemoryDiagnoser]
[WarmupCount(0)]
[IterationCount(1)]
public class QueryBenchmark
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
            "CREATE TABLE test.test (ts DateTime64(3), id UInt32, pressure Float64) ENGINE = Memory");
        using var ts = new Column<ChDateTime64>();
        using var id = new Column<ChUInt32>();
        using var pressure = new Column<ChFloat64>();

        for (var i = 0; i < 1_000_000; i++)
        {
            ts.Add(DateTime.Now.Ticks);
            id.Add((uint)i);
            pressure.Add(1000.0 + i);
        }

        using var block = new ClickHouseBlock();
        block.AppendColumn("ts", ts);
        block.AppendColumn("id", id);
        block.AppendColumn("pressure", pressure);
        ChDriverConnection.Insert("test.test", block);
    }

    [Benchmark(Description = "ClickHouse.Driver: Query 100M", Baseline = true)]
    public void ChDriverQuery100M()
    {
        ChDriverConnection.Select(
            "SELECT id, pressure FROM test.test",
            _ => { });
    }

    [Benchmark(Description = "ClickHouse.Client: Query 100M")]
    public void ChClientQuery100M()
    {
        var result = ChClientConnection.Query<(uint, double)>("SELECT id, pressure FROM test.test");
    }
}