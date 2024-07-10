using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnIPv6Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nint chc_column_ipv6_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_column_ipv6_append(nint column,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)]
        ReadOnlySpan<byte> value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial byte[] chc_column_ipv6_at(nint column, nuint index);
}