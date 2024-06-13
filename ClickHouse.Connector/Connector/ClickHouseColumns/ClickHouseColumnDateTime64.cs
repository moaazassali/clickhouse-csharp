namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnDateTime64 : ClickHouseColumn<long>
{
    public ClickHouseColumnDateTime64(int precision)
    {
        NativeColumn = Native.Columns.NativeColumnDateTime64.CreateColumnDateTime64(precision);
    }

    public override void Append(long value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDateTime64.ColumnDateTime64Append(NativeColumn, value);
    }

    public long this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnDateTime64.ColumnDateTime64At(NativeColumn, index);
        }
    }
}