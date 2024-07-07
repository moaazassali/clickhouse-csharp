namespace ClickHouse.Driver.Driver.ClickHouseColumns;

public class ClickHouseColumnFloat64 : ClickHouseColumn<double>
{
    public ClickHouseColumnFloat64()
    {
        NativeColumn = Native.Columns.NativeColumnFloat64.chc_column_float64_create();
    }
    
    public ClickHouseColumnFloat64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(double value)
    {
        CheckDisposed();
        Native.Columns.NativeColumnFloat64.chc_column_float64_append(NativeColumn, value);
    }

    public double this[int index]
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumnFloat64.chc_column_float64_at(NativeColumn, (nuint)index);
        }
    }
}