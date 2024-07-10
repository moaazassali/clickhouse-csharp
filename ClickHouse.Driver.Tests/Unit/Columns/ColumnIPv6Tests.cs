using System.Net;
using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnIPv6Tests
{
    [Fact]
    public void Add_SingleValue_ReturnsSameValue()
    {
        var column = new ColumnIPv6();
        var value = IPAddress.Parse("2001:0db8:85a3:0000:0000:8a2e:0370:7334");

        column.Add(value);

        Assert.Equal(value, column[0]);
    }

    [Fact]
    public void Add_ThrowsException_WhenValueIsNotIPv6()
    {
        var column = new ColumnIPv6();
        var value = IPAddress.Parse("192.168.1.1");

        Assert.Throws<ArgumentException>(() => column.Add(value));
    }
}