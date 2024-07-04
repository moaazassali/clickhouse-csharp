namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnFloat32 : ClickHouseColumn<float>
{
    public ClickHouseColumnFloat32()
    {
        NativeColumn = Native.Columns.NativeColumnFloat32.chc_column_float32_create();
    }

    public ClickHouseColumnFloat32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(float value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnFloat32.chc_column_float32_append(NativeColumn, value);
    }

    public float this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnFloat32.chc_column_float32_at(NativeColumn, (nuint)index);
        }
    }
}