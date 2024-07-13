using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnLowCardinalityTests
{
    [Fact]
    public void Add_SingleValue_ReturnsSameValue_String()
    {
        var column = new ColumnLowCardinality<ChString>();
        column.Add("test");
        Assert.Equal("test", column[0]);
    }

    [Fact]
    public void Add_SingleValue_ReturnsSameValue_FixedString()
    {
        var column = new ColumnLowCardinality<ChFixedString>(5);
        column.Add("hello");
        Assert.Equal("hello", column[0]);
    }

    [Fact]
    public void Add_ThrowsClickHouseException_ValueIsLongerThanFixedStringSize()
    {
        var column = new ColumnLowCardinality<ChFixedString>(5);
        Assert.Throws<ClickHouseException>(() => column.Add("world!"));
    }

    [Fact]
    public void Add_NonNullValue_ReturnsSameValue_NullableString()
    {
        var column = new ColumnLowCardinality<ChNullable<ChString>>();
        column.Add("test");
        Assert.Equal("test", column[0].Value);
    }

    [Fact]
    public void Add_NullValue_ReturnsSameValue_NullableString()
    {
        var column = new ColumnLowCardinality<ChNullable<ChString>>();
        column.Add(null);
        Assert.True(column[0] == null);
    }
}