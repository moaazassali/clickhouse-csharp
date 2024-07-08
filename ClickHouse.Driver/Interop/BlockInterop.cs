using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop;

internal static partial class BlockInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_block_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_block_free(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void chc_block_append_column(nint block, string name, nint column);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nuint chc_block_column_count(nint block);

    // info
    // setInfo

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nuint chc_block_row_count(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nuint chc_block_refresh_row_count(nint block);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    public static partial string chc_block_column_name(nint block, nuint index);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_block_column_at(nint block, nuint index);
}