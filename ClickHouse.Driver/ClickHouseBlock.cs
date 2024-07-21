using ClickHouse.Driver.Columns;
using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver;

public class ClickHouseBlock : IDisposable
{
    internal nint NativeBlock { get; }

    private bool _disposed;
    private readonly bool _isOwnedByUnmanagedCode;

    // we have to keep references to the columns to prevent them from being collected by GC
    // which frees the native columns through Dispose() and causes a crash
    public List<Column> Columns { get; } = [];

    // again, possible conversion loss from nuint to int
    // will anyone have more than 2B rows in one block?
    public int RowCount
    {
        get
        {
            CheckDisposed();
            return (int)Interop.BlockInterop.chc_block_row_count(NativeBlock);
        }
    }

    public ClickHouseBlock()
    {
        NativeBlock = Interop.BlockInterop.chc_block_create();
    }

    public ClickHouseBlock(nint nativeBlock)
    {
        NativeBlock = nativeBlock;
        _isOwnedByUnmanagedCode = true;

        for (nuint i = 0; i < Interop.BlockInterop.chc_block_column_count(nativeBlock); i++)
        {
            var nativeColumn = Interop.BlockInterop.chc_block_column_at(nativeBlock, i);
            Columns.Add(CreateColumn(ColumnInterop.chc_column_type_code(nativeColumn), nativeColumn));
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (_isOwnedByUnmanagedCode)
        {
            return;
        }

        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
        }

        Interop.BlockInterop.chc_block_free(NativeBlock);
        _disposed = true;
    }

    ~ClickHouseBlock()
    {
        Dispose(false);
    }

    private void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public void AppendColumn(string columnName, Column column)
    {
        CheckDisposed();
        Columns.Add(column);
        Interop.BlockInterop.chc_block_append_column(NativeBlock, columnName, column.NativeColumnWrapper.NativeColumn);
    }

    public nuint RefreshRowCount()
    {
        CheckDisposed();
        return Interop.BlockInterop.chc_block_refresh_row_count(NativeBlock);
    }

    public string GetColumnName(nuint index)
    {
        CheckDisposed();
        return Interop.BlockInterop.chc_block_column_name(NativeBlock, index);
    }

    private static Column CreateColumn(ColumnType type, nint nativeColumn)
    {
        return type switch
        {
            ColumnType.UInt8 => new Column<ChUInt8>(nativeColumn, default),
            ColumnType.UInt16 => new Column<ChUInt16>(nativeColumn, default),
            ColumnType.UInt32 => new Column<ChUInt32>(nativeColumn, default),
            ColumnType.UInt64 => new Column<ChUInt64>(nativeColumn, default),
            ColumnType.Int8 => new Column<ChInt8>(nativeColumn, default),
            ColumnType.Int16 => new Column<ChInt16>(nativeColumn, default),
            ColumnType.Int32 => new Column<ChInt32>(nativeColumn, default),
            ColumnType.Int64 => new Column<ChInt64>(nativeColumn, default),
            ColumnType.Float32 => new Column<ChFloat32>(nativeColumn, default),
            ColumnType.Float64 => new Column<ChFloat64>(nativeColumn, default),
            // ColumnType.String => new ColumnString(nativeColumn),
            // ColumnType.FixedString => new ColumnFixedString(nativeColumn),
            // ColumnType.DateTime => new ColumnDateTime(nativeColumn),
            // ColumnType.Date => new ColumnDate(nativeColumn),
            // ClickHouseColumnType.Array => new ClickHouseColumnArray(),
            // ClickHouseColumnType.Nullable => new ClickHouseColumnNullable(),
            // ClickHouseColumnType.Tuple => new ClickHouseColumnTuple(),
            // ClickHouseColumnType.Enum8 => new ClickHouseColumnEnum8(),
            // ClickHouseColumnType.Enum16 => new ClickHouseColumnEnum16(),
            // ColumnType.Uuid => new ColumnUuid(nativeColumn),
            // ColumnType.IPv4 => new ColumnIPv4(nativeColumn),
            // ClickHouseColumnType.IPv6 => new ClickHouseColumnIPv6(),
            // ClickHouseColumnType.Int128 => new ClickHouseColumnInt128(),
            // ClickHouseColumnType.Decimal => new ClickHouseColumnDecimal(),
            // ClickHouseColumnType.Decimal32 => new ClickHouseColumnDecimal32(),
            // ClickHouseColumnType.Decimal64 => new ClickHouseColumnDecimal64(),
            // ClickHouseColumnType.Decimal128 => new ClickHouseColumnDecimal128(),
            // ClickHouseColumnType.LowCardinality => new ClickHouseColumnLowCardinality(),
            ColumnType.DateTime64 => new Column<ChDateTime64>(nativeColumn, default),
            // ColumnType.Date32 => new ColumnDate32(nativeColumn),
            // ClickHouseColumnType.Map => new ClickHouseColumnMap(),
            // ClickHouseColumnType.Point => new ClickHouseColumnPoint(),
            // ClickHouseColumnType.Ring => new ClickHouseColumnRing(),
            // ClickHouseColumnType.Polygon => new ClickHouseColumnPolygon(),
            // ClickHouseColumnType.MultiPolygon => new ClickHouseColumnMultiPolygon(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}