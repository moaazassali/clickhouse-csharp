using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnDate32 : ClickHouseColumn<int>
{
    public ClickHouseColumnDate32()
    {
        NativeColumn = ColumnDate32Interop.chc_column_date32_create();
    }

    public ClickHouseColumnDate32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(int value)
    {
        CheckDisposed();
        ColumnDate32Interop.chc_column_date32_append(NativeColumn, value);
    }

    public int this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnDate32Interop.chc_column_date32_at(NativeColumn, (nuint)index);
        }
    }
}