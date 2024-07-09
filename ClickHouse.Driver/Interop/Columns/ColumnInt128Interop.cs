using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnInt128Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_int128_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_int128_append(nint column, Int128Interop value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial Int128Interop chc_column_int128_at(nint column, nuint index);
}