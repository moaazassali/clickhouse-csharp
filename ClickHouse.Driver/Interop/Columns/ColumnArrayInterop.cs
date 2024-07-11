using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnArrayInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial ResultStatusInterop chc_column_array_create(nint inColumn, out nint outColumn);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial int chc_column_array_item_type(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_column_array_add_offset(nint column, nuint offset);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nuint chc_column_array_get_offset(nint column, nuint n);
}