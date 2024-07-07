namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnInt32 : ClickHouseColumn<int>
{
    public ClickHouseColumnInt32()
    {
        NativeColumn = Native.Columns.NativeColumnInt32.chc_column_int32_create();
    }

    public ClickHouseColumnInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(int value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt32.chc_column_int32_append(NativeColumn, value);
    }

    public int this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnInt32.chc_column_int32_at(NativeColumn, (nuint)index);
        }
    }
}