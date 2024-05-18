using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native;

internal static partial class NativeBlock
{
    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint CreateBlock();

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial void FreeBlock(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge.dll", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void AppendColumn(nint block, string name, nint column);

    [LibraryImport("clickhouse-cpp-wrapper.dll")]
    public static partial nuint GetColumnCount(nint block);

    // info
    // setInfo

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nuint GetRowCount(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nuint RefreshRowCount(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge.dll", StringMarshalling = StringMarshalling.Utf8)]
    public static partial string GetColumnName(nint block, nuint index);

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint GetColumnAt(nint block, nuint index);
}
