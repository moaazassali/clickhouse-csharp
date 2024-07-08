using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.ClickHouseColumns;

public class ClickHouseColumnString : ClickHouseColumn<string>
{
    public ClickHouseColumnString()
    {
        NativeColumn = ColumnStringInterop.chc_column_string_create();
    }

    public ClickHouseColumnString(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(string value)
    {
        CheckDisposed();
        ColumnStringInterop.chc_column_string_append(NativeColumn, value);
    }

    public string this[int index]
    {
        get
        {
            CheckDisposed();
            var x = ColumnStringInterop.chc_column_string_at(NativeColumn, (nuint)index);
            return x.ToString();
        }
    }
}