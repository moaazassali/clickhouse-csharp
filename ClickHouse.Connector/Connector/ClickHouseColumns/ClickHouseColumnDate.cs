namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnDate : ClickHouseColumn<ushort>
{
    public ClickHouseColumnDate()
    {
        NativeColumn = Native.Columns.NativeColumnDate.CreateColumnDate();
    }
    
    public ClickHouseColumnDate(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(ushort value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDate.ColumnDateAppend(NativeColumn, value);
    }

    public ushort this[int index]
    {
        get
        {
            CheckDisposed();
            return (ushort)Native.Columns.NativeColumnDate.ColumnDateAt(NativeColumn, index);
        }
    }
}