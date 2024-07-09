using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct Int128Interop
{
    internal long High;
    internal ulong Low;

    internal Int128 ToInt128() => ((Int128)High << 64) | Low;

    internal static Int128Interop FromInt128(Int128 value) =>
        new() { High = unchecked((long)(value >> 64)), Low = (ulong)value };
}