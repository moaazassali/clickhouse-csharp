using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnDateTime64
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_datetime64_create(nuint precision);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_datetime64_append(nint column, long value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial long chc_column_datetime64_at(nint column, nuint index);
}