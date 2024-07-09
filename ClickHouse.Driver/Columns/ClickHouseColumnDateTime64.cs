using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnDateTime64 : ClickHouseColumn<long>
{
    public ClickHouseColumnDateTime64(int precision)
    {
        NativeColumn = ColumnDateTime64Interop.chc_column_datetime64_create((nuint)precision);
    }

    public ClickHouseColumnDateTime64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(long value)
    {
        CheckDisposed();
        ColumnDateTime64Interop.chc_column_datetime64_append(NativeColumn, value);
    }

    public override long this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnDateTime64Interop.chc_column_datetime64_at(NativeColumn, (nuint)index);
        }
    }
}