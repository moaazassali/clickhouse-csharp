using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ColumnLowCardinality<T> : Column where T : struct, IChTypeSupportsLowCardinality
{
    public ColumnLowCardinality(int? size = null)
    {
        Column nestedColumn;

        if (typeof(T) == typeof(ChString))
        {
            if (size.HasValue)
            {
                throw new ArgumentException("Size is not supported for ColumnString");
            }

            nestedColumn = new Column<ChString>();
        }
        else if (typeof(T) == typeof(ChFixedString))
        {
            ArgumentNullException.ThrowIfNull(size);
            nestedColumn = new Column<ChFixedString>();
        }
        else if (typeof(T) == typeof(ChNullable<ChString>))
        {
            if (size.HasValue)
            {
                throw new ArgumentException("Size is not supported for ColumnString");
            }

            nestedColumn = new ColumnNullable<ChString>();
        }
        else if (typeof(T) == typeof(ChNullable<ChFixedString>))
        {
            ArgumentNullException.ThrowIfNull(size);
            nestedColumn = new ColumnNullable<ChFixedString>(size.Value);
        }
        else
        {
            throw new NotSupportedException();
        }

        ColumnLowCardinalityInterop.chc_column_low_cardinality_create(nestedColumn.NativeColumn, out var outColumn);

        // we are not keeping a reference to nestedColumn; instead, we are disposing it.
        // this is because the interop method will use it to create a copy of the column. we have no further use for it.
        nestedColumn.Dispose();
        NativeColumn = outColumn;
    }

    internal ColumnLowCardinality(nint nativeColumn, bool _)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(string? value)
    {
        CheckDisposed();

        var resultStatus = ColumnLowCardinalityInterop.chc_column_low_cardinality_append(NativeColumn, value);

        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }
    }

    public string? this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var optional = ColumnLowCardinalityInterop.chc_column_low_cardinality_at(NativeColumn, (nuint)index);
            if (optional.Type == OptionalTypeInterop.Invalid)
            {
                throw new ArgumentException("Received invalid OptionalType.");
            }

            return optional.Type == OptionalTypeInterop.Null ? null : optional.Value.StringView.ToString();
        }
    }
}