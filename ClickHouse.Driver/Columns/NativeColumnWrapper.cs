using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public abstract class NativeColumnWrapper : IDisposable
{
    protected internal nint NativeColumn { get; protected init; }

    private bool _disposed;

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
    internal abstract object At(int index);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            Console.WriteLine("Already disposed");
            return;
        }

        // no managed resources to free
        if (disposing)
        {
        }

        ColumnInterop.chc_column_free(NativeColumn);

        _disposed = true;
    }

    ~NativeColumnWrapper()
    {
        Dispose(false);
    }

    protected void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

public abstract class NativeColumnWrapper<T> : NativeColumnWrapper
{
    internal override void Add(object value) => Add((T)value);

    internal abstract void Add(T value);

    internal abstract T this[int index] { get; }
}