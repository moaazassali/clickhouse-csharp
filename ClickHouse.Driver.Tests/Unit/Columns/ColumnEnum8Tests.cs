using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnEnum8Tests
{
    // [Fact]
    // public void Constructor_ThrowsException_WhenEnumTypeIsNotSByte()
    // {
    //     Assert.Throws<InvalidOperationException>(() => new Column<ChEnum8<DayOfWeek>>());
    // }

    private enum DayOfWeek2 : sbyte
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

    [Fact]
    public void Constructor_DoesNotThrowException_WhenEnumTypeIsSByte()
    {
        var col = new Column<ChEnum8<DayOfWeek2>>();
    }
}