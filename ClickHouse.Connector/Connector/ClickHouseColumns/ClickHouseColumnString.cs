namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnString : ClickHouseColumn<string>
{
    public ClickHouseColumnString()
    {
        NativeColumn = Native.Columns.NativeColumnString.chc_column_string_create();
    }

    public ClickHouseColumnString(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(string value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnString.chc_column_string_append(NativeColumn, value);
    }

    public string this[int index]
    {
        get
        {
            CheckDisposed();
            var x = Native.Columns.NativeColumnString.chc_column_string_at(NativeColumn, (nuint)index);
            return x.ToString();
        }
    }
}