using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Native;

internal static partial class NativeClient
{
    [LibraryImport("clickhouse-cpp-wrapper.dll", StringMarshalling = StringMarshalling.Utf8)]
    public static partial nint CreateClient(string host);

    // [DllImport("clickhouse-cpp-wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    // public static extern nint CreateClient([MarshalAs(UnmanagedType.LPStr)] string host);

    [LibraryImport("clickhouse-cpp-wrapper.dll")]
    public static partial void FreeClient(nint client);

    [LibraryImport("clickhouse-cpp-wrapper.dll", StringMarshalling = StringMarshalling.Utf8)]
    public static partial NativeClickHouseResultStatus Execute(nint client, nint query);

    // Select
    // SelectWithQueryId
    // SelectCancelable
    // SelectCancelableWithQueryId

    [LibraryImport("clickhouse-cpp-wrapper.dll", StringMarshalling = StringMarshalling.Utf8)]
    public static partial NativeClickHouseResultStatus Insert(nint client, string tableName, nint block);

    [LibraryImport("clickhouse-cpp-wrapper.dll", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void InsertWithQueryId(nint client, string tableName, string queryId, nint block);

    [LibraryImport("clickhouse-cpp-wrapper.dll")]
    public static partial void Ping(nint client);

    [LibraryImport("clickhouse-cpp-wrapper.dll")]
    public static partial void ResetConnection(nint client);

    // GetServerInfo
    // GetCurrentEndpoint

    [LibraryImport("clickhouse-cpp-wrapper.dll")]
    public static partial void ResetConnectionEndpoint(nint client);

    // GetVersion
}
