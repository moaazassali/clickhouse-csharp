using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnEnum8<T> : Column, IColumn<T>, ISupportsNullable where T : Enum
{
    public ColumnEnum8()
    {
        if (Enum.GetUnderlyingType(typeof(T)) != typeof(sbyte))
        {
            throw new InvalidOperationException(
                $"The enum type {typeof(T).Name} must have sbyte as its underlying type.");
        }

        NativeColumn = ColumnEnum8Interop.chc_column_enum8_create();
    }

    public ColumnEnum8(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(T value)
    {
        CheckDisposed();
        ColumnEnum8Interop.chc_column_enum8_append(NativeColumn, (sbyte)value.GetTypeCode());
    }

    public T this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return (T)Enum.ToObject(typeof(T), ColumnEnum8Interop.chc_column_enum8_at(NativeColumn, (nuint)index));
        }
    }
}