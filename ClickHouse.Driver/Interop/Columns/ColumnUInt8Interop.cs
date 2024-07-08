using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnUInt8Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uint8_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uint8_append(nint column, byte value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial byte chc_column_uint8_at(nint column, nuint index);
}