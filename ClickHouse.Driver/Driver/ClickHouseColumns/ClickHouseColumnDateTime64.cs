namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnDateTime64 : ClickHouseColumn<long>
{
    public ClickHouseColumnDateTime64(int precision)
    {
        NativeColumn = Native.Columns.NativeColumnDateTime64.chc_column_datetime64_create((nuint)precision);
    }

    public ClickHouseColumnDateTime64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(long value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDateTime64.chc_column_datetime64_append(NativeColumn, value);
    }

    public long this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnDateTime64.chc_column_datetime64_at(NativeColumn, (nuint)index);
        }
    }
}