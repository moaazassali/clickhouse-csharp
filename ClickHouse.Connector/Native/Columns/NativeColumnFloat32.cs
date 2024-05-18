using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnFloat32
{
    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint CreateColumnFloat32();

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial void ColumnFloat32Append(nint column, float value);
}
