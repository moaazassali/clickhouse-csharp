using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal partial struct NativeResultStatus
{
    internal int Code;
    internal nint Message;

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_result_status_free(ref NativeResultStatus resultStatus);
}