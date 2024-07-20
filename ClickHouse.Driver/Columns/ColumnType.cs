using System.Collections;
using System.Net;
using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public enum ColumnType
{
    Void,
    Int8,
    Int16,
    Int32,
    Int64,
    UInt8,
    UInt16,
    UInt32,
    UInt64,
    Float32,
    Float64,
    String,
    FixedString,
    DateTime,
    Date,
    Array,
    Nullable,
    Tuple,
    Enum8,
    Enum16,
    Uuid,
    IPv4,
    IPv6,
    Int128,
    Decimal,
    Decimal32,
    Decimal64,
    Decimal128,
    LowCardinality,
    DateTime64,
    Date32,
    Map,
    Point,
    Ring,
    Polygon,
    MultiPolygon
}

public interface IChType;

internal interface IChTypeAddable
{
    void AddToColumn(nint nativeColumn);
}

public interface IChBaseType : IChType;

public interface IChTypeSupportsLowCardinality : IChType;

public readonly struct ChUInt8 : IChBaseType, IChTypeAddable
{
    private byte Value { get; init; }
    public static implicit operator ChUInt8(byte value) => new() { Value = value };
    public static implicit operator byte(ChUInt8 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnUInt8Interop.chc_column_uint8_append(nativeColumn, Value);
    }
}

public readonly struct ChUInt16 : IChBaseType, IChTypeAddable
{
    private ushort Value { get; init; }
    public static implicit operator ChUInt16(ushort value) => new() { Value = value };
    public static implicit operator ushort(ChUInt16 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnUInt16Interop.chc_column_uint16_append(nativeColumn, Value);
    }
}

public readonly struct ChUInt32 : IChBaseType, IChTypeAddable
{
    private uint Value { get; init; }
    public static implicit operator ChUInt32(uint value) => new() { Value = value };
    public static implicit operator uint(ChUInt32 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnUInt32Interop.chc_column_uint32_append(nativeColumn, Value);
    }
}

public readonly struct ChUInt64 : IChBaseType, IChTypeAddable
{
    private ulong Value { get; init; }
    public static implicit operator ChUInt64(ulong value) => new() { Value = value };
    public static implicit operator ulong(ChUInt64 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnUInt64Interop.chc_column_uint64_append(nativeColumn, Value);
    }
}

public readonly struct ChInt8 : IChBaseType, IChTypeAddable
{
    private sbyte Value { get; init; }
    public static implicit operator ChInt8(sbyte value) => new() { Value = value };
    public static implicit operator sbyte(ChInt8 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnInt8Interop.chc_column_int8_append(nativeColumn, Value);
    }
}

public readonly struct ChInt16 : IChBaseType, IChTypeAddable
{
    private short Value { get; init; }
    public static implicit operator ChInt16(short value) => new() { Value = value };
    public static implicit operator short(ChInt16 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnInt16Interop.chc_column_int16_append(nativeColumn, Value);
    }
}

public readonly struct ChInt32 : IChBaseType, IChTypeAddable
{
    private int Value { get; init; }
    public static implicit operator ChInt32(int value) => new() { Value = value };
    public static implicit operator int(ChInt32 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnInt32Interop.chc_column_int32_append(nativeColumn, Value);
    }
}

public readonly struct ChInt64 : IChBaseType, IChTypeAddable
{
    private long Value { get; init; }
    public static implicit operator ChInt64(long value) => new() { Value = value };
    public static implicit operator long(ChInt64 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnInt64Interop.chc_column_int64_append(nativeColumn, Value);
    }
}

public readonly struct ChInt128 : IChBaseType, IChTypeAddable
{
    private Int128 Value { get; init; }
    public static implicit operator ChInt128(Int128 value) => new() { Value = value };
    public static implicit operator Int128(ChInt128 value) => value.Value;

    internal static ChInt128 FromInt128Interop(Int128Interop value) => new() { Value = value.ToInt128() };

    internal Int128Interop ToInt128Interop()
    {
        return Int128Interop.FromInt128(Value);
    }

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnInt128Interop.chc_column_int128_append(nativeColumn, ToInt128Interop());
    }
}

public readonly struct ChUuid : IChBaseType, IChTypeAddable
{
    private Guid Value { get; init; }
    public static implicit operator ChUuid(Guid value) => new() { Value = value };
    public static implicit operator Guid(ChUuid value) => value.Value;

    internal static ChUuid FromUuidInterop(UuidInterop value) => new() { Value = GuidFromUuidInterop(value) };

    internal UuidInterop ToUuidInterop()
    {
        return GuidToUuidInterop(Value);
    }

    // Taken from https://stackoverflow.com/a/49380620/14003273
    // Should take another look at this because of endianness issues
    private static unsafe Guid GuidFromUuidInterop(UuidInterop value)
    {
        var ptr = stackalloc ulong[2];
        ptr[0] = value.First;
        ptr[1] = value.Second;
        return *(Guid*)ptr;
    }

    private static unsafe UuidInterop GuidToUuidInterop(Guid value)
    {
        var ptr = (ulong*)&value;
        return new UuidInterop { First = *ptr++, Second = *ptr };
    }

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnUuidInterop.chc_column_uuid_append(nativeColumn, ToUuidInterop());
    }
}

