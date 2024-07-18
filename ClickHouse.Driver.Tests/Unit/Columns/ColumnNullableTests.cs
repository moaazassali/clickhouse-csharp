using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnNullableTests
{
    [Fact]
    public void Add_NonNullValue_ReturnsSameValue()
    {
        var col = new Column<ChNullable<ChInt8>>();

        col.Add((ChInt8)42);

        Assert.Equal(42, col[0].Value);
    }

    [Fact]
    public void Add_NullValue_ReturnsNull()
    {
        var col = new Column<ChNullable<ChInt8>>();

        col.Add(null);

        Assert.False(col[0].HasValue);
    }
}