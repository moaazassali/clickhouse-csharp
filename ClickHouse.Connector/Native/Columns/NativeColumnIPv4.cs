using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnIPv4
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_ipv4_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_ipv4_append(nint column, uint value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial uint chc_column_ipv4_at(nint column, nuint index);
}