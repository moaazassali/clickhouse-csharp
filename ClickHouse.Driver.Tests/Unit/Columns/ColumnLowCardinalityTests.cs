using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnLowCardinalityTests
{
    [Fact]
    public void Add_SingleValue_ReturnsSameValue_String()
    {
        var column = new ColumnLowCardinality<ColumnString>();
        column.Add("test");
        Assert.Equal("test", column[0]);
    }

    [Fact]
    public void Add_SingleValue_ReturnsSameValue_FixedString()
    {
        var column = new ColumnLowCardinality<ColumnFixedString>(5);
        column.Add("hello");
        Assert.Equal("hello", column[0]);
    }

    [Fact]
    public void Add_ThrowsClickHouseException_ValueIsLongerThanFixedStringSize()
    {
        var column = new ColumnLowCardinality<ColumnFixedString>(5);
        Assert.Throws<ClickHouseException>(() => column.Add("world!"));
    }
}