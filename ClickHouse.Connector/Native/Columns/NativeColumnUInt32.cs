using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt32
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnUInt32();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnUInt32Append(nint column, uint value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial uint ColumnUInt32At(nint column, nint index);
}