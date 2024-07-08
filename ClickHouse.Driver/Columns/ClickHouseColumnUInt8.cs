using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnUInt8 : ClickHouseColumn<byte>
{
    public ClickHouseColumnUInt8()
    {
        NativeColumn = ColumnUInt8Interop.chc_column_uint8_create();
    }

    public ClickHouseColumnUInt8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(byte value)
    {
        CheckDisposed();
        ColumnUInt8Interop.chc_column_uint8_append(NativeColumn, value);
    }

    public byte this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnUInt8Interop.chc_column_uint8_at(NativeColumn, (nuint)index);
        }
    }
}