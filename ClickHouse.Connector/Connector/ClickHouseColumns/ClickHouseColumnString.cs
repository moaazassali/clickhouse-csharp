namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnString : ClickHouseColumn<string>
{
    public ClickHouseColumnString()
    {
        NativeColumn = Native.Columns.NativeColumnString.CreateColumnString();
    }
    
    public ClickHouseColumnString(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(string value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnString.ColumnStringAppend(NativeColumn, value);
    }

    public string this[int index]
    {
        get
        {
            CheckDisposed();
            var x = Native.Columns.NativeColumnString.ColumnStringAt(NativeColumn, index);
            return x.ToString();
        }
    }
}