using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnFixedString
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nint chc_column_fixed_string_create(nuint size);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial NativeResultStatus chc_column_fixed_string_append(nint column,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial NativeStringView chc_column_fixed_string_at(nint column, nuint index);
}