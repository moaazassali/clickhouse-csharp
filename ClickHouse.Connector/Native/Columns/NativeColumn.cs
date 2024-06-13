using System.Runtime.InteropServices;
using ClickHouse.Connector.Connector.ClickHouseColumns;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumn
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void FreeColumn(nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ClickHouseColumnType GetColumnType(nint column);
}