using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnEnum16Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nint chc_column_enum16_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_column_enum16_append(nint column, short value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial short chc_column_enum16_at(nint column, nuint index);
}