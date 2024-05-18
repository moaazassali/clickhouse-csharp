using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnDateTime64
{
    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint CreateColumnDateTime64(nint precision);

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial void ColumnDateTime64Append(nint column, long value);
}
