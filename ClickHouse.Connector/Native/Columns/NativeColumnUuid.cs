using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUuid
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnUuid();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnUuidAppend(nint column, NativeUuid value);
    
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeUuid ColumnUuidAt(nint column, nint index);
}