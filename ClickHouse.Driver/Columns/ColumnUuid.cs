using ClickHouse.Driver.Interop.Columns;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Columns;

public class ColumnUuid : Column, IColumn<Guid>, ISupportsNullable
{
    public ColumnUuid()
    {
        NativeColumn = ColumnUuidInterop.chc_column_uuid_create();
    }

    public ColumnUuid(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(Guid guid)
    {
        CheckDisposed();
        GuidToInt64(guid, out var first, out var second);
        ColumnUuidInterop.chc_column_uuid_append(NativeColumn,
            new UuidInterop { First = first, Second = second });
    }

    public Guid this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var nativeUuid = ColumnUuidInterop.chc_column_uuid_at(NativeColumn, (nuint)index);
            return GuidFromInt64(nativeUuid.First, nativeUuid.Second);
        }
    }

    // Taken from https://stackoverflow.com/a/49380620/14003273
    // Should take another look at this because of endianness issues
    private static unsafe Guid GuidFromInt64(ulong x, ulong y)
    {
        var ptr = stackalloc ulong[2];
        ptr[0] = x;
        ptr[1] = y;
        return *(Guid*)ptr;
    }

    private static unsafe void GuidToInt64(Guid value, out ulong x, out ulong y)
    {
        var ptr = (ulong*)&value;
        x = *ptr++;
        y = *ptr;
    }
}