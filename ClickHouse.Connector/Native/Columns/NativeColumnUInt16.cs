using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnUInt16
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnUInt16();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnUInt16Append(nint column, ushort value);
}