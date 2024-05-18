using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Connector;

public class ClickHouseConnection : IDisposable
{
    private readonly nint _nativeClient;
    private readonly string _host;
    private bool _disposed;

    public ClickHouseConnection(string host)
    {
        _host = host;
        _disposed = false;
        _nativeClient = Native.NativeClient.CreateClient(host);
    }

    public void Dispose()
    {
        Native.NativeClient.FreeClient(_nativeClient);
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~ClickHouseConnection()
    {
        Dispose();
    }

    private void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public ClickHouseResultStatus Execute(string query)
    {
        CheckDisposed();
        using var clickHouseQuery = new ClickHouseQuery(query);
        var nativeResultStatus = Native.NativeClient.Execute(_nativeClient, clickHouseQuery.NativeQuery);
        return new ClickHouseResultStatus(nativeResultStatus);
    }

    public void Execute(ClickHouseQuery query)
    {
        CheckDisposed();
        Native.NativeClient.Execute(_nativeClient, query.NativeQuery);
    }

    public ClickHouseResultStatus Insert(string tableName, ClickHouseBlock block)
    {
        CheckDisposed();
        var nativeResultStatus = Native.NativeClient.Insert(_nativeClient, tableName, block.NativeBlock);
        return new ClickHouseResultStatus(nativeResultStatus);
    }
    
    public ClickHouseServerInfo GetServerInfo()
    {
        CheckDisposed();
        var nativeServerInfo = Native.NativeClient.GetServerInfo(_nativeClient);
        return new ClickHouseServerInfo(nativeServerInfo);
    }
}