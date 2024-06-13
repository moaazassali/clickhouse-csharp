using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt64
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnUInt64();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnUInt64Append(nint column, ulong value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ulong ColumnUInt64At(nint column, nint index);
}