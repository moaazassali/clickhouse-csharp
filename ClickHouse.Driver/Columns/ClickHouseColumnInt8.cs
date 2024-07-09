using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnInt8 : ClickHouseColumn<sbyte>
{
    public ClickHouseColumnInt8()
    {
        NativeColumn = ColumnInt8Interop.chc_column_int8_create();
    }

    public ClickHouseColumnInt8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(sbyte value)
    {
        CheckDisposed();
        ColumnInt8Interop.chc_column_int8_append(NativeColumn, value);
    }

    public override sbyte this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnInt8Interop.chc_column_int8_at(NativeColumn, (nuint)index);
        }
    }
}