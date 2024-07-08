using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnDateTimeInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_datetime_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_datetime_append(nint column, uint value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial int chc_column_datetime_at(nint column, nuint index);
}