using System.Runtime.CompilerServices;
using ClickHouse.Driver.Interop.Columns;

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

    protected bool _disposed;

    public ColumnType Type
    {
        get
        {
            CheckDisposed();
            return ColumnInterop.chc_column_type_code(NativeColumn);
        }
    }

    public void Reserve(int size)
    {
        CheckDisposed();
        ColumnInterop.chc_column_reserve(NativeColumn, (nuint)size);
    }

    public void Clear()
    {
        CheckDisposed();
        ColumnInterop.chc_column_clear(NativeColumn);
    }

    public int Count
    {
        get
        {
            CheckDisposed();
            return (int)ColumnInterop.chc_column_size(NativeColumn);
        }
    }

    internal abstract void Add(object value);
    public abstract object At(int index);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            Console.WriteLine("Already disposed");
            return;
        }

        if (disposing)
        {
            // TODO: dispose managed state (managed objects).
        }

        ColumnInterop.chc_column_free(NativeColumn);

        _disposed = true;
    }

    ~Column()
    {
        Dispose(false);
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

public sealed class Column<T> : Column, IColumn<T> where T : IChType
{
    private readonly NativeColumnWrapper<T> _column;

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

    private Action<T> GetAddFunc()
    {
        if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
        {
            return ((BaseColumn<T>)_column).Add;
        }

        if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
        {
            return ((NullableColumn<T>)_column).Add;
        }

        if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
        {
            return ((LowCardinalityColumn<T>)_column).Add;
        }

        if (typeof(IChArray).IsAssignableFrom(typeof(T)))
        {
            return ((ArrayColumn<T>)_column).Add;
        }

        throw new NotSupportedException(typeof(T).ToString());
    }

    internal override void Add(object value) => Add((T)value);

    public void Add(T value)
    {
        CheckDisposed();

        // Calling _column.Add(value) directly is much slow than the code below, around 2x slower
        if (typeof(IChBaseType).IsAssignableFrom(typeof(T)))
        {
            ((BaseColumn<T>)_column).Add(value);
        }

        else if (typeof(IChNullable).IsAssignableFrom(typeof(T)))
        {
            ((NullableColumn<T>)_column).Add(value);
        }

        else if (typeof(IChLowCardinality).IsAssignableFrom(typeof(T)))
        {
            ((LowCardinalityColumn<T>)_column).Add(value);
        }

        else if (typeof(IChArray).IsAssignableFrom(typeof(T)))
        {
            ((ArrayColumn<T>)_column).Add(value);
        }
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

    public new void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        _column.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}