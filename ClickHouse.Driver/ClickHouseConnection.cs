using ClickHouse.Driver.Interop;

namespace ClickHouse.Driver;

public class ClickHouseConnection : IDisposable
{
    private readonly nint _nativeClient;
    private bool _disposed;

    public ClickHouseConnection(ClickHouseClientOptions options)
    {
        _disposed = false;
        var optionsInterop = options.ToClientOptionsInterop();
        var nativeResultStatus = ClientInterop.chc_client_create(ref optionsInterop, out var nativeClient);
        optionsInterop.Free(options.NativeEndpoints);

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
        ClientInterop.chc_client_free(_nativeClient);
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
        var nativeResultStatus = ClientInterop.chc_client_execute(_nativeClient, clickHouseQuery.NativeQuery);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Execute(ClickHouseQuery query)
    {
        CheckDisposed();
        var nativeResultStatus = ClientInterop.chc_client_execute(_nativeClient, query.NativeQuery);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public delegate void SelectCallback(ClickHouseBlock block);

    public void Select(string query, SelectCallback callback)
    {
        CheckDisposed();

        using var clickHouseQuery = new ClickHouseQuery(query);
        QueryInterop.chc_query_on_data(clickHouseQuery.NativeQuery, nativeBlock =>
        {
            var block = new ClickHouseBlock(nativeBlock);
            callback(block);
        });

        Execute(clickHouseQuery);
    }

    public void Insert(string tableName, ClickHouseBlock block)
    {
        CheckDisposed();
        var nativeResultStatus = ClientInterop.chc_client_insert(_nativeClient, tableName, block.NativeBlock);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Insert(string tableName, ClickHouseBlock block, string queryId)
    {
        CheckDisposed();
        var nativeResultStatus =
            ClientInterop.chc_client_insert_with_query_id(_nativeClient, tableName, queryId, block.NativeBlock);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void Ping()
    {
        CheckDisposed();
        var nativeResultStatus = ClientInterop.chc_client_ping(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void ResetConnection()
    {
        CheckDisposed();
        var nativeResultStatus = ClientInterop.chc_client_reset_connection(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public void ResetConnectionEndpoint()
    {
        CheckDisposed();
        var nativeResultStatus = ClientInterop.chc_client_reset_connection_endpoint(_nativeClient);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public ClickHouseServerInfo GetServerInfo()
    {
        CheckDisposed();
        var nativeServerInfo = ClientInterop.chc_client_get_server_info(_nativeClient);
        return new ClickHouseServerInfo(nativeServerInfo);
    }

    public ClickHouseEndpoint GetCurrentEndpoint()
    {
        CheckDisposed();
        var nativeEndpoint = ClientInterop.chc_client_get_current_endpoint(_nativeClient);
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