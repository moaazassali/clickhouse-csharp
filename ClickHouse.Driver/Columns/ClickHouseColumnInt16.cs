using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnInt16 : ClickHouseColumn<short>
{
    public ClickHouseColumnInt16()
    {
        NativeColumn = ColumnInt16Interop.chc_column_int16_create();
    }

    public ClickHouseColumnInt16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(short value)
    {
        CheckDisposed();
        ColumnInt16Interop.chc_column_int16_append(NativeColumn, value);
    }

    public override short this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnInt16Interop.chc_column_int16_at(NativeColumn, (nuint)index);
        }
    }
}