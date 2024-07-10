using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Structs;

internal enum OptionalTypeInterop : byte
{
    Invalid,
    Null,
    UInt8,
    UInt16,
    UInt32,
    UInt64,
    Int8,
    Int16,
    Int32,
    Int64,
    Int128,
    Uuid,
    Float32,
    Float64,
    StringView,
    IPv6,
}

[StructLayout(LayoutKind.Explicit)]
internal struct OptionalValueInterop
{
    [FieldOffset(0)] internal byte UInt8;
    [FieldOffset(0)] internal ushort UInt16;
    [FieldOffset(0)] internal uint UInt32;
    [FieldOffset(0)] internal ulong UInt64;
    [FieldOffset(0)] internal sbyte Int8;
    [FieldOffset(0)] internal short Int16;
    [FieldOffset(0)] internal int Int32;
    [FieldOffset(0)] internal long Int64;
    [FieldOffset(0)] internal Int128Interop Int128;
    [FieldOffset(0)] internal UuidInterop Uuid;
    [FieldOffset(0)] internal float Float32;
    [FieldOffset(0)] internal double Float64;
    [FieldOffset(0)] internal StringViewInterop StringView;
    [FieldOffset(0)] internal In6AddrInterop Ipv6;
}

[StructLayout(LayoutKind.Sequential)]
internal struct OptionalInterop
{
    internal OptionalTypeInterop Type;
    internal OptionalValueInterop Value;
}