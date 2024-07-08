using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Structs;

[StructLayout(LayoutKind.Sequential)]
internal partial struct ResultStatusInterop
{
    internal int Code;
    internal nint Message;

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_result_status_free(ref ResultStatusInterop resultStatusInterop);
}