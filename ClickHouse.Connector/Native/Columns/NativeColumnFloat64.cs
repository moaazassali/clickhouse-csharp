using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnFloat64
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnFloat64();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnFloat64Append(nint column, double value);
}