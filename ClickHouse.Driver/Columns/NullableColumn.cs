using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

internal class NullableColumn<T> : NativeColumnWrapper<T>
{
    // We need to keep a reference to the nested column to prevent it from being garbage collected.
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly NativeColumnWrapper _nestedColumn;

    private enum DummyEnum
    {
    }

    internal NullableColumn(uint? a = null, uint? b = null) : base(a, b)
    {
        T value = default;

        switch (value)
        {
            case ChNullable<ChUInt8>:
                _nestedColumn = new BaseColumn<ChUInt8>();
                break;
            case ChNullable<ChUInt16>:
                _nestedColumn = new BaseColumn<ChUInt16>();
                break;
            case ChNullable<ChUInt32>:
                _nestedColumn = new BaseColumn<ChUInt32>();
                break;
            case ChNullable<ChUInt64>:
                _nestedColumn = new BaseColumn<ChUInt64>();
                break;
            case ChNullable<ChInt8>:
                _nestedColumn = new BaseColumn<ChInt8>();
                break;
            case ChNullable<ChInt16>:
                _nestedColumn = new BaseColumn<ChInt16>();
                break;
            case ChNullable<ChInt32>:
                _nestedColumn = new BaseColumn<ChInt32>();
                break;
            case ChNullable<ChInt64>:
                _nestedColumn = new BaseColumn<ChInt64>();
                break;
            case ChNullable<ChInt128>:
                _nestedColumn = new BaseColumn<ChInt128>();
                break;
            case ChNullable<ChUuid>:
                _nestedColumn = new BaseColumn<ChUuid>();
                break;
            case ChNullable<ChFloat32>:
                _nestedColumn = new BaseColumn<ChFloat32>();
                break;
            case ChNullable<ChFloat64>:
                _nestedColumn = new BaseColumn<ChFloat64>();
                break;
            case ChNullable<ChDate>:
                _nestedColumn = new BaseColumn<ChDate>();
                break;
            case ChNullable<ChDate32>:
                _nestedColumn = new BaseColumn<ChDate32>();
                break;
            case ChNullable<ChDateTime>:
                _nestedColumn = new BaseColumn<ChDateTime>();
                break;
            case ChNullable<ChDateTime64>:
                _nestedColumn = new BaseColumn<ChDateTime64>();
                break;
            case IChNullable<IChEnum8>:
                _nestedColumn = new BaseColumn<ChEnum8<DummyEnum>>();
                break;
            case IChNullable<IChEnum16>:
                _nestedColumn = new BaseColumn<ChEnum16<DummyEnum>>();
                break;
            case ChNullable<ChString>:
                _nestedColumn = new BaseColumn<ChString>();
                break;
            case ChNullable<ChFixedString>:
                _nestedColumn = new BaseColumn<ChFixedString>();
                break;
            case ChNullable<ChIPv4>:
                _nestedColumn = new BaseColumn<ChIPv4>();
                break;
            case ChNullable<ChIPv6>:
                _nestedColumn = new BaseColumn<ChIPv6>();
                break;
            default:
                throw new ArgumentException(value.GetType().ToString());
        }

        ColumnNullableInterop.chc_column_nullable_create(_nestedColumn.NativeColumn, out var nativeColumn);
        NativeColumn = nativeColumn;
    }

    internal NullableColumn(nint nativeColumn, bool _) : base(nativeColumn, default)
    {
    }

