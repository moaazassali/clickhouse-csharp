using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native;

internal static partial class NativeQuery
{
    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial nint CreateQuery(string query);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial nint CreateQuery(string query, string queryId);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void FreeQuery(nint query);
}