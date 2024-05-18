﻿using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Native.Columns;

internal static partial class NativeColumnDateTime
{
    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial nint CreateColumnDateTime();

    [LibraryImport("clickhouse-cpp-c-bridge.dll")]
    public static partial void ColumnDateTimeAppend(nint column, long value);
}
