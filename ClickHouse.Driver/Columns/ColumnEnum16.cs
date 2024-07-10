using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ColumnEnum16<T> : Column<T> where T : Enum
{
    public ColumnEnum16()
    {
        if (Enum.GetUnderlyingType(typeof(T)) != typeof(short))
        {
            throw new InvalidOperationException(
                $"The enum type {typeof(T).Name} must have short as its underlying type.");
        }

        NativeColumn = ColumnEnum16Interop.chc_column_enum16_create();
    }

    public ColumnEnum16(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public override void Add(T value)
    {
        CheckDisposed();
        ColumnEnum16Interop.chc_column_enum16_append(NativeColumn, (short)value.GetTypeCode());
    }

    public override T this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            return (T)Enum.ToObject(typeof(T), ColumnEnum16Interop.chc_column_enum16_at(NativeColumn, (nuint)index));
        }
    }
}