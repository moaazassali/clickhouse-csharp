using System.Runtime.InteropServices;
using ClickHouse.Connector.Native;

namespace ClickHouse.Connector.Connector;

public class ClickHouseConnection : IDisposable
{
    private readonly nint _nativeClient;
    private bool _disposed;

    public unsafe ClickHouseConnection(ClickHouseClientOptions options)
    {
        _disposed = false;
        var nativeOptions = options.ToNativeClientOptions();
        var nativeResultStatus = NativeClient.CreateClient(ref nativeOptions, out var nativeClient);
        nativeOptions.Free(options.NativeEndpoints);
        
        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }

        // nativeClient is a double pointer, the usage of "out nint" is equivalent to dereferencing the pointer
        // based on experimentation, so nativeClient becomes a single pointer to the client
        _nativeClient = nativeClient;
    }

    public void Dispose()
    {
        NativeClient.FreeClient(_nativeClient);
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
        var nativeResultStatus = NativeClient.Execute(_nativeClient, clickHouseQuery.NativeQuery);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Execute(ClickHouseQuery query)
    {
        CheckDisposed();
        var nativeResultStatus = NativeClient.Execute(_nativeClient, query.NativeQuery);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public delegate void SelectCallback(ClickHouseBlock block);

    public void Select(string query, SelectCallback selectCallback)
    {
        CheckDisposed();
        using var clickHouseQuery = new ClickHouseQuery(query);
        var nativeResultStatus = NativeClient.Select(_nativeClient, clickHouseQuery.NativeQuery, (nativeBlock) =>
        {
            var block = new ClickHouseBlock(nativeBlock);
            selectCallback(block);
        });

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Insert(string tableName, ClickHouseBlock block)
    {
        CheckDisposed();
        var nativeResultStatus = NativeClient.Insert(_nativeClient, tableName, block.NativeBlock);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Insert(string tableName, ClickHouseBlock block, string queryId)
    {
        CheckDisposed();
        var nativeResultStatus =
            NativeClient.InsertWithQueryId(_nativeClient, tableName, queryId, block.NativeBlock);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Ping()
    {
        CheckDisposed();
        var nativeResultStatus = NativeClient.Ping(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void ResetConnection()
    {
        CheckDisposed();
        var nativeResultStatus = NativeClient.ResetConnection(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void ResetConnectionEndpoint()
    {
        CheckDisposed();
        var nativeResultStatus = NativeClient.ResetConnectionEndpoint(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public ClickHouseServerInfo GetServerInfo()
    {
        CheckDisposed();
        var nativeServerInfo = NativeClient.GetServerInfo(_nativeClient);
        return new ClickHouseServerInfo(nativeServerInfo);
    }

    public ClickHouseEndpoint GetCurrentEndpoint()
    {
        CheckDisposed();
        var nativeEndpoint = NativeClient.GetCurrentEndpoint(_nativeClient);
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