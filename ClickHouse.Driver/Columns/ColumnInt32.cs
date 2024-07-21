using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ColumnInt32 : OldColumn, IOldColumn<ChInt32>
{
    public ColumnInt32()
    {
        NativeColumn = ColumnInt32Interop.chc_column_int32_create();
    }

    public ColumnInt32(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    internal override void Add(object value) => Add((ChInt32)value);

    public void Add(ChInt32 value)
    {
        CheckDisposed();
        ResultStatusInterop resultStatus = default;
        ColumnInt32Interop.chc_column_int32_append(NativeColumn, value);
        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }
    }

    public override object At(int index) => this[index];

    public ChInt32 this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnInt32Interop.chc_column_int32_at(NativeColumn, (nuint)index);
        }
    }
}