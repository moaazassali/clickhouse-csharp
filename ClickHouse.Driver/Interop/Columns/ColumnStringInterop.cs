using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnStringInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_string_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_string_append(nint column, [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial StringViewInterop chc_column_string_at(nint column, nuint index);
}