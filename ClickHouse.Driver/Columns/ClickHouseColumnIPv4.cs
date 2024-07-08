using System.Net;
using System.Net.Sockets;
using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ClickHouseColumnIPv4 : ClickHouseColumn<IPAddress>
{
    public ClickHouseColumnIPv4()
    {
        NativeColumn = ColumnIPv4Interop.chc_column_ipv4_create();
    }

    public ClickHouseColumnIPv4(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(IPAddress value)
    {
        CheckDisposed();

        if (value.AddressFamily != AddressFamily.InterNetwork)
        {
            throw new ArgumentException("Only IPv4 addresses are supported", nameof(value));
        }

        var bytes = value.GetAddressBytes();
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }

        ColumnIPv4Interop.chc_column_ipv4_append(NativeColumn, BitConverter.ToUInt32(bytes, 0));
    }

    public void Append(uint value)
    {
        CheckDisposed();
        ColumnIPv4Interop.chc_column_ipv4_append(NativeColumn, value);
    }

    public IPAddress this[int index]
    {
        get
        {
            CheckDisposed();
            var value = ColumnIPv4Interop.chc_column_ipv4_at(NativeColumn, (nuint)index);
            return new IPAddress(value);
        }
    }
}