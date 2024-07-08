using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.ClickHouseColumns;

public class ClickHouseColumnDateTime : ClickHouseColumn<uint>
{
    public ClickHouseColumnDateTime()
    {
        NativeColumn = ColumnDateTimeInterop.chc_column_datetime_create();
    }

    public ClickHouseColumnDateTime(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(uint value)
    {
        CheckDisposed();
        ColumnDateTimeInterop.chc_column_datetime_append(NativeColumn, value);
    }

    public uint this[int index]
    {
        get
        {
            CheckDisposed();
            return (uint)ColumnDateTimeInterop.chc_column_datetime_at(NativeColumn, (nuint)index);
        }
    }
}