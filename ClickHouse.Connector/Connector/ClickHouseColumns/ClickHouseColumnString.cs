namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnString : ClickHouseColumn<string>
{
    public ClickHouseColumnString()
    {
        NativeColumn = Native.Columns.NativeColumnString.CreateColumnString();
    }

    public override void Append(string value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnString.ColumnStringAppend(NativeColumn, value);
    }
}