using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ColumnNullable<T> : Column where T : struct, IChTypeNotNullable
{
    // We need to keep a reference to the nested column to prevent it from being garbage collected.
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly Column _nestedColumn;

    public ColumnNullable()
    {
        _nestedColumn = new Column<T>();
        ColumnNullableInterop.chc_column_nullable_create(_nestedColumn.NativeColumn, out var nativeColumn);
        NativeColumn = nativeColumn;
    }

    public ColumnNullable(Func<Column<T>> factory)
    {
        var nestedColumn = factory();
        ColumnNullableInterop.chc_column_nullable_create(nestedColumn.NativeColumn, out var nativeColumn);
        NativeColumn = nativeColumn;
    }

    internal ColumnNullable(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public unsafe void Add(T? value)
    {
        CheckDisposed();
        ResultStatusInterop resultStatus = default;

        if (value == null)
        {
            resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, nint.Zero);
        }
        else
        {
            switch (value.Value)
            {
                case ChUInt8 uint8:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint8));
                    break;
                case ChUInt16 uint16:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint16));
                    break;
                case ChUInt32 uint32:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint32));
                    break;
                case ChUInt64 uint64:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint64));
                    break;
                case ChInt8 int8:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int8));
                    break;
                case ChInt16 int16:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int16));
                    break;
                case ChInt32 int32:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int32));
                    break;
                case ChInt64 int64:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int64));
                    break;
                case ChInt128 int128:
                    var int128Interop = Int128Interop.FromInt128(int128);
                    resultStatus =
                        ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int128Interop));
                    break;
                case ChUuid uuid:
                    var uuidInterop = uuid.ToUuidInterop();
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uuidInterop));
                    break;
                case ChFloat32 float32:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&float32));
                    break;
                case ChFloat64 float64:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&float64));
                    break;
                case ChDate date:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&date));
                    break;
                case ChDate32 date32:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&date32));
                    break;
                case ChDateTime dateTime:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&dateTime));
                    break;
                case ChDateTime64 dateTime64:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&dateTime64));
                    break;
                case IChEnum8 enum8:
                    var enum8Value = enum8.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&enum8Value));
                    break;
                case IChEnum16 enum16:
                    var enum16Value = enum16.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&enum16Value));
                    break;
                case ChString str:
                    var strBytes = System.Text.Encoding.UTF8.GetBytes(str);
                    fixed (byte* ptr = strBytes)
                    {
                        resultStatus =
                            ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(ptr));
                    }

                    break;
                case ChFixedString fixedStr:
                    var fixedStrBytes = System.Text.Encoding.UTF8.GetBytes(fixedStr);
                    fixed (byte* ptr = fixedStrBytes)
                    {
                        resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(ptr));
                    }

                    break;
                case ChIPv4 ipv4:
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&ipv4));
                    break;
                case ChIPv6 ipv6:
                    var ipv6Interop = ipv6.ToIn6AddrInterop();
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&ipv6Interop));
                    break;
            }
        }

        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }
    }

    public T? this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var optional = ColumnNullableInterop.chc_column_nullable_at(NativeColumn, (nuint)index);
            return GetValueFromOptionalInterop(optional);
        }
    }

    private static T? GetValueFromOptionalInterop(OptionalInterop optional)
    {
        if (optional.Type == OptionalTypeInterop.Invalid)
        {
            throw new ArgumentException("Received invalid OptionalType.");
        }

        if (optional.Type == OptionalTypeInterop.Null)
        {
            return null;
        }

        T value = default;
        switch (value)
        {
            case ChUInt8:
                return (T)(object)(ChUInt8)optional.Value.UInt8;
            case ChUInt16:
                return (T)(object)(ChUInt16)optional.Value.UInt16;
            case ChUInt32:
                return (T)(object)(ChUInt32)optional.Value.UInt32;
            case ChUInt64:
                return (T)(object)(ChUInt64)optional.Value.UInt64;
            case ChInt8:
                return (T)(object)(ChInt8)optional.Value.Int8;
            case ChInt16:
                return (T)(object)(ChInt16)optional.Value.Int16;
            case ChInt32:
                return (T)(object)(ChInt32)optional.Value.Int32;
            case ChInt64:
                return (T)(object)(ChInt64)optional.Value.Int64;
            case ChInt128:
                return (T)(object)(ChInt128)optional.Value.Int128.ToInt128();
            case ChUuid:
                return (T)(object)ChUuid.FromUuidInterop(optional.Value.Uuid);
            case ChFloat32:
                return (T)(object)(ChFloat32)optional.Value.Float32;
            case ChFloat64:
                return (T)(object)(ChFloat64)optional.Value.Float64;
            case ChDate:
                return (T)(object)(ChDate)optional.Value.UInt16;
            case ChDate32:
                return (T)(object)(ChDate32)optional.Value.Int32;
            case ChDateTime:
                return (T)(object)(ChDateTime)optional.Value.UInt32;
            case ChDateTime64:
                return (T)(object)(ChDateTime64)optional.Value.UInt32;
            case IChEnum8 enum8:
                enum8.Value = optional.Value.Int8;
                return (T)enum8;
            case IChEnum16 enum16:
                enum16.Value = optional.Value.Int16;
                return (T)enum16;
            case ChString:
                return (T)(object)(ChString)optional.Value.StringView.ToString();
            case ChFixedString:
                return (T)(object)(ChFixedString)optional.Value.StringView.ToString();
            case ChIPv4:
                return (T)(object)(ChIPv4)optional.Value.UInt32;
            case ChIPv6:
                return (T)(object)ChIPv6.FromIn6AddrInterop(optional.Value.Ipv6);
        }

        throw new ArgumentException(value.GetType().ToString());
    }
}