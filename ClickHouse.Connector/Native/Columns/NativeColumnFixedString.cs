using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnFixedString
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial nint CreateColumnFixedString(nint size);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial NativeClickHouseResultStatus ColumnFixedStringAppend(nint column, [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial NativeStringView ColumnFixedStringAt(nint column, nint index);
}