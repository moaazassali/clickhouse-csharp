namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnFloat32 : ClickHouseColumn<float>
{
    public ClickHouseColumnFloat32()
    {
        NativeColumn = Native.Columns.NativeColumnFloat32.CreateColumnFloat32();
    }
    
    public ClickHouseColumnFloat32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(float value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnFloat32.ColumnFloat32Append(NativeColumn, value);
    }

    public float this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnFloat32.ColumnFloat32At(NativeColumn, index);
        }
    }
}