using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnInt128Tests
{
    private readonly ColumnInt128 _column = new();

    [Fact]
    public void Add_SingleValue_ReturnsSameValue()
    {
        var value = new Int128(123, 456);

        _column.Add(value);
        var actual = _column[0];

        Assert.Equal(value, actual);
    }
}