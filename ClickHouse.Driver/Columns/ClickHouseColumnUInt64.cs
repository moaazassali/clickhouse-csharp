using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnUInt64 : ClickHouseColumn<ulong>
{
    public ClickHouseColumnUInt64()
    {
        NativeColumn = ColumnUInt64Interop.chc_column_uint64_create();
    }
    
    public ClickHouseColumnUInt64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(ulong value)
    {
        CheckDisposed();
        ColumnUInt64Interop.chc_column_uint64_append(NativeColumn, value);
    }

    public override ulong this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnUInt64Interop.chc_column_uint64_at(NativeColumn, (nuint)index);
        }
    }
}