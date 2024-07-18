namespace ClickHouse.Driver.Columns;

public interface IColumn : IDisposable
{
    ColumnType Type { get; }
    void Reserve(int size);
    void Clear();
    int Count { get; }
    object At(int index);
}

public abstract class Column : IColumn
{
    protected internal nint NativeColumn { get; protected init; }

    private bool _disposed;

    public ColumnType Type
    {
        get
        {
            CheckDisposed();
            return Interop.Columns.ColumnInterop.chc_column_type_code(NativeColumn);
        }
    }

    public void Reserve(int size)
    {
        CheckDisposed();
        Interop.Columns.ColumnInterop.chc_column_reserve(NativeColumn, (nuint)size);
    }

    public void Clear()
    {
        CheckDisposed();
        Interop.Columns.ColumnInterop.chc_column_clear(NativeColumn);
    }

    public int Count
    {
        get
        {
            CheckDisposed();
            return (int)Interop.Columns.ColumnInterop.chc_column_size(NativeColumn);
        }
    }

    internal virtual void Add(object value) => throw new NotImplementedException();
    public virtual object At(int index) => throw new NotImplementedException();

    public void Dispose()
    {
        Interop.Columns.ColumnInterop.chc_column_free(NativeColumn);
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~Column()
    {
        Dispose();
    }

    protected void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

public interface IColumn<T> : IColumn
{
    void Add(T value);
    T this[int index] { get; }
}

public class Column<T> : Column, IColumn<T> where T : IChType
{
    public readonly IColumn<T> _column;

    public Column()
    {
        if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
        {
            var baseCol = new BaseColumn<T>();
            NativeColumn = baseCol.NativeColumn;
            _column = baseCol;
            return;
        }

        if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
        {
            var nullCol = new NullableColumn<T>();
            NativeColumn = nullCol.NativeColumn;
            _column = nullCol;
            return;
        }

        if (typeof(ChLowCardinality<>).IsAssignableFrom(typeof(T)))
        {
            var lcCol = new LowCardinalityColumn<T>();
            NativeColumn = lcCol.NativeColumn;
            _column = lcCol;
            return;
        }

        if (typeof(IChArray).IsAssignableFrom(typeof(T)))
        {
            var arrayCol = new ArrayColumn<T>();
            NativeColumn = arrayCol.NativeColumn;
            _column = arrayCol;
            return;
        }

        throw new NotSupportedException(typeof(T).ToString());
    }


// second argument is a dummy to distinguish from the constructor above internally within the library
// internal Column(nint nativeColumn, bool _)
// {
//     NativeColumn = nativeColumn;
// }

    internal override void Add(object value) => Add((T)value);

    public void Add(T value)
    {
        if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
        {
            _column.Add(value);
            return;
        }

        if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
        {
            _column.Add(value);
            return;
        }

        if (typeof(ChLowCardinality<>).IsAssignableFrom(typeof(T)))
        {
            _column.Add(value);
            return;
        }

        if (typeof(IChArray).IsAssignableFrom(typeof(T)))
        {
            _column.Add(value);
            return;
        }

        throw new NotSupportedException(typeof(T).ToString());
    }

    public override object At(int index)
    {
        return this[index];
    }

    public T this[int index]
    {
        get
        {
            if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
            {
                return _column[index];
            }

            if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
            {
                return _column[index];
            }

            if (typeof(ChLowCardinality<>).IsAssignableFrom(typeof(T)))
            {
                return _column[index];
            }

            if (typeof(IChArray).IsAssignableFrom(typeof(T)))
            {
                return _column[index];
            }

            throw new NotSupportedException(typeof(T).ToString());
        }
    }
}