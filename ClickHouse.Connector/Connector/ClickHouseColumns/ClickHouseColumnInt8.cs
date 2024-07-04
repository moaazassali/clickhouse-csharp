namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnInt8 : ClickHouseColumn<sbyte>
{
    public ClickHouseColumnInt8()
    {
        NativeColumn = Native.Columns.NativeColumnInt8.chc_column_int8_create();
    }

    public ClickHouseColumnInt8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(sbyte value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnInt8.chc_column_int8_append(NativeColumn, value);
    }

    public sbyte this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnInt8.chc_column_int8_at(NativeColumn, (nuint)index);
        }
    }
}