using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnInt64
{
    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint CreateColumnInt64();

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial void ColumnInt64Append(nint column, long value);
}