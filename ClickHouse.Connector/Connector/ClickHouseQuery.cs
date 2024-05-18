namespace ClickHouse.Connector.Connector;

public class ClickHouseQuery : IDisposable
{
    internal nint NativeQuery { get; }

    public ClickHouseQuery(string query)
    {
        NativeQuery = Native.NativeQuery.CreateQuery(query);
    }

    public ClickHouseQuery(string query, string queryId)
    {
        NativeQuery = Native.NativeQuery.CreateQuery(query, queryId);
    }

    public void Dispose()
    {
        Native.NativeQuery.FreeQuery(NativeQuery);
        GC.SuppressFinalize(this);
    }

    ~ClickHouseQuery()
    {
        Dispose();
    }
}
