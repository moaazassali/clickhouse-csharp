namespace ClickHouse.Driver;

public class ClickHouseQuery : IDisposable
{
    internal nint NativeQuery { get; }

    public ClickHouseQuery(string query, string? queryId = null)
    {
        NativeQuery = Interop.QueryInterop.chc_query_create(query, queryId);
    }

    public void Dispose()
    {
        Interop.QueryInterop.chc_query_free(NativeQuery);
        GC.SuppressFinalize(this);
    }

    ~ClickHouseQuery()
    {
        Dispose();
    }
}