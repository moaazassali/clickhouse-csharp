namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnInt64 : ClickHouseColumn<long>
{
    public ClickHouseColumnInt64()
    {
        NativeColumn = Native.Columns.NativeColumnInt64.chc_column_int64_create();
    }

    public ClickHouseColumnInt64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(long value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt64.chc_column_int64_append(NativeColumn, value);
    }

    public long this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnInt64.chc_column_int64_at(NativeColumn, (nuint)index);
        }
    }
}