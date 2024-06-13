namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnDateTime : ClickHouseColumn<uint>
{
    public ClickHouseColumnDateTime()
    {
        NativeColumn = Native.Columns.NativeColumnDateTime.CreateColumnDateTime();
    }
    
    public ClickHouseColumnDateTime(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(uint value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDateTime.ColumnDateTimeAppend(NativeColumn, value);
    }

    public uint this[int index]
    {
        get
        {
            CheckDisposed();
            return (uint)Native.Columns.NativeColumnDateTime.ColumnDateTimeAt(NativeColumn, index);
        }
    }
}