using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnInt32 : ClickHouseColumn<int>
{
    public ClickHouseColumnInt32()
    {
        NativeColumn = ColumnInt32Interop.chc_column_int32_create();
    }

    public ClickHouseColumnInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(int value)
    {
        CheckDisposed();
        ColumnInt32Interop.chc_column_int32_append(NativeColumn, value);
    }

    public override int this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnInt32Interop.chc_column_int32_at(NativeColumn, (nuint)index);
        }
    }
}