using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt8
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnUInt8();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnUInt8Append(nint column, byte value);
}