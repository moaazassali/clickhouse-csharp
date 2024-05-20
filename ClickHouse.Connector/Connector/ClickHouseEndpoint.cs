using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Connector;

public class ClickHouseEndpoint
{
    public string Host { get; init; }
    public ushort Port { get; init; }

    public ClickHouseEndpoint(string host, ushort port)
    {
        Host = host;
        Port = port;
    }
    
    internal ClickHouseEndpoint(NativeEndpoint endpoint)
    {
        Host = Marshal.PtrToStringUTF8(endpoint.Host) ?? string.Empty;
        Port = endpoint.Port;
        
        NativeEndpoint.FreeEndpointWrapper(ref endpoint);
    }
    
    internal NativeEndpoint ToNativeEndpoint()
    {
        return new NativeEndpoint
        {
            Host = Marshal.StringToHGlobalAnsi(Host),
            Port = Port,
        };
    }
}