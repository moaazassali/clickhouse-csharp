using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnUInt16 : Column<ushort>
{
    public ColumnUInt16()
    {
        NativeColumn = ColumnUInt16Interop.chc_column_uint16_create();
    }
    
    public ColumnUInt16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(ushort value)
    {
        CheckDisposed();
        ColumnUInt16Interop.chc_column_uint16_append(NativeColumn, value);
    }

    public override ushort this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnUInt16Interop.chc_column_uint16_at(NativeColumn, (nuint)index);
        }
    }
}