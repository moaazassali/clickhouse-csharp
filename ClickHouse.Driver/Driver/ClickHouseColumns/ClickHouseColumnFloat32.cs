using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnFloat32 : ClickHouseColumn<float>
{
    public ClickHouseColumnFloat32()
    {
        NativeColumn = ColumnFloat32Interop.chc_column_float32_create();
    }

    public ClickHouseColumnFloat32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(float value)
    {
        CheckDisposed();
        ColumnFloat32Interop.chc_column_float32_append(NativeColumn, value);
    }

    public float this[int index]
    {
        get
        {
            CheckDisposed();
            return ColumnFloat32Interop.chc_column_float32_at(NativeColumn, (nuint)index);
        }
    }
}