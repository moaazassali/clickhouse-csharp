using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnUInt64Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uint64_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uint64_append(nint column, ulong value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ulong chc_column_uint64_at(nint column, nuint index);
}