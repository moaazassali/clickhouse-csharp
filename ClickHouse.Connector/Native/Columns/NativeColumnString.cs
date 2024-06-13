using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnString
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnString();

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void ColumnStringAppend(nint column, string value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeStringView ColumnStringAt(nint column, nint index);
}