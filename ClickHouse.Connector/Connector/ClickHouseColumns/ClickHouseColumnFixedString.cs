namespace ClickHouse.Connector.Connector.ClickHouseColumns;

public class ClickHouseColumnFixedString : ClickHouseColumn<string>
{
    private int _size;

    public ClickHouseColumnFixedString(int size)
    {
        _size = size;
        NativeColumn = Native.Columns.NativeColumnFixedString.CreateColumnFixedString(size);
    }

    public ClickHouseColumnFixedString(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Append(string value)
    {
        CheckDisposed();
        var nativeResultStatus = Native.Columns.NativeColumnFixedString.ColumnFixedStringAppend(NativeColumn, value);

        if (nativeResultStatus.Code != 0)
        {
            throw new ClickHouseException(nativeResultStatus);
        }
    }

    public string this[int index]
    {
        get
        {
            CheckDisposed();
            var x = Native.Columns.NativeColumnFixedString.ColumnFixedStringAt(NativeColumn, index);
            return x.ToString();
        }
    }
}