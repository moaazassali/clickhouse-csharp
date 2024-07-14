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

public class Column<T> : Column, IColumn<T> where T : struct, IChType
{
    private readonly IColumn<T> _column;

    public Column()
    {
        T value = default;
        switch (value)
        {
            case IChBaseType:
                var baseCol = new BaseColumn<T>();
                NativeColumn = baseCol.NativeColumn;
                _column = baseCol;
                break;
            case IChNullable:
                var nullCol = new NullableColumn<T>();
                NativeColumn = nullCol.NativeColumn;
                _column = nullCol;
                break;
            case IChLowCardinality:
                var lcCol = new LowCardinalityColumn<T>();
                NativeColumn = lcCol.NativeColumn;
                _column = lcCol;
                break;
            case IChArray:
                var arrayCol = new ArrayColumn<T>();
                NativeColumn = arrayCol.NativeColumn;
                _column = arrayCol;
                break;
            default: throw new ArgumentException(value.GetType().ToString());
        }
    }

    // second argument is a dummy to distinguish from the constructor above internally within the library
    // internal Column(nint nativeColumn, bool _)
    // {
    //     NativeColumn = nativeColumn;
    // }

    public void Add(T value)
    {
        switch (value)
        {
            case IChBaseType:
                ((BaseColumn<T>)_column).Add(value);
                break;
            case IChNullable:
                ((NullableColumn<T>)_column).Add(value);
                break;
            case IChLowCardinality:
                ((LowCardinalityColumn<T>)_column).Add(value);
                break;
            default: throw new ArgumentException(value.GetType().ToString());
        }
    }

    public T this[int index]
    {
        get
        {
            T value = default;
            return value switch
            {
                IChBaseType => _column[index],
                IChNullable => _column[index],
                IChLowCardinality => _column[index],
                _ => throw new ArgumentException(value.GetType().ToString())
            };
        }
    }
}