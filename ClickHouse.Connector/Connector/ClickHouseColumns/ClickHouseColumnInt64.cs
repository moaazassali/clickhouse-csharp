namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnInt64 : ClickHouseColumn<long>
{
    public ClickHouseColumnInt64()
    {
        NativeColumn = Native.Columns.NativeColumnInt64.CreateColumnInt64();
    }

    public override void Append(long value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt64.ColumnInt64Append(NativeColumn, value);
    }
}