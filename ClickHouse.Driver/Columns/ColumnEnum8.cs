using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnEnum8 : Column<sbyte>
{
    public ColumnEnum8()
    {
        NativeColumn = ColumnEnum8Interop.chc_column_enum8_create();
    }

    public ColumnEnum8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(sbyte value)
    {
        CheckDisposed();
        ColumnEnum8Interop.chc_column_enum8_append(NativeColumn, value);
    }

    public override sbyte this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnEnum8Interop.chc_column_enum8_at(NativeColumn, (nuint)index);
        }
    }
}