using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnNullableInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial ResultStatusInterop chc_column_nullable_create(nint inColumn, out nint outColumn);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial ResultStatusInterop chc_column_nullable_append(nint column, nint value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial OptionalInterop chc_column_nullable_at(nint column, nuint index);
}