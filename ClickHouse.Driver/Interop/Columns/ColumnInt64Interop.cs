using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnInt64Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_int64_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_int64_append(nint column, long value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial long chc_column_int64_at(nint column, nuint index);
}