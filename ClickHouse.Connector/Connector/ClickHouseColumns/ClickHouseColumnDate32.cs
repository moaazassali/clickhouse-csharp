namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnDate32 : ClickHouseColumn<int>
{
    public ClickHouseColumnDate32()
    {
        NativeColumn = Native.Columns.NativeColumnDate32.chc_column_date32_create();
    }

    public ClickHouseColumnDate32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(int value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDate32.chc_column_date32_append(NativeColumn, value);
    }

    public int this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnDate32.chc_column_date32_at(NativeColumn, (nuint)index);
        }
    }
}