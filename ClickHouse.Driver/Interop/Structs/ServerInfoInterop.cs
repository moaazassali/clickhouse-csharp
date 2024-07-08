using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Structs;

[StructLayout(LayoutKind.Sequential)]
internal partial struct ServerInfoInterop
{
    internal nint Name;
    internal nint Timezone;
    internal nint DisplayName;
    internal ulong VersionMajor;
    internal ulong VersionMinor;
    internal ulong VersionPatch;
    internal ulong Revision;
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_server_info_free(ref ServerInfoInterop serverInfoInterop);
}