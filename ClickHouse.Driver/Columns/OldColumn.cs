using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public interface IOldColumn : IDisposable
{
    ColumnType Type { get; }
    void Reserve(int size);
    void Clear();
    int Count { get; }
    object At(int index);
}

public abstract class OldColumn : IOldColumn
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

    ~OldColumn()
    {
        Dispose(false);
    }

    protected void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

public interface IOldColumn<T> : IOldColumn
{
    void Add(T value);
    T this[int index] { get; }
}