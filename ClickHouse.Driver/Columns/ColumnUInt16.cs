using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnUInt16 : Column, IColumn<ushort>
{
    public ColumnUInt16()
    {
        NativeColumn = ColumnUInt16Interop.chc_column_uint16_create();
    }

    public ColumnUInt16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(ushort value)
    {
        CheckDisposed();
        ColumnUInt16Interop.chc_column_uint16_append(NativeColumn, value);
    }

    public ushort this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnUInt16Interop.chc_column_uint16_at(NativeColumn, (nuint)index);
        }
    }
}