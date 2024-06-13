namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnUInt64 : ClickHouseColumn<ulong>
{
    public ClickHouseColumnUInt64()
    {
        NativeColumn = Native.Columns.NativeColumnUInt64.CreateColumnUInt64();
    }

    public override void Append(ulong value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnUInt64.ColumnUInt64Append(NativeColumn, value);
    }

    public ulong this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnUInt64.ColumnUInt64At(NativeColumn, index);
        }
    }
}