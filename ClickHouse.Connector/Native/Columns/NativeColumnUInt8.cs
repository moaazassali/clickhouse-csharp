using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt8
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uint8_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uint8_append(nint column, byte value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial byte chc_column_uint8_at(nint column, nuint index);
}