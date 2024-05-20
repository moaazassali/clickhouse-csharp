using System.Runtime.InteropServices;
using ClickHouse.Connector.Native.Structs;

namespace ClickHouse.Connector.Connector;

public class ClickHouseClientOptions
{
    public string Host { get; init; } = "";
    public ushort Port { get; init; } = 9000;
    public ClickHouseEndpoint[] Endpoints { get; init; } = [];
    internal NativeEndpoint[] NativeEndpoints { get; private set; } = [];
    public string DefaultDatabase { get; init; } = "default";
    public string User { get; init; } = "default";
    public string Password { get; init; } = string.Empty;
    public bool RethrowExceptions { get; init; } = true;
    public bool PingBeforeQuery { get; init; } = false;
    public uint SendRetries { get; init; } = 1;
    public TimeSpan RetryTimeout { get; init; } = TimeSpan.FromSeconds(5);
    public CompressionMethod CompressionMethod { get; init; } = CompressionMethod.None;
    public bool TcpKeepalive { get; init; } = false;
    public TimeSpan TcpKeepAliveIdle { get; init; } = TimeSpan.FromSeconds(60);
    public TimeSpan TcpKeepAliveInterval { get; init; } = TimeSpan.FromSeconds(5);
    public uint TcpKeepAliveCount { get; init; } = 3;
    public bool TcpNoDelay { get; init; } = true;
    public TimeSpan ConnectionConnectTimeout { get; init; } = TimeSpan.FromSeconds(5);
    public TimeSpan ConnectionRecvTimeout { get; init; } = TimeSpan.FromSeconds(0);
    public TimeSpan ConnectionSendTimeout { get; init; } = TimeSpan.FromSeconds(0);
    public bool BackwardCompatibilityLowCardinalityAsWrappedColumn { get; init; } = false;
    public uint MaxCompressionChunkSize { get; init; } = 65535;

    internal NativeClientOptions ToNativeClientOptions()
    {
        var nativeClientOptions = new NativeClientOptions
        {
            Host = Marshal.StringToHGlobalAnsi(Host),
            Port = Port,
            Endpoints = Marshal.AllocHGlobal(Marshal.SizeOf<NativeEndpoint>() * Endpoints.Length),
            EndpointsCount = (nuint)Endpoints.Length,
            DefaultDatabase = Marshal.StringToHGlobalAnsi(DefaultDatabase),
            User = Marshal.StringToHGlobalAnsi(User),
            Password = Marshal.StringToHGlobalAnsi(Password),
            RethrowExceptions = RethrowExceptions ? (byte)1 : (byte)0,
            PingBeforeQuery = PingBeforeQuery ? (byte)1 : (byte)0,
            SendRetries = SendRetries,
            RetryTimeout = RetryTimeout.Ticks,
            CompressionMethod = (int)CompressionMethod,
            TcpKeepalive = TcpKeepalive ? (byte)1 : (byte)0,
            TcpKeepaliveIdle = TcpKeepAliveIdle.Ticks,
            TcpKeepaliveIntvl = TcpKeepAliveInterval.Ticks,
            TcpKeepaliveCnt = TcpKeepAliveCount,
            TcpNodelay = TcpNoDelay ? (byte)1 : (byte)0,
            ConnectionConnectTimeout = ConnectionConnectTimeout.Seconds,
            ConnectionRecvTimeout = ConnectionRecvTimeout.Milliseconds,
            ConnectionSendTimeout = ConnectionSendTimeout.Milliseconds,
            BackwardCompatibilityLowcardinalityAsWrappedColumn =
                BackwardCompatibilityLowCardinalityAsWrappedColumn ? (byte)1 : (byte)0,
            MaxCompressionChunkSize = MaxCompressionChunkSize,
        };

        NativeEndpoints = new NativeEndpoint[Endpoints.Length];
        

        for (var i = 0; i < Endpoints.Length; i++)
        {
            NativeEndpoints[i] = Endpoints[i].ToNativeEndpoint();
            Marshal.StructureToPtr(NativeEndpoints[i],
                nativeClientOptions.Endpoints + Marshal.SizeOf<NativeEndpoint>() * i, false);
        }

        return nativeClientOptions;
    }
}