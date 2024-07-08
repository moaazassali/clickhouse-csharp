using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver;

public class ClickHouseEndpoint
{
    public string Host { get; init; }
    public ushort Port { get; init; }

    public ClickHouseEndpoint(string host, ushort port)
    {
        Host = host;
        Port = port;
    }
    
    internal ClickHouseEndpoint(EndpointInterop endpointInterop)
    {
        Host = Marshal.PtrToStringUTF8(endpointInterop.Host) ?? string.Empty;
        Port = endpointInterop.Port;
        
        EndpointInterop.chc_endpoint_free(ref endpointInterop);
    }
    
    internal EndpointInterop ToNativeEndpoint()
    {
        return new EndpointInterop
        {
            Host = Marshal.StringToHGlobalAnsi(Host),
            Port = Port,
        };
    }
}