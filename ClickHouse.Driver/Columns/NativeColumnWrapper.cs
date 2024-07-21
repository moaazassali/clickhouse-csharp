using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public abstract class NativeColumnWrapper : IDisposable
{
    protected bool Disposed;
    protected internal nint NativeColumn { get; protected init; }
    private readonly bool _isOwnedByBlock;

    internal NativeColumnWrapper()
    {
    }

    // useless bool parameter to distinguish from the public constructor
    internal NativeColumnWrapper(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
        _isOwnedByBlock = true;
    }

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
        if (_isOwnedByBlock)
        {
            return;
        }

        if (Disposed)
        {
            return;
        }

        // no managed resources to free
        if (disposing)
        {
        }

        ColumnInterop.chc_column_free(NativeColumn);

        Disposed = true;
    }

    ~NativeColumnWrapper()
    {
        Dispose(false);
    }

    protected void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
    }
}

internal abstract class NativeColumnWrapper<T> : NativeColumnWrapper
{
    internal override void Add(object value) => Add((T)value);

    internal abstract void Add(T value);

    internal override object At(int index) => this[index]!;

    internal abstract T this[int index] { get; }

    internal NativeColumnWrapper()
    {
    }

    internal NativeColumnWrapper(nint nativeColumn) : base(nativeColumn)
    {
    }
}