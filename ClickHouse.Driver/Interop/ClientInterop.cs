using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop;

internal static partial class ClientInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ResultStatusInterop chc_client_create(ref ClientOptionsInterop options,
        out nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_client_free(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial ResultStatusInterop chc_client_execute(nint client, nint query);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial ResultStatusInterop chc_client_insert(nint client, string tableName, nint block);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial ResultStatusInterop chc_client_insert_with_query_id(nint client, string tableName,
        string queryId,
        nint block);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ResultStatusInterop chc_client_ping(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ResultStatusInterop chc_client_reset_connection(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ServerInfoInterop chc_client_get_server_info(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial EndpointInterop chc_client_get_current_endpoint(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial ResultStatusInterop chc_client_reset_connection_endpoint(nint client);

    // no need to implement GetVersion() as it references static variables in the C++ library
    // we can just implement it directly in C#
}