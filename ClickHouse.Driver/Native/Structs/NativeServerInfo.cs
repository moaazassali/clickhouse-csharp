using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal partial struct NativeServerInfo
{
    internal nint Name;
    internal nint Timezone;
    internal nint DisplayName;
    internal ulong VersionMajor;
    internal ulong VersionMinor;
    internal ulong VersionPatch;
    internal ulong Revision;
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_server_info_free(ref NativeServerInfo serverInfo);
}