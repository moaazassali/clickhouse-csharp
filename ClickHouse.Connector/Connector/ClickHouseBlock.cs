using ClickHouse.Connector.Connector.ClickHouseColumns;

namespace ClickHouse.Connector.Connector;

public class ClickHouseBlock : IDisposable
{
    internal nint NativeBlock { get; }
    private bool _disposed;
    public List<ClickHouseColumn> Columns { get; } = [];

    public ClickHouseBlock()
    {
        NativeBlock = Native.NativeBlock.CreateBlock();
        _disposed = false;
    }
    
    public ClickHouseBlock(nint nativeBlock)
    {
        NativeBlock = nativeBlock;
        _disposed = false;
    }

    public void Dispose()
    {
        Native.NativeBlock.FreeBlock(NativeBlock);
        // should columns be disposed here as well?
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~ClickHouseBlock()
    {
        Dispose();
    }

    private void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public void AppendColumn(string tableName, ClickHouseColumn column)
    {
        CheckDisposed();
        Columns.Add(column);
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