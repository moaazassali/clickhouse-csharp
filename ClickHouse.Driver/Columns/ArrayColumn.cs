using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

internal interface IArrayColumn : IColumn
{
    Column BaseColumn { get; set; }
}

internal class ArrayColumn<T> : Column, IArrayColumn, IColumn<T> where T : struct, IChType
{
    private readonly Column _nestedColumn;
    public Column BaseColumn { get; set; }

    internal ArrayColumn()
    {
        T value = default;
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(ChArray<>))
        {
            var elementType = typeof(T).GenericTypeArguments[0]; // This gets the U in ChArray<U>

            // Dynamically create the correct column type based on elementType
            if (typeof(IChBaseType).IsAssignableFrom(elementType))
            {
                var columnType = typeof(BaseColumn<>).MakeGenericType(elementType);
                _nestedColumn = (Column)Activator.CreateInstance(columnType);
                BaseColumn = _nestedColumn;
            }
            else if (typeof(IChNullable).IsAssignableFrom(elementType))
            {
                var columnType = typeof(NullableColumn<>).MakeGenericType(elementType);
                _nestedColumn = (Column)Activator.CreateInstance(columnType);
                BaseColumn = _nestedColumn;
            }
            else if (typeof(IChLowCardinality).IsAssignableFrom(elementType))
            {
                var columnType = typeof(LowCardinalityColumn<>).MakeGenericType(elementType);
                _nestedColumn = (Column)Activator.CreateInstance(columnType);
                BaseColumn = _nestedColumn;
            }
            else if (typeof(IChArray).IsAssignableFrom(elementType))
            {
                var columnType = typeof(ArrayColumn<>).MakeGenericType(elementType);
                var nestedColumn = Activator.CreateInstance(columnType);
                _nestedColumn = (Column)nestedColumn;
                BaseColumn = ((IArrayColumn)nestedColumn).BaseColumn;
            }

            var resultStatus =
                ColumnArrayInterop.chc_column_array_create(_nestedColumn.NativeColumn, out var nativeColumn);

            if (resultStatus.Code != 0)
            {
                throw new ClickHouseException(resultStatus);
            }

            NativeColumn = nativeColumn;
        }
    }

    internal ArrayColumn(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(T value)
    {
        CheckDisposed();
    }

    public T this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return default;
        }
    }
}