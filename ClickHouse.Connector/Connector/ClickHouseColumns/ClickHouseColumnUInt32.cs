namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnUInt32 : ClickHouseColumn<uint>
{
    public ClickHouseColumnUInt32()
    {
        NativeColumn = Native.Columns.NativeColumnUInt32.CreateColumnUInt32();
    }
    
    public ClickHouseColumnUInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(uint value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnUInt32.ColumnUInt32Append(NativeColumn, value);
    }

    public uint this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnUInt32.ColumnUInt32At(NativeColumn, index);
        }
    }
}