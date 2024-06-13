namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnUuid : ClickHouseColumn<Guid>
{
    public ClickHouseColumnUuid()
    {
        NativeColumn = Native.Columns.NativeColumnUuid.CreateColumnUuid();
    }

    public ClickHouseColumnUuid(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(Guid guid)
    {
        CheckDisposed();
        GuidToInt64(guid, out var first, out var second);
        Native.Columns.NativeColumnUuid.ColumnUuidAppend(NativeColumn,
            new Native.Structs.NativeUuid { First = first, Second = second });
    }

    public Guid this[int index]
    {
        get
        {
            CheckDisposed();
            var nativeUuid = Native.Columns.NativeColumnUuid.ColumnUuidAt(NativeColumn, index);
            return GuidFromInt64(nativeUuid.First, nativeUuid.Second);
        }
    }

    // Taken from https://stackoverflow.com/a/49380620/14003273
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