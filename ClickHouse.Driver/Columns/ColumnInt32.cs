using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnInt32 : Column<int>
{
    public ColumnInt32()
    {
        NativeColumn = ColumnInt32Interop.chc_column_int32_create();
    }

    public ColumnInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(int value)
    {
        CheckDisposed();
        ColumnInt32Interop.chc_column_int32_append(NativeColumn, value);
    }

    public override int this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnInt32Interop.chc_column_int32_at(NativeColumn, (nuint)index);
        }
    }
}