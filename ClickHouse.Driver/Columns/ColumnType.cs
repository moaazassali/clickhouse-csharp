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

public interface IChType
{
}

public interface IChTypeSupportsNullable : IChType
{
}

public interface IChTypeSupportsLowCardinality : IChType
{
}

public readonly struct ChUInt8 : IChType
{
    private byte Value { get; init; }
    public static implicit operator ChUInt8(byte value) => new() { Value = value };
    public static implicit operator byte(ChUInt8 value) => value.Value;
}

public readonly struct ChUInt16 : IChType
{
    private ushort Value { get; init; }
    public static implicit operator ChUInt16(ushort value) => new() { Value = value };
    public static implicit operator ushort(ChUInt16 value) => value.Value;
}

public readonly struct ChUInt32 : IChType
{
    private uint Value { get; init; }
    public static implicit operator ChUInt32(uint value) => new() { Value = value };
    public static implicit operator uint(ChUInt32 value) => value.Value;
}

public readonly struct ChUInt64 : IChType
{
    private ulong Value { get; init; }
    public static implicit operator ChUInt64(ulong value) => new() { Value = value };
    public static implicit operator ulong(ChUInt64 value) => value.Value;
}

public readonly struct ChInt8 : IChType
{
    private sbyte Value { get; init; }
    public static implicit operator ChInt8(sbyte value) => new() { Value = value };
    public static implicit operator sbyte(ChInt8 value) => value.Value;
}

public readonly struct ChInt16 : IChType
{
    private short Value { get; init; }
    public static implicit operator ChInt16(short value) => new() { Value = value };
    public static implicit operator short(ChInt16 value) => value.Value;
}

public readonly struct ChInt32 : IChType
{
    private int Value { get; init; }
    public static implicit operator ChInt32(int value) => new() { Value = value };
    public static implicit operator int(ChInt32 value) => value.Value;
}

public readonly struct ChInt64 : IChType
{
    private long Value { get; init; }
    public static implicit operator ChInt64(long value) => new() { Value = value };
    public static implicit operator long(ChInt64 value) => value.Value;
}

public readonly struct ChInt128 : IChType
{
    private Int128 Value { get; init; }
    public static implicit operator ChInt128(Int128 value) => new() { Value = value };
    public static implicit operator Int128(ChInt128 value) => value.Value;

    internal static ChInt128 FromInt128Interop(Int128Interop value) => new() { Value = value.ToInt128() };

    internal Int128Interop ToInt128Interop()
    {
        return Int128Interop.FromInt128(Value);
    }
}

public readonly struct ChUuid : IChType
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
}

public readonly struct ChFloat32 : IChType
{
    private float Value { get; init; }
    public static implicit operator ChFloat32(float value) => new() { Value = value };
    public static implicit operator float(ChFloat32 value) => value.Value;
}

public readonly struct ChFloat64 : IChType
{
    private double Value { get; init; }
    public static implicit operator ChFloat64(double value) => new() { Value = value };
    public static implicit operator double(ChFloat64 value) => value.Value;
}

public readonly struct ChDecimal : IChType
{
    private Int128 Value { get; init; }
    public static implicit operator ChDecimal(Int128 value) => new() { Value = value };
    public static implicit operator Int128(ChDecimal value) => value.Value;
}

public readonly struct ChDate : IChType
{
    private ushort Value { get; init; }
    public static implicit operator ChDate(ushort value) => new() { Value = value };
    public static implicit operator ushort(ChDate value) => value.Value;
}

public readonly struct ChDate32 : IChType
{
    private int Value { get; init; }
    public static implicit operator ChDate32(int value) => new() { Value = value };
    public static implicit operator int(ChDate32 value) => value.Value;
}

public readonly struct ChDateTime : IChType
{
    private uint Value { get; init; }
    public static implicit operator ChDateTime(uint value) => new() { Value = value };
    public static implicit operator uint(ChDateTime value) => value.Value;
}

public readonly struct ChDateTime64 : IChType
{
    private long Value { get; init; }
    public static implicit operator ChDateTime64(long value) => new() { Value = value };
    public static implicit operator long(ChDateTime64 value) => value.Value;
}

public readonly struct ChEnum8 : IChType
{
    private sbyte Value { get; init; }
    public static implicit operator ChEnum8(sbyte value) => new() { Value = value };
    public static implicit operator sbyte(ChEnum8 value) => value.Value;
}

public readonly struct ChEnum16 : IChType
{
    private short Value { get; init; }
    public static implicit operator ChEnum16(short value) => new() { Value = value };
    public static implicit operator short(ChEnum16 value) => value.Value;
}

public readonly struct ChString : IChType
{
    private string Value { get; init; }
    public static implicit operator ChString(string value) => new() { Value = value };
    public static implicit operator string(ChString value) => value.Value;
}

public readonly struct ChFixedString : IChType
{
    private string Value { get; init; }
    public static implicit operator ChFixedString(string value) => new() { Value = value };
    public static implicit operator string(ChFixedString value) => value.Value;
}

public readonly struct ChIPv4 : IChType
{
    private uint Value { get; init; }
    public static implicit operator ChIPv4(uint value) => new() { Value = value };
    public static implicit operator uint(ChIPv4 value) => value.Value;
}

public readonly struct ChIPv6 : IChType
{
    private byte[] Value { get; init; }
    public static implicit operator ChIPv6(byte[] value) => new() { Value = value };
    public static implicit operator byte[](ChIPv6 value) => value.Value;

    internal static unsafe ChIPv6 FromIpv6Interop(In6AddrInterop value)
    {
        ReadOnlySpan<byte> bytes = new(value.Bytes, 16);
        return new ChIPv6 { Value = bytes.ToArray() };
    }

    internal unsafe In6AddrInterop ToIpv6Interop()
    {
        fixed (byte* ptr = Value)
        {
            return new In6AddrInterop { Bytes = ptr };
        }
    }
}