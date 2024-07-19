using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnInt32 : Column, IColumn<int>
{
    public ColumnInt32()
    {
        NativeColumn = ColumnInt32Interop.chc_column_int32_create();
    }

    public ColumnInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    internal override void Add(object value) => Add((int)value);

    public void Add(int value)
    {
        CheckDisposed();
        ColumnInt32Interop.chc_column_int32_append(NativeColumn, value);
    }

    public override object At(int index) => this[index];

    public int this[int index]
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