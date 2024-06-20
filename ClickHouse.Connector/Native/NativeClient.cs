using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Native;

internal static partial class NativeClient
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus CreateClient(ref NativeClientOptions options, out nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void FreeClient(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial NativeClickHouseResultStatus Execute(nint client, nint query);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeSelectCallback(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus Select(nint client, nint query,
        [MarshalAs(UnmanagedType.FunctionPtr)] NativeSelectCallback selectCallback);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool NativeSelectCancelableCallback(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus Select(nint client, nint query,
        [MarshalAs(UnmanagedType.FunctionPtr)] NativeSelectCancelableCallback selectCallback);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial NativeClickHouseResultStatus Insert(nint client, string tableName, nint block);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial NativeClickHouseResultStatus InsertWithQueryId(nint client, string tableName, string queryId,
        nint block);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus Ping(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus ResetConnection(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeServerInfo GetServerInfo(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeEndpoint GetCurrentEndpoint(nint client);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus ResetConnectionEndpoint(nint client);

    // no need to implement GetVersion() as it references static variables in the C++ library
    // we can just implement it directly in C#
}