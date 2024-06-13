namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnUInt16 : ClickHouseColumn<ushort>
{
    public ClickHouseColumnUInt16()
    {
        NativeColumn = Native.Columns.NativeColumnUInt16.CreateColumnUInt16();
    }
    
    public ClickHouseColumnUInt16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(ushort value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnUInt16.ColumnUInt16Append(NativeColumn, value);
    }

    public ushort this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnUInt16.ColumnUInt16At(NativeColumn, index);
        }
    }
}