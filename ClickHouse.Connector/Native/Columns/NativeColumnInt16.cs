using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnInt16
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnInt16();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnInt16Append(nint column, short value);
}