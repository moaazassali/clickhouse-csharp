namespace ClickHouse.Driver.Columns;

public interface IColumn : IDisposable
{
    ColumnType Type { get; }
    void Reserve(int size);
    void Clear();
    int Count { get; }
}

public abstract class Column : IColumn
{
    internal abstract NativeColumnWrapper NativeColumnWrapper { get; }

    public ColumnType Type => NativeColumnWrapper.Type;

    public void Reserve(int size) => NativeColumnWrapper.Reserve(size);

    public void Clear() => NativeColumnWrapper.Clear();

    public int Count => NativeColumnWrapper.Count;

    public void Dispose()
    {
        NativeColumnWrapper.Dispose();
    }
}

public sealed class Column<T> : Column where T : IChType
{
    private readonly NativeColumnWrapper<T> _nativeColumnWrapper;

    internal override NativeColumnWrapper NativeColumnWrapper => _nativeColumnWrapper;

    public Column()
    {
        if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
        {
            var col = new BaseColumn<T>();
            _nativeColumnWrapper = col;
        }
        else if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
        {
            var col = new NullableColumn<T>();
            _nativeColumnWrapper = col;
        }
        else if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
        {
            var col = new LowCardinalityColumn<T>();
            _nativeColumnWrapper = col;
        }
        else if (typeof(IChArray).IsAssignableFrom(typeof(T)))
        {
            var col = new ArrayColumn<T>();
            _nativeColumnWrapper = col;
        }
        else throw new NotSupportedException(typeof(T).ToString());
    }

    public void Add(T value)
    {
        // Calling _column.Add(value) directly is much slow than the code below, around 2x slower
        if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
        {
            ((BaseColumn<T>)_nativeColumnWrapper).Add(value);
        }

        else if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
        {
            ((NullableColumn<T>)_nativeColumnWrapper).Add(value);
        }

        else if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
        {
            ((LowCardinalityColumn<T>)_nativeColumnWrapper).Add(value);
        }

        else if (typeof(IChArray).IsAssignableFrom(typeof(T)))
        {
            ((ArrayColumn<T>)_nativeColumnWrapper).Add(value);
        }
    }

    public T this[int index]
    {
        get
        {
            if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
            {
                return _nativeColumnWrapper[index];
            }

            if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
            {
                return _nativeColumnWrapper[index];
            }

            if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
            {
                return _nativeColumnWrapper[index];
            }

            if (typeof(IChArray).IsAssignableFrom(typeof(T)))
            {
                return _nativeColumnWrapper[index];
            }

            throw new NotSupportedException(typeof(T).ToString());
        }
    }
}