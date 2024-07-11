using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnFloat64 : Column, IColumn<double>
{
    public ColumnFloat64()
    {
        NativeColumn = ColumnFloat64Interop.chc_column_float64_create();
    }
    
    public ColumnFloat64(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(double value)
    {
        CheckDisposed();
        ColumnFloat64Interop.chc_column_float64_append(NativeColumn, value);
    }

    public double this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnFloat64Interop.chc_column_float64_at(NativeColumn, (nuint)index);
        }
    }
}