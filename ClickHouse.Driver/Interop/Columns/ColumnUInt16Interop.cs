using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnUInt16Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uint16_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uint16_append(nint column, ushort value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ushort chc_column_uint16_at(nint column, nuint index);
}