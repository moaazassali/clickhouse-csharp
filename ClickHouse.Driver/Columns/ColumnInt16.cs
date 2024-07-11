using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnInt16 : Column, IColumn<short>
{
    public ColumnInt16()
    {
        NativeColumn = ColumnInt16Interop.chc_column_int16_create();
    }

    public ColumnInt16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(short value)
    {
        CheckDisposed();
        ColumnInt16Interop.chc_column_int16_append(NativeColumn, value);
    }

    public short this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnInt16Interop.chc_column_int16_at(NativeColumn, (nuint)index);
        }
    }
}