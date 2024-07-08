using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnUInt32Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uint32_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uint32_append(nint column, uint value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial uint chc_column_uint32_at(nint column, nuint index);
}