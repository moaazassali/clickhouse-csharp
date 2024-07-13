namespace ClickHouse.Driver.Columns;

public abstract class Column : IDisposable
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

public interface IColumn<T>
{
    void Add(T value);
    T this[int index] { get; }
}

public class Column<T> : IColumn<T> where T : struct, IChType
{
    private readonly IColumn<T> _column;

    public Column()
    {
        T value = default;
        _column = value switch
        {
            IChBaseType => new BaseColumn<T>(),
            IChNullable => new NullableColumn<T>(),
            IChLowCardinality => new LowCardinalityColumn<T>(),
        };
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