using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt32
{
    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint CreateColumnUInt32();

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial void ColumnUInt32Append(nint column, uint value);
}