public readonly struct ChFloat32 : IChBaseType, IChTypeAddable
{
    private float Value { get; init; }
    public static implicit operator ChFloat32(float value) => new() { Value = value };
    public static implicit operator float(ChFloat32 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnFloat32Interop.chc_column_float32_append(nativeColumn, Value);
    }
}

public readonly struct ChFloat64 : IChBaseType, IChTypeAddable
{
    private double Value { get; init; }
    public static implicit operator ChFloat64(double value) => new() { Value = value };
    public static implicit operator double(ChFloat64 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnFloat64Interop.chc_column_float64_append(nativeColumn, Value);
    }
}

// public readonly struct ChDecimal : IChBaseType, IChTypeAddable
// {
//     private Int128 Value { get; init; }
//     public static implicit operator ChDecimal(Int128 value) => new() { Value = value };
//     public static implicit operator Int128(ChDecimal value) => value.Value;
//
//     void IChTypeAddable.AddToColumn(nint nativeColumn)
//     {
//         ColumnDecimalInterop.chc_column_decimal_append(nativeColumn, Value);
//     }
// }

public readonly struct ChDate : IChBaseType, IChTypeAddable
{
    private ushort Value { get; init; }
    public static implicit operator ChDate(ushort value) => new() { Value = value };
    public static implicit operator ushort(ChDate value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnDateInterop.chc_column_date_append(nativeColumn, Value);
    }
}

public readonly struct ChDate32 : IChBaseType, IChTypeAddable
{
    private int Value { get; init; }
    public static implicit operator ChDate32(int value) => new() { Value = value };
    public static implicit operator int(ChDate32 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnDate32Interop.chc_column_date32_append(nativeColumn, Value);
    }
}

public readonly struct ChDateTime : IChBaseType, IChTypeAddable
{
    private uint Value { get; init; }
    public static implicit operator ChDateTime(uint value) => new() { Value = value };
    public static implicit operator uint(ChDateTime value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnDateTimeInterop.chc_column_datetime_append(nativeColumn, Value);
    }
}

public readonly struct ChDateTime64 : IChBaseType, IChTypeAddable
{
    private long Value { get; init; }
    public static implicit operator ChDateTime64(long value) => new() { Value = value };
    public static implicit operator long(ChDateTime64 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnDateTime64Interop.chc_column_datetime64_append(nativeColumn, Value);
    }
}

internal interface IChEnum8 : IChBaseType, IChTypeAddable
{
    internal sbyte Value { get; set; }
}

public struct ChEnum8<T> : IChEnum8 where T : struct, Enum
{
    public sbyte Value { get; set; }
    public static implicit operator ChEnum8<T>(T value) => new() { Value = (sbyte)(object)value };
    public static implicit operator T(ChEnum8<T> value) => (T)(object)value.Value;

    public static explicit operator ChEnum8<T>(sbyte value) => new() { Value = value };
    public static explicit operator sbyte(ChEnum8<T> value) => value.Value;

    public static explicit operator ChEnum8<T>(ChInt8 value) => new() { Value = value };
    public static explicit operator ChInt8(ChEnum8<T> value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnEnum8Interop.chc_column_enum8_append(nativeColumn, Value);
    }
}

internal interface IChEnum16 : IChBaseType, IChTypeAddable
{
    internal short Value { get; set; }
}

public struct ChEnum16<T> : IChEnum16 where T : struct, Enum
{
    public short Value { get; set; }
    public static implicit operator ChEnum16<T>(T value) => new() { Value = (short)(object)value };
    public static implicit operator T(ChEnum16<T> value) => (T)(object)value.Value;

    public static explicit operator ChEnum16<T>(short value) => new() { Value = value };
    public static explicit operator short(ChEnum16<T> value) => value.Value;

    public static explicit operator ChEnum16<T>(ChInt16 value) => new() { Value = value };
    public static explicit operator ChInt16(ChEnum16<T> value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnEnum16Interop.chc_column_enum16_append(nativeColumn, Value);
    }
}

public readonly struct ChString : IChBaseType, IChTypeAddable, IChTypeSupportsLowCardinality
{
    private string Value { get; init; }
    public static implicit operator ChString(string value) => new() { Value = value };
    public static implicit operator string(ChString value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnStringInterop.chc_column_string_append(nativeColumn, Value);
    }
}

