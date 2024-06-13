namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnFloat64 : ClickHouseColumn<double>
{
    public ClickHouseColumnFloat64()
    {
        NativeColumn = Native.Columns.NativeColumnFloat64.CreateColumnFloat64();
    }

    public override void Append(double value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnFloat64.ColumnFloat64Append(NativeColumn, value);
    }

    public double this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnFloat64.ColumnFloat64At(NativeColumn, index);
        }
    }
}