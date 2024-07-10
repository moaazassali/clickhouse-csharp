using System.Runtime.InteropServices;
using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_free(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ColumnType chc_column_type_code(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial string chc_column_type_name(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_reserve(nint column, nuint size);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_clear(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nuint chc_column_size(nint column);
}