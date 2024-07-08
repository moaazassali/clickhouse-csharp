using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnUInt16 : ClickHouseColumn<ushort>
{
    public ClickHouseColumnUInt16()
    {
        NativeColumn = ColumnUInt16Interop.chc_column_uint16_create();
    }
    
    public ClickHouseColumnUInt16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(ushort value)
    {
        CheckDisposed();
        ColumnUInt16Interop.chc_column_uint16_append(NativeColumn, value);
    }

    public ushort this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnUInt16Interop.chc_column_uint16_at(NativeColumn, (nuint)index);
        }
    }
}