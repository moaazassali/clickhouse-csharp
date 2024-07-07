namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnInt16 : ClickHouseColumn<short>
{
    public ClickHouseColumnInt16()
    {
        NativeColumn = Native.Columns.NativeColumnInt16.chc_column_int16_create();
    }

    public ClickHouseColumnInt16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(short value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt16.chc_column_int16_append(NativeColumn, value);
    }

    public short this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnInt16.chc_column_int16_at(NativeColumn, (nuint)index);
        }
    }
}