using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal partial struct NativeEndpoint
{
    internal nint Host;
    internal ushort Port;
    
    // Used to free the memory allocated by the C++ library
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_endpoint_free(ref NativeEndpoint nativeEndpoint);
    
    // Used to free the memory allocated by this project
    internal void Free()
    {
        Marshal.FreeHGlobal(Host);
    }
}