using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnInt16
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnInt16();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnInt16Append(nint column, short value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial short ColumnInt16At(nint column, nint index);
}