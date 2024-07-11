using System.Net;
using System.Net.Sockets;
using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ColumnIPv6 : Column, IColumn<IPAddress>, ISupportsNullable
{
    public ColumnIPv6()
    {
        NativeColumn = ColumnIPv6Interop.chc_column_ipv6_create();
    }

    public ColumnIPv6(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(IPAddress value)
    {
        CheckDisposed();
        if (value.AddressFamily != AddressFamily.InterNetworkV6)
        {
            throw new ArgumentException("Only IPv6 addresses are supported", nameof(value));
        }

        ColumnIPv6Interop.chc_column_ipv6_append(NativeColumn, In6AddrInterop.FromIPAddress(value));
    }

    public IPAddress this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var value = ColumnIPv6Interop.chc_column_ipv6_at(NativeColumn, (nuint)index);

            return value.ToIPAddress();
        }
    }
}