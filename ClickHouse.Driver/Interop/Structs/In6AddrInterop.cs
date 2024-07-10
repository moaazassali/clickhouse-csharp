using System.Net;
using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Interop.Structs;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct In6AddrInterop
{
    internal fixed byte Bytes[16];

    internal IPAddress ToIPAddress()
    {
        fixed (byte* ptr = Bytes)
        {
            ReadOnlySpan<byte> bytes = new(ptr, 16);
            return new IPAddress(bytes);
        }
    }

    internal static In6AddrInterop FromIPAddress(IPAddress address)
    {
        In6AddrInterop result = default;
        address.TryWriteBytes(new Span<byte>(result.Bytes, 16), out _);
        return result;
    }
}