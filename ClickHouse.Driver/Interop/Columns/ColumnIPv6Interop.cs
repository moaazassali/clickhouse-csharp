using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnIPv6Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nint chc_column_ipv6_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_column_ipv6_append(nint column, In6AddrInterop value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial In6AddrInterop chc_column_ipv6_at(nint column, nuint index);
}