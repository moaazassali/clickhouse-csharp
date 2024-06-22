using ClickHouse.Connector.Connector.ClickHouseColumns;

namespace ClickHouse.Connector.Connector;

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
            return (int)Native.NativeBlock.GetRowCount(NativeBlock);
        }
    }

    public ClickHouseBlock()
    {
        NativeBlock = Native.NativeBlock.CreateBlock();
        _disposed = false;
    }

    public ClickHouseBlock(nint nativeBlock)
    {
        NativeBlock = nativeBlock;
        _disposed = false;

        for (nuint i = 0; i < Native.NativeBlock.GetColumnCount(nativeBlock); i++)
        {
            var nativeColumn = Native.NativeBlock.GetColumnAt(nativeBlock, i);
            Columns.Add(CreateColumn(Native.Columns.NativeColumn.GetColumnType(nativeColumn), nativeColumn));
        }
    }

    public void Dispose()
    {
        Native.NativeBlock.FreeBlock(NativeBlock);
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
        Native.NativeBlock.AppendColumn(NativeBlock, columnName, column.NativeColumn);
    }

    public nuint RefreshRowCount()
    {
        CheckDisposed();
        return Native.NativeBlock.RefreshRowCount(NativeBlock);
    }

    public string GetColumnName(nuint index)
    {
        CheckDisposed();
        return Native.NativeBlock.GetColumnName(NativeBlock, index);
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
            // ClickHouseColumnType.DateTime => new ClickHouseColumnDateTime(),
            // ClickHouseColumnType.Date => new ClickHouseColumnDate(),
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
            // ClickHouseColumnType.Date32 => new ClickHouseColumnDate32(),
            // ClickHouseColumnType.Map => new ClickHouseColumnMap(),
            // ClickHouseColumnType.Point => new ClickHouseColumnPoint(),
            // ClickHouseColumnType.Ring => new ClickHouseColumnRing(),
            // ClickHouseColumnType.Polygon => new ClickHouseColumnPolygon(),
            // ClickHouseColumnType.MultiPolygon => new ClickHouseColumnMultiPolygon(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}