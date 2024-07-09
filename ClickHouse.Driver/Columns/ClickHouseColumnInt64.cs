using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

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

    public override void Add(long value)
    {
        CheckDisposed();
        ColumnInt64Interop.chc_column_int64_append(NativeColumn, value);
    }

    public override long this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnInt64Interop.chc_column_int64_at(NativeColumn, (nuint)index);
        }
    }
}