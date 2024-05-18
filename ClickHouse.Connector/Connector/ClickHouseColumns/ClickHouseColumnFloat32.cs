namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnFloat32 : ClickHouseColumn<float>
{
    public ClickHouseColumnFloat32()
    {
        NativeColumn = Native.Columns.NativeColumnFloat32.CreateColumnFloat32();
    }

    public override void Append(float value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnFloat32.ColumnFloat32Append(NativeColumn, value);
    }
}