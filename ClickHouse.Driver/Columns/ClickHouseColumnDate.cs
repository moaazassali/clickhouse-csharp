using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnDate : ClickHouseColumn<ushort>
{
    public ClickHouseColumnDate()
    {
        NativeColumn = ColumnDateInterop.chc_column_date_create();
    }

    public ClickHouseColumnDate(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(ushort value)
    {
        CheckDisposed();
        ColumnDateInterop.chc_column_date_append(NativeColumn, value);
    }

    public override ushort this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnDateInterop.chc_column_date_at(NativeColumn, (nuint)index);
        }
    }
}