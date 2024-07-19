using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnEnum16Tests
{
    // [Fact]
    // public void Constructor_ThrowsException_WhenEnumTypeIsNotShort()
    // {
    //     Assert.Throws<InvalidOperationException>(() => new Column<ChEnum16<DayOfWeek2>>());
    // }

    private enum DayOfWeek2 : short
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
    public void Constructor_DoesNotThrowException_WhenEnumTypeIsShort()
    {
        var col = new Column<ChEnum16<DayOfWeek2>>();
    }
}