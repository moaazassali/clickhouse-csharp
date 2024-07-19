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

    internal abstract void Add(object value);
    public abstract object At(int index);

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
    private readonly IColumn<T> _column;

    public Column()
    {
        if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
        {
            var col = new BaseColumn<T>();
            NativeColumn = col.NativeColumn;
            _column = col;
        }
        else if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
        {
            var col = new NullableColumn<T>();
            NativeColumn = col.NativeColumn;
            _column = col;
        }
        else if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
        {
            var col = new LowCardinalityColumn<T>();
            NativeColumn = col.NativeColumn;
            _column = col;
        }
        else if (typeof(IChArray).IsAssignableFrom(typeof(T)))
        {
            var col = new ArrayColumn<T>();
            NativeColumn = col.NativeColumn;
            _column = col;
        }
        else throw new NotSupportedException(typeof(T).ToString());
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

        if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
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

            if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
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