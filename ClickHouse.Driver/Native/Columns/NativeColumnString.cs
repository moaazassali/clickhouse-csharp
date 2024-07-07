using System.Runtime.InteropServices;
using ClickHouse.Driver.Native.Structs;

namespace ClickHouse.Driver.Native.Columns;

internal static partial class NativeColumnString
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_string_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_string_append(nint column, [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeStringView chc_column_string_at(nint column, nuint index);
}