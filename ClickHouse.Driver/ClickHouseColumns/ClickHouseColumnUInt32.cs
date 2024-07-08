using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.ClickHouseColumns;

public class ClickHouseColumnUInt32 : ClickHouseColumn<uint>
{
    public ClickHouseColumnUInt32()
    {
        NativeColumn = ColumnUInt32Interop.chc_column_uint32_create();
    }
    
    public ClickHouseColumnUInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(uint value)
    {
        CheckDisposed();
        ColumnUInt32Interop.chc_column_uint32_append(NativeColumn, value);
    }

    public uint this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnUInt32Interop.chc_column_uint32_at(NativeColumn, (nuint)index);
        }
    }
}