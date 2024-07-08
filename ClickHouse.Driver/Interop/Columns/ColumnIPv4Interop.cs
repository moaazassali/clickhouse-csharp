using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnIPv4Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_ipv4_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_ipv4_append(nint column, uint value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial uint chc_column_ipv4_at(nint column, nuint index);
}