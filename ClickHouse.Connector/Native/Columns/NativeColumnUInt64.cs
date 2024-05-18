using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt64
{
    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint CreateColumnUInt64();

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial void ColumnUInt64Append(nint column, ulong value);
}