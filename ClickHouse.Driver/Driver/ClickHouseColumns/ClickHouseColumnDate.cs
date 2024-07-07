namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnDate : ClickHouseColumn<ushort>
{
    public ClickHouseColumnDate()
    {
        NativeColumn = Native.Columns.NativeColumnDate.chc_column_date_create();
    }

    public ClickHouseColumnDate(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(ushort value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnDate.chc_column_date_append(NativeColumn, value);
    }

    public ushort this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnDate.chc_column_date_at(NativeColumn, (nuint)index);
        }
    }
}