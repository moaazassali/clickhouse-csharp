using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumn
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void FreeColumn(nint column);
}
