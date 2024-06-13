using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnInt8
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnInt8();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnInt8Append(nint column, sbyte value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial sbyte ColumnInt8At(nint column, nint index);
}