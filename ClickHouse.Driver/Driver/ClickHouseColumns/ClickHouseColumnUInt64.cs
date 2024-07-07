namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnUInt64 : ClickHouseColumn<ulong>
{
    public ClickHouseColumnUInt64()
    {
        NativeColumn = Native.Columns.NativeColumnUInt64.chc_column_uint64_create();
    }
    
    public ClickHouseColumnUInt64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(ulong value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnUInt64.chc_column_uint64_append(NativeColumn, value);
    }

    public ulong this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnUInt64.chc_column_uint64_at(NativeColumn, (nuint)index);
        }
    }
}