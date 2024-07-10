using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Interop.Columns;

internal static partial class ColumnLowCardinalityInterop
{
    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial ResultStatusInterop chc_column_low_cardinality_create(nint inColumn, out nint outColumn);

    [LibraryImport("clickhouse-cpp-c-bridge", StringMarshalling = StringMarshalling.Utf8)]
    internal static partial ResultStatusInterop chc_column_low_cardinality_append(nint column, string value);

    [LibraryImport("clickhouse-cpp-c-bridge")]
    internal static partial OptionalInterop chc_column_low_cardinality_at(nint column, nuint index);
}