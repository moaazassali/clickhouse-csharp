using ClickHouse.Driver.Columns;
using Xunit.Abstractions;

namespace ClickHouse.Driver.Tests.Integration;

public class ClickHouseConnectionIT
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ClickHouseConnectionIT(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CreateConnection_ValidOptions_Success()
    {
        using var connection = new ClickHouseConnection(new ClickHouseClientOptions
        {
            Host = "localhost",
        });
    }

    [Fact]
    public void CreateConnection_InvalidOptions_ThrowsClickHouseException()
    {
        Assert.Throws<ClickHouseException>(() =>
        {
            using var connection = new ClickHouseConnection(new ClickHouseClientOptions
            {
                Host = "invalid",
            });
        });
    }

    [Fact]
    public void Execute_ValidQuery_Success()
    {
        using var connection = new ClickHouseConnection(new ClickHouseClientOptions
        {
            Host = "localhost",
        });

        connection.Execute("SELECT 1");
    }

    [Fact]
    public void Execute_InvalidQuery_ThrowsClickHouseException()
    {
        using var connection = new ClickHouseConnection(new ClickHouseClientOptions
        {
            Host = "localhost",
        });

        Assert.Throws<ClickHouseException>(() => { connection.Execute("SELECT * FROM invalid_table"); });
    }

    [Fact]
    public void Execute_InsertBaseColumns_Select_ReturnsSameData()
    {
        using var connection = new ClickHouseConnection(new ClickHouseClientOptions
        {
            Host = "localhost",
        });

        connection.Execute("CREATE DATABASE IF NOT EXISTS ClickHouseDriverIntegrationTests");
        connection.Execute(
            " DROP TABLE IF EXISTS ClickHouseDriverIntegrationTests.Execute_InsertData_Select_ReturnsSameData");
        connection.Execute(
            """
            CREATE TABLE ClickHouseDriverIntegrationTests.Execute_InsertData_Select_ReturnsSameData
            (ts DateTime64, id UInt64, pressure Float64) ENGINE = Memory
            """
        );

        using var ts = new Column<ChDateTime64>();
        using var id = new Column<ChUInt64>();
        using var pressure = new Column<ChFloat64>();

        var tsList = new List<ChDateTime64>();
        var idList = new List<ChUInt64>();
        var pressureList = new List<ChFloat64>();

        for (var i = 0; i < 10; i++)
        {
            tsList.Add(DateTime.Now.Ticks);
            idList.Add((uint)i);
            pressureList.Add(1000.0 + i);

            ts.Add(tsList[i]);
            id.Add(idList[i]);
            pressure.Add(pressureList[i]);
        }

        using var block = new ClickHouseBlock();
        block.AppendColumn("ts", ts);
        block.AppendColumn("id", id);
        block.AppendColumn("pressure", pressure);
        connection.Insert("ClickHouseDriverIntegrationTests.Execute_InsertData_Select_ReturnsSameData", block);

        connection.Select(
            "SELECT * FROM ClickHouseDriverIntegrationTests.Execute_InsertData_Select_ReturnsSameData",
            result =>
            {
                if (result.RowCount != 10) return;

                Assert.Equal(10, result.RowCount);
                Assert.Equal(3, result.Columns.Count);

                for (var i = 0; i < 10; i++)
                {
                    Assert.Equal(tsList[i], ((Column<ChDateTime64>)result.Columns[0])[i]);
                    Assert.Equal(idList[i], ((Column<ChUInt64>)result.Columns[1])[i]);
                    Assert.Equal(pressureList[i], ((Column<ChFloat64>)result.Columns[2])[i]);
                }
            });

        connection.Execute("DROP TABLE ClickHouseDriverIntegrationTests.Execute_InsertData_Select_ReturnsSameData");
    }
}