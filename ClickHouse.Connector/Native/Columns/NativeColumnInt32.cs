using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnInt32
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnInt32();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnInt32Append(nint column, int value);
}