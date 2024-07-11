using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnLowCardinality<T> : Column, IColumn<string> where T : Column, IColumn<string>
{
    public ColumnLowCardinality(int? size = null)
    {
        T nestedColumn;
        if (typeof(T) == typeof(ColumnString))
        {
            if (size.HasValue)
            {
                throw new ArgumentException("Size is not supported for ColumnString");
            }

            nestedColumn = (T)(object)new ColumnString();
        }
        else if (typeof(T) == typeof(ColumnFixedString))
        {
            ArgumentNullException.ThrowIfNull(size);
            nestedColumn = (T)(object)new ColumnFixedString(size.Value);
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

    public void Add(string value)
    {
        CheckDisposed();
        var resultStatus = ColumnLowCardinalityInterop.chc_column_low_cardinality_append(NativeColumn, value);

        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }
    }

    public string this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var optional = ColumnLowCardinalityInterop.chc_column_low_cardinality_at(NativeColumn, (nuint)index);
            return optional.Value.StringView.ToString();
        }
    }
}