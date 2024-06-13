namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnUInt8 : ClickHouseColumn<byte>
{
    public ClickHouseColumnUInt8()
    {
        NativeColumn = Native.Columns.NativeColumnUInt8.CreateColumnUInt8();
    }

    public override void Append(byte value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnUInt8.ColumnUInt8Append(NativeColumn, value);
    }

    public byte this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnUInt8.ColumnUInt8At(NativeColumn, index);
        }
    }
}