﻿using ClickHouse.Driver.Driver.ClickHouseColumns;
using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Driver;

public class ClickHouseBlock : IDisposable
{
    internal nint NativeBlock { get; }
    private bool _disposed;
    public List<ClickHouseColumn> Columns { get; } = [];

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
        _disposed = false;
    }

    public ClickHouseBlock(nint nativeBlock)
    {
        NativeBlock = nativeBlock;
        _disposed = false;

        for (nuint i = 0; i < Interop.BlockInterop.chc_block_column_count(nativeBlock); i++)
        {
            var nativeColumn = Interop.BlockInterop.chc_block_column_at(nativeBlock, i);
            Columns.Add(CreateColumn(ColumnInterop.chc_column_type(nativeColumn), nativeColumn));
        }
    }

    public void Dispose()
    {
        Interop.BlockInterop.chc_block_free(NativeBlock);
        // should columns be disposed here as well?
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~ClickHouseBlock()
    {
        Dispose();
    }

    private void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public void AppendColumn(string columnName, ClickHouseColumn column)
    {
        CheckDisposed();
        Columns.Add(column);
        Interop.BlockInterop.chc_block_append_column(NativeBlock, columnName, column.NativeColumn);
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

    private static ClickHouseColumn CreateColumn(ClickHouseColumnType type, nint nativeColumn)
    {
        return type switch
        {
            ClickHouseColumnType.Int8 => new ClickHouseColumnInt8(nativeColumn),
            ClickHouseColumnType.Int16 => new ClickHouseColumnInt16(nativeColumn),
            ClickHouseColumnType.Int32 => new ClickHouseColumnInt32(nativeColumn),
            ClickHouseColumnType.Int64 => new ClickHouseColumnInt64(nativeColumn),
            ClickHouseColumnType.UInt8 => new ClickHouseColumnUInt8(nativeColumn),
            ClickHouseColumnType.UInt16 => new ClickHouseColumnUInt16(nativeColumn),
            ClickHouseColumnType.UInt32 => new ClickHouseColumnUInt32(nativeColumn),
            ClickHouseColumnType.UInt64 => new ClickHouseColumnUInt64(nativeColumn),
            ClickHouseColumnType.Float32 => new ClickHouseColumnFloat32(nativeColumn),
            ClickHouseColumnType.Float64 => new ClickHouseColumnFloat64(nativeColumn),
            ClickHouseColumnType.String => new ClickHouseColumnString(nativeColumn),
            ClickHouseColumnType.FixedString => new ClickHouseColumnFixedString(nativeColumn),
            ClickHouseColumnType.DateTime => new ClickHouseColumnDateTime(nativeColumn),
            ClickHouseColumnType.Date => new ClickHouseColumnDate(nativeColumn),
            // ClickHouseColumnType.Array => new ClickHouseColumnArray(),
            // ClickHouseColumnType.Nullable => new ClickHouseColumnNullable(),
            // ClickHouseColumnType.Tuple => new ClickHouseColumnTuple(),
            // ClickHouseColumnType.Enum8 => new ClickHouseColumnEnum8(),
            // ClickHouseColumnType.Enum16 => new ClickHouseColumnEnum16(),
            ClickHouseColumnType.Uuid => new ClickHouseColumnUuid(nativeColumn),
            ClickHouseColumnType.IPv4 => new ClickHouseColumnIPv4(nativeColumn),
            // ClickHouseColumnType.IPv6 => new ClickHouseColumnIPv6(),
            // ClickHouseColumnType.Int128 => new ClickHouseColumnInt128(),
            // ClickHouseColumnType.Decimal => new ClickHouseColumnDecimal(),
            // ClickHouseColumnType.Decimal32 => new ClickHouseColumnDecimal32(),
            // ClickHouseColumnType.Decimal64 => new ClickHouseColumnDecimal64(),
            // ClickHouseColumnType.Decimal128 => new ClickHouseColumnDecimal128(),
            // ClickHouseColumnType.LowCardinality => new ClickHouseColumnLowCardinality(),
            ClickHouseColumnType.DateTime64 => new ClickHouseColumnDateTime64(nativeColumn),
            ClickHouseColumnType.Date32 => new ClickHouseColumnDate32(nativeColumn),
            // ClickHouseColumnType.Map => new ClickHouseColumnMap(),
            // ClickHouseColumnType.Point => new ClickHouseColumnPoint(),
            // ClickHouseColumnType.Ring => new ClickHouseColumnRing(),
            // ClickHouseColumnType.Polygon => new ClickHouseColumnPolygon(),
            // ClickHouseColumnType.MultiPolygon => new ClickHouseColumnMultiPolygon(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}