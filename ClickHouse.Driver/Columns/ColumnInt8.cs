using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnInt8 : Column, IColumn<sbyte>
{
    public ColumnInt8()
    {
        NativeColumn = ColumnInt8Interop.chc_column_int8_create();
    }

    public ColumnInt8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(sbyte value)
    {
        CheckDisposed();
        ColumnInt8Interop.chc_column_int8_append(NativeColumn, value);
    }

    public sbyte this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnInt8Interop.chc_column_int8_at(NativeColumn, (nuint)index);
        }
    }
}