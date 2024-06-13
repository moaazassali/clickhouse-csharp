namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnInt16 : ClickHouseColumn<short>
{
    public ClickHouseColumnInt16()
    {
        NativeColumn = Native.Columns.NativeColumnInt16.CreateColumnInt16();
    }

    public override void Append(short value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt16.ColumnInt16Append(NativeColumn, value);
    }

    public short this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnInt16.ColumnInt16At(NativeColumn, index);
        }
    }
}