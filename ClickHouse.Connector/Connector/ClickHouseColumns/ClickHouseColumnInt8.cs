namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnInt8 : ClickHouseColumn<sbyte>
{
    public ClickHouseColumnInt8()
    {
        NativeColumn = Native.Columns.NativeColumnInt8.CreateColumnInt8();
    }
    
    public ClickHouseColumnInt8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(sbyte value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt8.ColumnInt8Append(NativeColumn, value);
    }
    
    public sbyte this[int index] {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnInt8.ColumnInt8At(NativeColumn, index);
        }
    }
}
