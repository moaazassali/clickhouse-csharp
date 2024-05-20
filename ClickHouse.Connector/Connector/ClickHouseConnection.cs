using System.ComponentModel;
using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Connector;

public class ClickHouseConnection : IDisposable
{
    private readonly nint _nativeClient;
    private readonly string _host;
    private bool _disposed;

    public ClickHouseConnection(ClickHouseClientOptions options)
    {
        _disposed = false;
        var nativeOptions = options.ToNativeClientOptions();
        _nativeClient = Native.NativeClient.CreateClient(ref nativeOptions);
        nativeOptions.Free(options.NativeEndpoints);
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

    public void Execute(string query)
    {
        CheckDisposed();
        using var clickHouseQuery = new ClickHouseQuery(query);
        var nativeResultStatus = Native.NativeClient.Execute(_nativeClient, clickHouseQuery.NativeQuery);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Execute(ClickHouseQuery query)
    {
        CheckDisposed();
        var nativeResultStatus = Native.NativeClient.Execute(_nativeClient, query.NativeQuery);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Insert(string tableName, ClickHouseBlock block)
    {
        CheckDisposed();
        var nativeResultStatus = Native.NativeClient.Insert(_nativeClient, tableName, block.NativeBlock);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Insert(string tableName, ClickHouseBlock block, string queryId)
    {
        CheckDisposed();
        var nativeResultStatus =
            Native.NativeClient.InsertWithQueryId(_nativeClient, tableName, queryId, block.NativeBlock);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Ping()
    {
        CheckDisposed();
        var nativeResultStatus = Native.NativeClient.Ping(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void ResetConnection()
    {
        CheckDisposed();
        var nativeResultStatus = Native.NativeClient.ResetConnection(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void ResetConnectionEndpoint()
    {
        CheckDisposed();
        var nativeResultStatus = Native.NativeClient.ResetConnectionEndpoint(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public ClickHouseServerInfo GetServerInfo()
    {
        CheckDisposed();
        var nativeServerInfo = Native.NativeClient.GetServerInfo(_nativeClient);
        return new ClickHouseServerInfo(nativeServerInfo);
    }

    public ClickHouseEndpoint GetCurrentEndpoint()
    {
        CheckDisposed();
        var nativeEndpoint = Native.NativeClient.GetCurrentEndpoint(_nativeClient);
        return new ClickHouseEndpoint(nativeEndpoint);
    }

    public static ClickHouseCppVersion GetVersion()
    {
        return new ClickHouseCppVersion();
    }
}

public struct ClickHouseCppVersion
{
    public ClickHouseCppVersion()
    {
    }

    public int Major { get; } = 2;
    public int Minor { get; } = 5;
    public int Patch { get; } = 1;
    public int Build { get; } = 0;
}