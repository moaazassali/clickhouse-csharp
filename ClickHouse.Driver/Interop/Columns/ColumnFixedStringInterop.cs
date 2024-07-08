using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnFixedStringInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nint chc_column_fixed_string_create(nuint size);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial ResultStatusInterop chc_column_fixed_string_append(nint column,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial StringViewInterop chc_column_fixed_string_at(nint column, nuint index);
}