public readonly struct ChFixedString : IChBaseType, IChTypeAddable, IChTypeSupportsLowCardinality
{
    private string Value { get; init; }
    public static implicit operator ChFixedString(string value) => new() { Value = value };
    public static implicit operator string(ChFixedString value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnFixedStringInterop.chc_column_fixed_string_append(nativeColumn, Value);
    }
}

public readonly struct ChIPv4 : IChBaseType, IChTypeAddable
{
    private uint Value { get; init; }
    public static implicit operator ChIPv4(uint value) => new() { Value = value };
    public static implicit operator uint(ChIPv4 value) => value.Value;

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnIPv4Interop.chc_column_ipv4_append(nativeColumn, Value);
    }
}

public readonly struct ChIPv6 : IChBaseType, IChTypeAddable
{
    private byte[] Value { get; init; }
    public static implicit operator ChIPv6(byte[] value) => new() { Value = value };
    public static implicit operator byte[](ChIPv6 value) => value.Value;
    public static implicit operator IPAddress(ChIPv6 value) => new(value.Value);

    public static implicit operator ChIPv6(IPAddress value)
    {
        if (value.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
        {
            throw new ArgumentException("Value is not an IPv6 address.", nameof(value));
        }

        return new ChIPv6 { Value = value.GetAddressBytes() };
    }

    internal static unsafe ChIPv6 FromIn6AddrInterop(In6AddrInterop value)
    {
        ReadOnlySpan<byte> bytes = new(value.Bytes, 16);
        return new ChIPv6 { Value = bytes.ToArray() };
    }

    internal unsafe In6AddrInterop ToIn6AddrInterop()
    {
        ReadOnlySpan<byte> bytes = new(Value);
        In6AddrInterop result = default;
        bytes.CopyTo(new Span<byte>(result.Bytes, 16));
        return result;
    }

    void IChTypeAddable.AddToColumn(nint nativeColumn)
    {
        ColumnIPv6Interop.chc_column_ipv6_append(nativeColumn, ToIn6AddrInterop());
    }
}

public interface IChNullable : IChTypeSupportsLowCardinality
{
    bool HasValue { get; }
}

public interface IChNullable<T> : IChNullable where T : IChBaseType
{
    T? Value { get; }
}

// Similar to Nullable<T> implementation
public readonly struct ChNullable<T> : IChNullable<T> where T : struct, IChBaseType
{
    private readonly T? _value;

    private ChNullable(T? value)
    {
        _value = value;
    }

    public T Value
    {
        get
        {
            if (_value is null)
            {
                throw new InvalidOperationException(
                    "Tried accessing the Value property of a null ChNullable<T> instance.");
            }

            return _value.Value;
        }
    }

    public bool HasValue => _value.HasValue;

    public static implicit operator ChNullable<T>(T? value) => new(value);
    public static implicit operator ChNullable<T>(T value) => new(value);
    public static implicit operator T?(ChNullable<T> value) => value._value;

    public static explicit operator T(ChNullable<T> value) => value._value!.Value;

    public bool Equals(ChNullable<T> other)
    {
        if (!HasValue) return other._value == null;
        if (other._value == null) return false;
        return _value.Equals(other._value);
    }

    public static bool operator ==(ChNullable<T> a, ChNullable<T> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ChNullable<T> a, ChNullable<T> b)
    {
        return !a.Equals(b);
    }
}

public interface IChLowCardinality : IChType;

public readonly struct ChLowCardinality<T> : IChLowCardinality where T : IChTypeSupportsLowCardinality
{
    public T Value { get; private init; }
    public static implicit operator ChLowCardinality<T>(T value) => new() { Value = value };
    public static implicit operator T(ChLowCardinality<T> value) => value.Value;
}

public interface IChArray : IChType, IEnumerable
{
    int Count { get; }
}

public class ChArray<T> : IReadOnlyList<T>, IChArray where T : IChType
{
    private readonly Column? _nestedColumn;
    private readonly int _offset;
    private readonly bool _userCreated;
    private readonly IReadOnlyList<T>? _values;

    public int Count { get; }

    private ChArray(Column? nestedColumn, int offset, int count, bool userCreated, IReadOnlyList<T>? values)
    {
        _nestedColumn = nestedColumn;
        _offset = offset;
        Count = count;
        _userCreated = userCreated;
        _values = values;
    }

    public static implicit operator ChArray<T>(List<T> value) => new(null, 0, value.Count, true, value);

    public IEnumerator<T> GetEnumerator()
    {
        if (_userCreated)
        {
            foreach (var value in _values!)
            {
                yield return value;
            }

            yield break;
        }

        for (var i = 0; i < Count; i++)
        {
            yield return (T)_nestedColumn!.At(_offset + i);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T this[int index]
    {
        get
        {
            if (_userCreated) return _values![index];

            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return (T)_nestedColumn!.At(_offset + index);
        }
    }
}