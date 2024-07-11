using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ColumnNullable<TColumn, TDataType> : Column, IColumn<TDataType?>
    where TColumn : Column, new()
{
    public ColumnNullable()
    {
        var nestedColumn = new TColumn();
        ColumnNullableInterop.chc_column_nullable_create(nestedColumn.NativeColumn, out var nativeColumn);
        NativeColumn = nativeColumn;
    }

    public ColumnNullable(Func<TColumn> factory)
    {
        var nestedColumn = factory();
        ColumnNullableInterop.chc_column_nullable_create(nestedColumn.NativeColumn, out var nativeColumn);
        NativeColumn = nativeColumn;
    }

    public ColumnNullable(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public unsafe void Add(TDataType? value)
    {
        CheckDisposed();
        ResultStatusInterop resultStatus = new() { Code = 0 };

        if (value == null)
        {
            resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, nint.Zero);
        }
        else
        {
            switch (typeof(TColumn))
            {
                case { } uint8 when uint8 == typeof(ColumnUInt8):
                case { } uint16 when uint16 == typeof(ColumnUInt16):
                case { } uint32 when uint32 == typeof(ColumnUInt32):
                case { } uint64 when uint64 == typeof(ColumnUInt64):
                case { } int8 when int8 == typeof(ColumnInt8):
                case { } int16 when int16 == typeof(ColumnInt16):
                case { } int32 when int32 == typeof(ColumnInt32):
                case { } int64 when int64 == typeof(ColumnInt64):
                case { } float32 when float32 == typeof(ColumnFloat32):
                case { } float64 when float64 == typeof(ColumnFloat64):
                case { } date when date == typeof(ColumnDate):
                case { } date32 when date32 == typeof(ColumnDate32):
                case { } datetime when datetime == typeof(ColumnDateTime):
                case { } datetime64 when datetime64 == typeof(ColumnDateTime64):
                case { } enum8 when enum8 == typeof(ColumnEnum8<>):
                case { } enum16 when enum16 == typeof(ColumnEnum16<>):
                case { } ipv4 when ipv4 == typeof(ColumnIPv4):
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&value));
                    break;
                case { } int128 when int128 == typeof(ColumnInt128):
                    var int128Interop = Int128Interop.FromInt128((Int128)value);
            }
        }

        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }
    }

    public TDataType? this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return ColumnNullableInterop.chc_column_nullable_at(NativeColumn, (nuint)index);
        }
    }
}