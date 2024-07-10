using System.Net;
using System.Net.Sockets;
using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnIPv4 : Column<IPAddress>, ISupportsNullable
{
    public ColumnIPv4()
    {
        NativeColumn = ColumnIPv4Interop.chc_column_ipv4_create();
    }

    public ColumnIPv4(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(IPAddress value)
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

    public void Add(uint value)
    {
        CheckDisposed();
        ColumnIPv4Interop.chc_column_ipv4_append(NativeColumn, value);
    }

    public override IPAddress this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var value = ColumnIPv4Interop.chc_column_ipv4_at(NativeColumn, (nuint)index);
            return new IPAddress(value);
        }
    }
}