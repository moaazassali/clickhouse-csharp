using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnFloat32
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint CreateColumnFloat32();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void ColumnFloat32Append(nint column, float value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial float ColumnFloat32At(nint column, nint index);
}