using ClickHouse.Connector.Connector.ClickHouseColumns;

namespace ClickHouse.Connector.Connector;

public class ClickHouseBlock : IDisposable
{
    internal nint NativeBlock { get; }
    private bool _disposed;
    private bool _disposing;

    public ClickHouseBlock()
    {
        NativeBlock = Native.NativeBlock.CreateBlock();
        _disposed = false;
    }

    public void Dispose()
    {
        if (_disposing || _disposed)
        {
            return;
        }

        _disposing = true;
        Native.NativeBlock.FreeBlock(NativeBlock);
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~ClickHouseBlock()
    {
        Dispose();
    }

    private void CheckDisposed()
    {
        Console.WriteLine($"Checking disposed: {_disposed}. Object is {this}");
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public void AppendColumn(string tableName, ClickHouseColumn column)
    {
        CheckDisposed();
        Native.NativeBlock.AppendColumn(NativeBlock, tableName, column.NativeColumn);
    }

    public nuint GetColumnCount()
    {
        CheckDisposed();
        return Native.NativeBlock.GetColumnCount(NativeBlock);
    }

    public nuint GetRowCount()
    {
        CheckDisposed();
        return Native.NativeBlock.GetRowCount(NativeBlock);
    }

    public nuint RefreshRowCount()
    {
        CheckDisposed();
        return Native.NativeBlock.RefreshRowCount(NativeBlock);
    }

    public string GetColumnName(nuint index)
    {
        CheckDisposed();
        return Native.NativeBlock.GetColumnName(NativeBlock, index);
    }
}
