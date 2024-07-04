using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Connector;

public class ClickHouseServerInfo
{
    public string Name { get; }
    public string Timezone { get; }
    public string DisplayName { get; }
    public ulong VersionMajor { get; }
    public ulong VersionMinor { get; }
    public ulong VersionPatch { get; }
    public ulong Revision { get; }
    
    internal ClickHouseServerInfo(Native.Structs.NativeServerInfo serverInfo)
    {
        Name = Marshal.PtrToStringUTF8(serverInfo.Name) ?? string.Empty;
        Timezone = Marshal.PtrToStringUTF8(serverInfo.Timezone) ?? string.Empty;
        DisplayName = Marshal.PtrToStringUTF8(serverInfo.DisplayName) ?? string.Empty;
        VersionMajor = serverInfo.VersionMajor;
        VersionMinor = serverInfo.VersionMinor;
        VersionPatch = serverInfo.VersionPatch;
        Revision = serverInfo.Revision;
        
        Native.Structs.NativeServerInfo.chc_server_info_free(ref serverInfo);
    }
}