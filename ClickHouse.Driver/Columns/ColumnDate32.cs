using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnDate32 : Column<int>, ISupportsNullable
{
    public ColumnDate32()
    {
        NativeColumn = ColumnDate32Interop.chc_column_date32_create();
    }

    public ColumnDate32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(int value)
    {
        CheckDisposed();
        ColumnDate32Interop.chc_column_date32_append(NativeColumn, value);
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

            return ColumnDate32Interop.chc_column_date32_at(NativeColumn, (nuint)index);
        }
    }
}