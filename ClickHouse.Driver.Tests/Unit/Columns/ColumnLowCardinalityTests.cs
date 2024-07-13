using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnLowCardinalityTests
{
    [Fact]
    public void Add_SingleValue_ReturnsSameValue_String()
    {
        var column = new Column<ChLowCardinality<ChString>>();
        column.Add((ChString)"test");
        Assert.Equal("test", (ChString)column[0]);
    }

    [Fact]
    public void Add_SingleValue_ReturnsSameValue_FixedString()
    {
        var column = new Column<ChLowCardinality<ChFixedString>>();
        column.Add((ChFixedString)"hello");
        Assert.Equal("hello", (ChFixedString)column[0]);
    }

    // [Fact]
    // public void Add_ThrowsClickHouseException_ValueIsLongerThanFixedStringSize()
    // {
    //     var column = new Column<ChLowCardinality<ChFixedString>>();
    //     Assert.Throws<ClickHouseException>(() => column.Add((ChFixedString)"world!"));
    // }

    [Fact]
    public void Add_NonNullValue_ReturnsSameValue_NullableString()
    {
        var column = new Column<ChLowCardinality<ChNullable<ChString>>>();
        column.Add((ChNullable<ChString>)(ChString)"test");
        Assert.Equal("test", column[0].Value.Value);
    }

    [Fact]
    public void Add_NullValue_ReturnsSameValue_NullableString()
    {
        var column = new Column<ChLowCardinality<ChNullable<ChString>>>();
        column.Add((ChNullable<ChString>)null);
        Assert.True((ChNullable<ChString>)column[0] == null);
    }
}