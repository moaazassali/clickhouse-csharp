using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct UuidInterop
{
    internal ulong First;
    internal ulong Second;
}