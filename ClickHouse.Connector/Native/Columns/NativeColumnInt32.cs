using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnInt32
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_int32_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_int32_append(nint column, int value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial int chc_column_int32_at(nint column, nuint index);
}