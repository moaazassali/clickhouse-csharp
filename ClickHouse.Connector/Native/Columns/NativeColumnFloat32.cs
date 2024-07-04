using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnFloat32
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_float32_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_float32_append(nint column, float value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial float chc_column_float32_at(nint column, nuint index);
}