using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnInt64 : ClickHouseColumn<long>
{
    public ClickHouseColumnInt64()
    {
        NativeColumn = ColumnInt64Interop.chc_column_int64_create();
    }

    public ClickHouseColumnInt64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(long value)
    {
        CheckDisposed();
        ColumnInt64Interop.chc_column_int64_append(NativeColumn, value);
    }

    public long this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnInt64Interop.chc_column_int64_at(NativeColumn, (nuint)index);
        }
    }
}