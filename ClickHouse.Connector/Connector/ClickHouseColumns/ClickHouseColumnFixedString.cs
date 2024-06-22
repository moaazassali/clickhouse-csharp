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
        // we could throw here if string has more bytes than size, but that would require UTF-8 encoding
        // which will again be done when passing value to native method with marshalling
        // in the future, we could do the UTF-8 encoding here and pass nint to native method instead of string
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