    internal override unsafe void Add(T value)
    {
        CheckDisposed();
        ResultStatusInterop resultStatus = default;

        if (!((IChNullable)value!).HasValue)
        {
            ColumnNullableInterop.chc_column_nullable_append(NativeColumn, nint.Zero);
        }
        else
        {
            switch (value)
            {
                case ChNullable<ChUInt8> uint8:
                    var uint8Value = uint8.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint8Value));
                    break;
                case ChNullable<ChUInt16> uint16:
                    var uint16Value = uint16.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint16Value));
                    break;
                case ChNullable<ChUInt32> uint32:
                    var uint32Value = uint32.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint32Value));
                    break;
                case ChNullable<ChUInt64> uint64:
                    var uint64Value = uint64.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uint64Value));
                    break;
                case ChNullable<ChInt8> int8:
                    var int8Value = int8.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int8Value));
                    break;
                case ChNullable<ChInt16> int16:
                    var int16Value = int16.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int16Value));
                    break;
                case ChNullable<ChInt32> int32:
                    var int32Value = int32.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int32Value));
                    break;
                case ChNullable<ChInt64> int64:
                    var int64Value = int64.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int64Value));
                    break;
                case ChNullable<ChInt128> int128:
                    var int128Value = int128.Value.ToInt128Interop();
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&int128Value));
                    break;
                case ChNullable<ChUuid> uuid:
                    var uuidInterop = uuid.Value.ToUuidInterop();
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&uuidInterop));
                    break;
                case ChNullable<ChFloat32> float32:
                    var float32Value = float32.Value;
                    resultStatus =
                        ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&float32Value));
                    break;
                case ChNullable<ChFloat64> float64:
                    var float64Value = float64.Value;
                    resultStatus =
                        ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&float64Value));
                    break;
                case ChNullable<ChDate> date:
                    var dateValue = date.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&dateValue));
                    break;
                case ChNullable<ChDate32> date32:
                    var date32Value = date32.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&date32Value));
                    break;
                case ChNullable<ChDateTime> dateTime:
                    var dateTimeValue = dateTime.Value;
                    resultStatus =
                        ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&dateTimeValue));
                    break;
                case ChNullable<ChDateTime64> dateTime64:
                    var dateTime64Value = dateTime64.Value;
                    resultStatus =
                        ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&dateTime64Value));
                    break;
                case IChNullable<IChEnum8> enum8:
                    var enum8Value = enum8.Value.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&enum8Value));
                    break;
                case IChNullable<IChEnum16> enum16:
                    var enum16Value = enum16.Value.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&enum16Value));
                    break;
                case ChNullable<ChString> str:
                    var strBytes = System.Text.Encoding.UTF8.GetBytes(str.Value);
                    fixed (byte* ptr = strBytes)
                    {
                        resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(ptr));
                    }

                    break;
                case ChNullable<ChFixedString> fixedStr:
                    var fixedStrBytes = System.Text.Encoding.UTF8.GetBytes(fixedStr.Value);
                    fixed (byte* ptr = fixedStrBytes)
                    {
                        resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(ptr));
                    }

                    break;
                case ChNullable<ChIPv4> ipv4:
                    var ipv4Value = ipv4.Value;
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&ipv4Value));
                    break;
                case ChNullable<ChIPv6> ipv6:
                    var ipv6Interop = ipv6.Value.ToIn6AddrInterop();
                    resultStatus = ColumnNullableInterop.chc_column_nullable_append(NativeColumn, (nint)(&ipv6Interop));
                    break;
            }
        }

        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }
    }

    internal override T this[int index]
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

    private static T GetValueFromOptionalInterop(OptionalInterop optional)
    {
        if (optional.Type == OptionalTypeInterop.Invalid)
        {
            throw new ArgumentException("Received invalid OptionalType.");
        }

        if (optional.Type == OptionalTypeInterop.Null)
        {
            return (T)(dynamic)null!;
        }

        T value = default;
        switch (value)
        {
            case ChNullable<ChUInt8>:
                return (T)(object)(ChNullable<ChUInt8>)(ChUInt8)optional.Value.UInt8;
            case ChNullable<ChUInt16>:
                return (T)(object)(ChNullable<ChUInt16>)(ChUInt16)optional.Value.UInt16;
            case ChNullable<ChUInt32>:
                return (T)(object)(ChNullable<ChUInt32>)(ChUInt32)optional.Value.UInt32;
            case ChNullable<ChUInt64>:
                return (T)(object)(ChNullable<ChUInt64>)(ChUInt64)optional.Value.UInt64;
            case ChNullable<ChInt8>:
                return (T)(object)(ChNullable<ChInt8>)(ChInt8)optional.Value.Int8;
            case ChNullable<ChInt16>:
                return (T)(object)(ChNullable<ChInt16>)(ChInt16)optional.Value.Int16;
            case ChNullable<ChInt32>:
                return (T)(object)(ChNullable<ChInt32>)(ChInt32)optional.Value.Int32;
            case ChNullable<ChInt64>:
                return (T)(object)(ChNullable<ChInt64>)(ChInt64)optional.Value.Int64;
            case ChNullable<ChInt128>:
                return (T)(object)(ChNullable<ChInt128>)(ChInt128)optional.Value.Int128.ToInt128();
            case ChNullable<ChUuid>:
                return (T)(object)(ChNullable<ChUuid>)ChUuid.FromUuidInterop(optional.Value.Uuid);
            case ChNullable<ChFloat32>:
                return (T)(object)(ChNullable<ChFloat32>)(ChFloat32)optional.Value.Float32;
            case ChNullable<ChFloat64>:
                return (T)(object)(ChNullable<ChFloat64>)(ChFloat64)optional.Value.Float64;
            case ChNullable<ChDate>:
                return (T)(object)(ChNullable<ChDate>)(ChDate)optional.Value.UInt16;
            case ChNullable<ChDate32>:
                return (T)(object)(ChNullable<ChDate32>)(ChDate32)optional.Value.Int32;
            case ChNullable<ChDateTime>:
                return (T)(object)(ChNullable<ChDateTime>)(ChDateTime)optional.Value.UInt32;
            case ChNullable<ChDateTime64>:
                return (T)(object)(ChNullable<ChDateTime64>)(ChDateTime64)optional.Value.UInt32;
            case IChNullable<IChEnum8> enum8:
                enum8.Value.Value = optional.Value.Int8;
                return (T)enum8;
            case IChNullable<IChEnum16> enum16:
                enum16.Value.Value = optional.Value.Int16;
                return (T)enum16;
            case ChNullable<ChString>:
                return (T)(object)(ChNullable<ChString>)(ChString)optional.Value.StringView.ToString();
            case ChNullable<ChFixedString>:
                return (T)(object)(ChNullable<ChFixedString>)(ChFixedString)optional.Value.StringView.ToString();
            case ChNullable<ChIPv4>:
                return (T)(object)(ChNullable<ChIPv4>)(ChIPv4)optional.Value.UInt32;
            case ChNullable<ChIPv6>:
                return (T)(object)(ChNullable<ChIPv6>)ChIPv6.FromIn6AddrInterop(optional.Value.Ipv6);
        }

        throw new ArgumentException(value.GetType().ToString());
    }
}