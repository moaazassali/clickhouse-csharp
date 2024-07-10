using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnDateTime64 : Column<long>
{
    public ColumnDateTime64(int precision)
    {
        NativeColumn = ColumnDateTime64Interop.chc_column_datetime64_create((nuint)precision);
    }

    public ColumnDateTime64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(long value)
    {
        CheckDisposed();
        ColumnDateTime64Interop.chc_column_datetime64_append(NativeColumn, value);
    }

    public override long this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnDateTime64Interop.chc_column_datetime64_at(NativeColumn, (nuint)index);
        }
    }
}