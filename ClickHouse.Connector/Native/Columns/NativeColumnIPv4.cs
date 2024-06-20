using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnIPv4
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnIPv4();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnIPv4Append(nint column, uint value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial uint ColumnIPv4At(nint column, nint index);
}