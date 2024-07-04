using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt32
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uint32_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uint32_append(nint column, uint value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial uint chc_column_uint32_at(nint column, nuint index);
}