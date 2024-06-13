using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnDate32
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnDate32();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnDate32Append(nint column, int value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial int ColumnDate32At(nint column, nint index);
}