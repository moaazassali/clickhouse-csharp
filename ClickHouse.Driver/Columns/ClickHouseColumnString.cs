using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

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

    public override void Add(string value)
    {
        CheckDisposed();
        ColumnStringInterop.chc_column_string_append(NativeColumn, value);
    }

    public override string this[int index]
    {
        get
        {
            CheckDisposed();
            var x = ColumnStringInterop.chc_column_string_at(NativeColumn, (nuint)index);
            return x.ToString();
        }
    }
}