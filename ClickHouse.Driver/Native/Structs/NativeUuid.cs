using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct NativeUuid
{
    internal ulong First;
    internal ulong Second;
}