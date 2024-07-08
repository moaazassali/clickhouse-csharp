using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Driver;

public class ClickHouseServerInfo
{
    public string Name { get; }
    public string Timezone { get; }
    public string DisplayName { get; }
    public ulong VersionMajor { get; }
    public ulong VersionMinor { get; }
    public ulong VersionPatch { get; }
    public ulong Revision { get; }
    
    internal ClickHouseServerInfo(ServerInfoInterop serverInfoInterop)
    {
        Name = Marshal.PtrToStringUTF8(serverInfoInterop.Name) ?? string.Empty;
        Timezone = Marshal.PtrToStringUTF8(serverInfoInterop.Timezone) ?? string.Empty;
        DisplayName = Marshal.PtrToStringUTF8(serverInfoInterop.DisplayName) ?? string.Empty;
        VersionMajor = serverInfoInterop.VersionMajor;
        VersionMinor = serverInfoInterop.VersionMinor;
        VersionPatch = serverInfoInterop.VersionPatch;
        Revision = serverInfoInterop.Revision;
        
        ServerInfoInterop.chc_server_info_free(ref serverInfoInterop);
    }
}