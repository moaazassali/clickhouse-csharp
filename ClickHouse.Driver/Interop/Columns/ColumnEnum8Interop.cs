using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnEnum8Interop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nint chc_column_enum8_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial void chc_column_enum8_append(nint column, sbyte value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial sbyte chc_column_enum8_at(nint column, nuint index);
}