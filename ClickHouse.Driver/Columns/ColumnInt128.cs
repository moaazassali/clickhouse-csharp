using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ColumnInt128 : Column, IColumn<Int128>, ISupportsNullable
{
    public ColumnInt128()
    {
        NativeColumn = ColumnInt128Interop.chc_column_int128_create();
    }

    public ColumnInt128(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(Int128 value)
    {
        CheckDisposed();
        ColumnInt128Interop.chc_column_int128_append(NativeColumn, Int128Interop.FromInt128(value));
    }

    public Int128 this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var int128Interop = ColumnInt128Interop.chc_column_int128_at(NativeColumn, (nuint)index);
            return int128Interop.ToInt128();
        }
    }
}