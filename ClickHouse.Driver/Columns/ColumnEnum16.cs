using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnEnum16 : Column<short>
{
    public ColumnEnum16()
    {
        NativeColumn = ColumnEnum16Interop.chc_column_enum16_create();
    }

    public ColumnEnum16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(short value)
    {
        CheckDisposed();
        ColumnEnum16Interop.chc_column_enum16_append(NativeColumn, value);
    }

    public override short this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnEnum16Interop.chc_column_enum16_at(NativeColumn, (nuint)index);
        }
    }
}