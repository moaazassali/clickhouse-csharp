using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnDate : Column<ushort>, ISupportsNullable
{
    public ColumnDate()
    {
        NativeColumn = ColumnDateInterop.chc_column_date_create();
    }

    public ColumnDate(nint nativeColumn)
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
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnDateInterop.chc_column_date_at(NativeColumn, (nuint)index);
        }
    }
}