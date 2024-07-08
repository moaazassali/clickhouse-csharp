using System.Runtime.InteropServices;
using ClickHouse.Driver.Driver.ClickHouseColumns;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_free(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ClickHouseColumnType chc_column_type(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_reserve(nint column, nuint size);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_clear(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nuint chc_column_size(nint column);
}