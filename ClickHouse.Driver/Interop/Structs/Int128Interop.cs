using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct Int128Interop
{
    internal long High;
    internal ulong Low;
}