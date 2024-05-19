using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal partial struct NativeClickHouseResultStatus
{
    internal int Code;
    internal nint Message;

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void FreeClickHouseStatusMessage(ref NativeClickHouseResultStatus resultStatus);
}
