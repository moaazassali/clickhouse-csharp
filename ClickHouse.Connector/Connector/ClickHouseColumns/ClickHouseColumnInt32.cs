namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnInt32 : ClickHouseColumn<int>
{
    public ClickHouseColumnInt32()
    {
        NativeColumn = Native.Columns.NativeColumnInt32.CreateColumnInt32();
    }

    public override void Append(int value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt32.ColumnInt32Append(NativeColumn, value);
    }
}