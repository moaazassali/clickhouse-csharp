using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnUuidInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uuid_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uuid_append(nint column, UuidInterop value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial UuidInterop chc_column_uuid_at(nint column, nuint index);
}