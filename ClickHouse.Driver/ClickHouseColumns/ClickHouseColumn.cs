namespace ClickHouse.Driver.ClickHouseColumns;

public abstract class ClickHouseColumn : IDisposable
{
    protected internal nint NativeColumn { get; protected init; }

    private bool _disposed;

    public ClickHouseColumnType Type
    {
        get
        {
            CheckDisposed();
            return Interop.Columns.ColumnInterop.chc_column_type(NativeColumn);
        }
    }

    public void Dispose()
    {
        Interop.Columns.ColumnInterop.chc_column_free(NativeColumn);
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~ClickHouseColumn()
    {
        Dispose();
    }

    protected void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

public abstract class ClickHouseColumn<T> : ClickHouseColumn
{
    public abstract void Append(T value);
}