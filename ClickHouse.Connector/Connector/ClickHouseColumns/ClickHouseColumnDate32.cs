namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnDate32 : ClickHouseColumn<int>
{
    public ClickHouseColumnDate32()
    {
        NativeColumn = Native.Columns.NativeColumnDate32.CreateColumnDate32();
    }
    
    public ClickHouseColumnDate32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(int value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDate32.ColumnDate32Append(NativeColumn, value);
    }

    public int this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnDate32.ColumnDate32At(NativeColumn, index);
        }
    }
}