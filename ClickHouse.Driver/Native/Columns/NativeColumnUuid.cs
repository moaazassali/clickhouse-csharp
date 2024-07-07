using System.Runtime.InteropServices;
using ClickHouse.Driver.Native.Structs;

namespace ClickHouse.Driver.Native.Columns;

internal static partial class NativeColumnUuid
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial nint chc_column_uuid_create();

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial void chc_column_uuid_append(nint column, NativeUuid value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    public static partial NativeUuid chc_column_uuid_at(nint column, nuint index);
}