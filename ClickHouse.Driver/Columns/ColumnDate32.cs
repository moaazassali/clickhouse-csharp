using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnDate32 : Column, IColumn<int>, ISupportsNullable
{
    public ColumnDate32()
    {
        NativeColumn = ColumnDate32Interop.chc_column_date32_create();
    }

    public ColumnDate32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(int value)
    {
        CheckDisposed();
        ColumnDate32Interop.chc_column_date32_append(NativeColumn, value);
    }

    public int this[int index]
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