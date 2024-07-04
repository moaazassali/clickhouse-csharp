using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Native;

internal static partial class NativeQuery
{
    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial nint chc_query_create(string query, string? queryId = null);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_query_free(nint query);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeSelectCallback(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus chc_query_on_data(nint query,
        [MarshalAs(UnmanagedType.FunctionPtr)] NativeSelectCallback selectCallback);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool NativeSelectCancelableCallback(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeClickHouseResultStatus chc_query_on_data_cancelable(nint query,
        [MarshalAs(UnmanagedType.FunctionPtr)] NativeSelectCancelableCallback selectCallback);
}