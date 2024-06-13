namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnInt32 : ClickHouseColumn<int>
{
    public ClickHouseColumnInt32()
    {
        NativeColumn = Native.Columns.NativeColumnInt32.CreateColumnInt32();
    }
    
    public ClickHouseColumnInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(int value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt32.ColumnInt32Append(NativeColumn, value);
    }
    
    public int this[int index] {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnInt32.ColumnInt32At(NativeColumn, index);
        }
    }
}