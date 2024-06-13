namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public abstract class ClickHouseColumn : IDisposable
{
    protected internal nint NativeColumn { get; protected init; }

    private bool _disposed;

    public ClickHouseColumnType Type
    {
        get
        {
            CheckDisposed();
            return Native.Columns.NativeColumn.GetColumnType(NativeColumn);
        }
    }

    public void Dispose()
    {
        Native.Columns.NativeColumn.FreeColumn(NativeColumn);
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