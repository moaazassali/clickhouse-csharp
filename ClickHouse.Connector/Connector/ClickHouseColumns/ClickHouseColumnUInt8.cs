namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnUInt8 : ClickHouseColumn<byte>
{
    public ClickHouseColumnUInt8()
    {
        NativeColumn = Native.Columns.NativeColumnUInt8.chc_column_uint8_create();
    }

    public ClickHouseColumnUInt8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(byte value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnUInt8.chc_column_uint8_append(NativeColumn, value);
    }

    public byte this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnUInt8.chc_column_uint8_at(NativeColumn, (nuint)index);
        }
    }
}