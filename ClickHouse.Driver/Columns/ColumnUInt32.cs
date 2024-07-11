using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnUInt32 : Column, IColumn<uint>
{
    public ColumnUInt32()
    {
        NativeColumn = ColumnUInt32Interop.chc_column_uint32_create();
    }

    public ColumnUInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(uint value)
    {
        CheckDisposed();
        ColumnUInt32Interop.chc_column_uint32_append(NativeColumn, value);
    }

    public uint this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnUInt32Interop.chc_column_uint32_at(NativeColumn, (nuint)index);
        }
    }
}