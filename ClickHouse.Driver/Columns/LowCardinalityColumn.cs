using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

internal class LowCardinalityColumn<T> : NativeColumnWrapper<T>
{
    internal LowCardinalityColumn(uint? a, uint? b) : base(a, b)
    {
        T value = default;
        NativeColumnWrapper nestedColumn;

        switch (value)
        {
            case ChLowCardinality<ChString>:
                nestedColumn = new BaseColumn<ChString>(null, null);
                break;
            case ChLowCardinality<ChFixedString>:
                nestedColumn = new BaseColumn<ChFixedString>(null, null);
                break;
            case ChLowCardinality<ChNullable<ChString>>:
                nestedColumn = new NullableColumn<ChNullable<ChString>>(null, null);
                break;
            case ChLowCardinality<ChNullable<ChFixedString>>:
                nestedColumn = new NullableColumn<ChNullable<ChFixedString>>(null, null);
                break;
            default: throw new NotSupportedException();
        }

        ColumnLowCardinalityInterop.chc_column_low_cardinality_create(nestedColumn.NativeColumn, out var outColumn);
        // we are not keeping a reference to nestedColumn; instead, we are disposing it.
        // this is because the interop method will use it to create a copy of the column. we have no further use for it.
        nestedColumn.Dispose();
        NativeColumn = outColumn;
    }

    internal LowCardinalityColumn(nint nativeColumn) : base(nativeColumn)
    {
    }

    internal override void Add(T value)
    {
        CheckDisposed();

        switch (value)
        {
            case ChLowCardinality<ChString> str:
                ColumnLowCardinalityInterop.chc_column_low_cardinality_append(NativeColumn, (ChString)str);
                break;
            case ChLowCardinality<ChFixedString> fixedStr:
                ColumnLowCardinalityInterop.chc_column_low_cardinality_append(NativeColumn, (ChFixedString)fixedStr);
                break;
            case ChLowCardinality<ChNullable<ChString>> nullableStr:
                ColumnLowCardinalityInterop.chc_column_low_cardinality_append(NativeColumn,
                    nullableStr.Value == null ? null! : nullableStr.Value.Value);
                break;
            case ChLowCardinality<ChNullable<ChFixedString>> nullableFixedStr:
                ColumnLowCardinalityInterop.chc_column_low_cardinality_append(NativeColumn,
                    nullableFixedStr.Value == null ? null! : nullableFixedStr.Value.Value);
                break;
            default: throw new NotSupportedException();
        }
    }

    internal override T this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var optional = ColumnLowCardinalityInterop.chc_column_low_cardinality_at(NativeColumn, (nuint)index);

            T value = default;
            switch (value)
            {
                case ChLowCardinality<ChString>:
                    return (T)(object)(ChLowCardinality<ChString>)(ChString)optional.Value.StringView.ToString();
                case ChLowCardinality<ChFixedString>:
                    return (T)(object)(ChLowCardinality<ChFixedString>)
                        (ChFixedString)optional.Value.StringView.ToString();
                case ChLowCardinality<ChNullable<ChString>>:
                    if (optional.Type == OptionalTypeInterop.Null)
                    {
                        return (T)(object)(ChLowCardinality<ChNullable<ChString>>)(ChNullable<ChString>)null;
                    }

                    return (T)(object)(ChLowCardinality<ChNullable<ChString>>)
                        (ChNullable<ChString>)(ChString)optional.Value.StringView.ToString();
                case ChLowCardinality<ChNullable<ChFixedString>>:
                    if (optional.Type == OptionalTypeInterop.Null)
                    {
                        return (T)(object)(ChLowCardinality<ChNullable<ChFixedString>>)(ChNullable<ChFixedString>)null;
                    }

                    return (T)(object)(ChLowCardinality<ChNullable<ChFixedString>>)
                        (ChNullable<ChFixedString>)(ChFixedString)optional.Value.StringView.ToString();
            }

            throw new NotSupportedException();
        }
    }
}