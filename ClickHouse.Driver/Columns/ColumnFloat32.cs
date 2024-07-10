using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnFloat32 : Column<float>
{
    public ColumnFloat32()
    {
        NativeColumn = ColumnFloat32Interop.chc_column_float32_create();
    }

    public ColumnFloat32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(float value)
    {
        CheckDisposed();
        ColumnFloat32Interop.chc_column_float32_append(NativeColumn, value);
    }

    public override float this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnFloat32Interop.chc_column_float32_at(NativeColumn, (nuint)index);
        }
    }
}