using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Native.Columns;

internal static partial class NativeColumnUInt64
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uint64_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uint64_append(nint column, ulong value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ulong chc_column_uint64_at(nint column, nuint index);
}