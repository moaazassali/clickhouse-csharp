using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnInt64
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnInt64();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnInt64Append(nint column, long value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial long ColumnInt64At(nint column, nint index);
}