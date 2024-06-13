using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnDate
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnDate();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnDateAppend(nint column, ushort value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ushort ColumnDateAt(nint column, nint index);
}