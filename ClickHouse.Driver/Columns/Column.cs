using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public abstract class Column : IDisposable
{
    protected internal nint NativeColumn { get; protected init; }

    private bool _disposed;

    public ColumnType Type
    {
        get
        {
            CheckDisposed();
            return Interop.Columns.ColumnInterop.chc_column_type_code(NativeColumn);
        }
    }

    public void Reserve(int size)
    {
        CheckDisposed();
        Interop.Columns.ColumnInterop.chc_column_reserve(NativeColumn, (nuint)size);
    }

    public void Clear()
    {
        CheckDisposed();
        Interop.Columns.ColumnInterop.chc_column_clear(NativeColumn);
    }

    public int Count
    {
        get
        {
            CheckDisposed();
            return (int)Interop.Columns.ColumnInterop.chc_column_size(NativeColumn);
        }
    }

    public void Dispose()
    {
        Interop.Columns.ColumnInterop.chc_column_free(NativeColumn);
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~Column()
    {
        Dispose();
    }

    protected void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

public interface IColumn<T>
{
    void Add(T value);
    T this[int index] { get; }
}

public class Column<T> : Column where T : struct, IChType
{
    public Column()
    {
        NativeColumn = typeof(T) switch
        {
            { } uint8 when uint8 == typeof(ChUInt8) => ColumnUInt8Interop.chc_column_uint8_create(),
            { } uint16 when uint16 == typeof(ChUInt16) => ColumnUInt16Interop.chc_column_uint16_create(),
            { } uint32 when uint32 == typeof(ChUInt32) => ColumnUInt32Interop.chc_column_uint32_create(),
            { } uint64 when uint64 == typeof(ChUInt64) => ColumnUInt64Interop.chc_column_uint64_create(),
            { } int8 when int8 == typeof(ChInt8) => ColumnInt8Interop.chc_column_int8_create(),
            { } int16 when int16 == typeof(ChInt16) => ColumnInt16Interop.chc_column_int16_create(),
            { } int32 when int32 == typeof(ChInt32) => ColumnInt32Interop.chc_column_int32_create(),
            { } int64 when int64 == typeof(ChInt64) => ColumnInt64Interop.chc_column_int64_create(),
            { } int128 when int128 == typeof(ChInt128) => ColumnInt128Interop.chc_column_int128_create(),
            { } uuid when uuid == typeof(ChUuid) => ColumnUuidInterop.chc_column_uuid_create(),
            { } float32 when float32 == typeof(ChFloat32) => ColumnFloat32Interop.chc_column_float32_create(),
            { } float64 when float64 == typeof(ChFloat64) => ColumnFloat64Interop.chc_column_float64_create(),
            { } date when date == typeof(ChDate) => ColumnDateInterop.chc_column_date_create(),
            { } date32 when date32 == typeof(ChDate32) => ColumnDate32Interop.chc_column_date32_create(),
            { } datetime when datetime == typeof(ChDateTime) => ColumnDateTimeInterop.chc_column_datetime_create(),
            { } datetime64 when datetime64 == typeof(ChDateTime64) => ColumnDateTime64Interop
                .chc_column_datetime64_create(3),
            { } enum8 when enum8 == typeof(ChEnum8) => ColumnEnum8Interop.chc_column_enum8_create(),
            { } enum16 when enum16 == typeof(ChEnum16) => ColumnEnum16Interop.chc_column_enum16_create(),
            { } str when str == typeof(ChString) => ColumnStringInterop.chc_column_string_create(),
            { } fixedStr when fixedStr == typeof(ChFixedString) => ColumnFixedStringInterop
                .chc_column_fixed_string_create(5),
            { } ipv4 when ipv4 == typeof(ChIPv4) => ColumnIPv4Interop.chc_column_ipv4_create(),
            { } ipv6 when ipv6 == typeof(ChIPv6) => ColumnIPv6Interop.chc_column_ipv6_create(),
        };
    }

    // second argument is a dummy to distinguish from the constructor above internally within the library
    internal Column(nint nativeColumn, bool _)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(T value)
    {
        CheckDisposed();
        ResultStatusInterop resultStatus = default;

        switch (value)
        {
            case ChUInt8 uint8:
                ColumnUInt8Interop.chc_column_uint8_append(NativeColumn, uint8);
                break;
            case ChUInt16 uint16:
                ColumnUInt16Interop.chc_column_uint16_append(NativeColumn, uint16);
                break;
            case ChUInt32 uint32:
                ColumnUInt32Interop.chc_column_uint32_append(NativeColumn, uint32);
                break;
            case ChUInt64 uint64:
                ColumnUInt64Interop.chc_column_uint64_append(NativeColumn, uint64);
                break;
            case ChInt8 int8:
                ColumnInt8Interop.chc_column_int8_append(NativeColumn, int8);
                break;
            case ChInt16 int16:
                ColumnInt16Interop.chc_column_int16_append(NativeColumn, int16);
                break;
            case ChInt32 int32:
                ColumnInt32Interop.chc_column_int32_append(NativeColumn, int32);
                break;
            case ChInt64 int64:
                ColumnInt64Interop.chc_column_int64_append(NativeColumn, int64);
                break;
            case ChInt128 int128:
                ColumnInt128Interop.chc_column_int128_append(NativeColumn, int128.ToInt128Interop());
                break;
            case ChUuid uuid:
                ColumnUuidInterop.chc_column_uuid_append(NativeColumn, uuid.ToUuidInterop());
                break;
            case ChFloat32 float32:
                ColumnFloat32Interop.chc_column_float32_append(NativeColumn, float32);
                break;
            case ChFloat64 float64:
                ColumnFloat64Interop.chc_column_float64_append(NativeColumn, float64);
                break;
            // case ChDecimal dec:
            //     ColumnDecimalInterop.chc_column_decimal_append(NativeColumn, dec);
            //     break;
            case ChDate date:
                ColumnDateInterop.chc_column_date_append(NativeColumn, date);
                break;
            case ChDate32 date32:
                ColumnDate32Interop.chc_column_date32_append(NativeColumn, date32);
                break;
            case ChDateTime dateTime:
                ColumnDateTimeInterop.chc_column_datetime_append(NativeColumn, dateTime);
                break;
            case ChDateTime64 dateTime64:
                ColumnDateTime64Interop.chc_column_datetime64_append(NativeColumn, dateTime64);
                break;
            case ChEnum8 enum8:
                ColumnEnum8Interop.chc_column_enum8_append(NativeColumn, enum8);
                break;
            case ChEnum16 enum16:
                ColumnEnum16Interop.chc_column_enum16_append(NativeColumn, enum16);
                break;
            case ChString str:
                ColumnStringInterop.chc_column_string_append(NativeColumn, str);
                break;
            case ChFixedString fixedStr:
                resultStatus = ColumnFixedStringInterop.chc_column_fixed_string_append(NativeColumn, fixedStr);
                break;
            case ChIPv4 ipv4:
                ColumnIPv4Interop.chc_column_ipv4_append(NativeColumn, ipv4);
                break;
            case ChIPv6 ipv6:
                ColumnIPv6Interop.chc_column_ipv6_append(NativeColumn, ipv6.ToIpv6Interop());
                break;
        }

        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }
    }

    public T this[int index]
    {
        get
        {
            CheckDisposed();
            T value = default;
            var indexNuint = (nuint)index;

            return value switch
            {
                ChUInt8 _ => (T)(object)ColumnUInt8Interop.chc_column_uint8_at(NativeColumn, indexNuint),
                ChUInt16 _ => (T)(object)ColumnUInt16Interop.chc_column_uint16_at(NativeColumn, indexNuint),
                ChUInt32 _ => (T)(object)ColumnUInt32Interop.chc_column_uint32_at(NativeColumn, indexNuint),
                ChUInt64 _ => (T)(object)ColumnUInt64Interop.chc_column_uint64_at(NativeColumn, indexNuint),
                ChInt8 _ => (T)(object)ColumnInt8Interop.chc_column_int8_at(NativeColumn, indexNuint),
                ChInt16 _ => (T)(object)ColumnInt16Interop.chc_column_int16_at(NativeColumn, indexNuint),
                ChInt32 _ => (T)(object)ColumnInt32Interop.chc_column_int32_at(NativeColumn, indexNuint),
                ChInt64 _ => (T)(object)ColumnInt64Interop.chc_column_int64_at(NativeColumn, indexNuint),
                ChInt128 _ => (T)(object)ColumnInt128Interop.chc_column_int128_at(NativeColumn, indexNuint).ToInt128(),
                ChUuid _ => (T)(object)ChUuid.FromUuidInterop(
                    ColumnUuidInterop.chc_column_uuid_at(NativeColumn, indexNuint)),
                ChFloat32 _ => (T)(object)ColumnFloat32Interop.chc_column_float32_at(NativeColumn, indexNuint),
                ChFloat64 _ => (T)(object)ColumnFloat64Interop.chc_column_float64_at(NativeColumn, indexNuint),
                ChDate _ => (T)(object)ColumnDateInterop.chc_column_date_at(NativeColumn, indexNuint),
                ChDate32 _ => (T)(object)ColumnDate32Interop.chc_column_date32_at(NativeColumn, indexNuint),
                ChDateTime _ => (T)(object)ColumnDateTimeInterop.chc_column_datetime_at(NativeColumn, indexNuint),
                ChDateTime64 _ => (T)(object)ColumnDateTime64Interop.chc_column_datetime64_at(NativeColumn, indexNuint),
                ChEnum8 _ => (T)(object)ColumnEnum8Interop.chc_column_enum8_at(NativeColumn, indexNuint),
                ChEnum16 _ => (T)(object)ColumnEnum16Interop.chc_column_enum16_at(NativeColumn, indexNuint),
                ChString _ => (T)(object)ColumnStringInterop.chc_column_string_at(NativeColumn, indexNuint),
                ChFixedString _ => (T)(object)ColumnFixedStringInterop.chc_column_fixed_string_at(NativeColumn,
                    indexNuint),
                ChIPv4 _ => (T)(object)ColumnIPv4Interop.chc_column_ipv4_at(NativeColumn, indexNuint),
                ChIPv6 _ => (T)(object)ChIPv6.FromIpv6Interop(
                    ColumnIPv6Interop.chc_column_ipv6_at(NativeColumn, indexNuint)),
                _ => throw new ArgumentOutOfRangeException(Type.ToString())
            };
        }
    }
}