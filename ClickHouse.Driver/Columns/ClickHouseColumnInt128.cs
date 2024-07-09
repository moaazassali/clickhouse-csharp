using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnInt128 : ClickHouseColumn<Int128>
{
    public ClickHouseColumnInt128()
    {
        NativeColumn = ColumnInt128Interop.chc_column_int128_create();
    }

    public ClickHouseColumnInt128(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(Int128 value)
    {
        CheckDisposed();
        ColumnInt128Interop.chc_column_int128_append(NativeColumn, Int128ToInterop(value));
    }

    public override Int128 this[int index]
    {
        get
        {
            CheckDisposed();
            var int128Interop = ColumnInt128Interop.chc_column_int128_at(NativeColumn, (nuint)index);
            return Int128FromInterop(int128Interop);
        }
    }

    private static Int128Interop Int128ToInterop(Int128 value)
    {
        return new Int128Interop { High = unchecked((long)(value >> 64)), Low = (ulong)value };
    }

    private static Int128 Int128FromInterop(Int128Interop value)
    {
        return ((Int128)value.High << 64) | value.Low;
    }
}