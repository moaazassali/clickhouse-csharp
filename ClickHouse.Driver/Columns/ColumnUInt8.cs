using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnUInt8 : Column<byte>, ISupportsNullable
{
    public ColumnUInt8()
    {
        NativeColumn = ColumnUInt8Interop.chc_column_uint8_create();
    }

    public ColumnUInt8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(byte value)
    {
        CheckDisposed();
        ColumnUInt8Interop.chc_column_uint8_append(NativeColumn, value);
    }

    public override byte this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnUInt8Interop.chc_column_uint8_at(NativeColumn, (nuint)index);
        }
    }
}