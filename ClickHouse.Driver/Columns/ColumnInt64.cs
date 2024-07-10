using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnInt64 : Column<long>, ISupportsNullable
{
    public ColumnInt64()
    {
        NativeColumn = ColumnInt64Interop.chc_column_int64_create();
    }

    public ColumnInt64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(long value)
    {
        CheckDisposed();
        ColumnInt64Interop.chc_column_int64_append(NativeColumn, value);
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

            return ColumnInt64Interop.chc_column_int64_at(NativeColumn, (nuint)index);
        }
    }
}