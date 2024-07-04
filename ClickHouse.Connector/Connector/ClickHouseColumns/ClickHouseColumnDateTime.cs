namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnDateTime : ClickHouseColumn<uint>
{
    public ClickHouseColumnDateTime()
    {
        NativeColumn = Native.Columns.NativeColumnDateTime.chc_column_datetime_create();
    }

    public ClickHouseColumnDateTime(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(uint value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDateTime.chc_column_datetime_append(NativeColumn, value);
    }

    public uint this[int index]
    {
        get
        {
            CheckDisposed();
            return (uint)Native.Columns.NativeColumnDateTime.chc_column_datetime_at(NativeColumn, (nuint)index);
        }
    }
}