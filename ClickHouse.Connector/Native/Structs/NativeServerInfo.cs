using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct NativeServerInfo
{
    internal nint Name;
    internal nint Timezone;
    internal nint DisplayName;
    internal ulong VersionMajor;
    internal ulong VersionMinor;
    internal ulong VersionPatch;
    internal ulong Revision;
}