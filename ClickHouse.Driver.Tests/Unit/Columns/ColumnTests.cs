using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnTests
{
    [Fact]
    public void Count_ThrowsException_WhenColumnIsDisposed()
    {
        var column = new ColumnInt8();

        column.Dispose();

        Assert.Throws<ObjectDisposedException>(() => column.Count);
    }
}