using System.Reflection;
using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public class ArrayColumn<T> : Column, IColumn<T>
{
    private readonly Column _nestedColumn;

    internal ArrayColumn()
    {
        if (!typeof(T).IsGenericType || typeof(T).GetGenericTypeDefinition() != typeof(ChArray<>))
        {
            throw new ArgumentException("T must be ChArray<U>");
        }

        var elementType = typeof(T).GenericTypeArguments[0]; // This gets the U in ChArray<U>

        // Dynamically create the correct column type based on elementType
        if (typeof(IChBaseType).IsAssignableFrom(elementType))
        {
            var columnType = typeof(BaseColumn<>).MakeGenericType(elementType);
            _nestedColumn = (Column)Activator.CreateInstance(columnType)!;
        }
        else if (typeof(IChNullable).IsAssignableFrom(elementType))
        {
            var columnType = typeof(NullableColumn<>).MakeGenericType(elementType);
            _nestedColumn = (Column)Activator.CreateInstance(columnType)!;
        }
        else if (typeof(IChLowCardinality).IsAssignableFrom(elementType))
        {
            var columnType = typeof(LowCardinalityColumn<>).MakeGenericType(elementType);
            _nestedColumn = (Column)Activator.CreateInstance(columnType)!;
        }
        else if (typeof(IChArray).IsAssignableFrom(elementType))
        {
            const BindingFlags constructorFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var columnType = typeof(ArrayColumn<>).MakeGenericType(elementType);
            _nestedColumn = (Column)Activator.CreateInstance(columnType, constructorFlags, null, null, null)!;
        }
        else
        {
            throw new ArgumentException("Unsupported type");
        }

        var resultStatus =
            ColumnArrayInterop.chc_column_array_create(_nestedColumn.NativeColumn, out var nativeColumn);

        if (resultStatus.Code != 0)
        {
            throw new ClickHouseException(resultStatus);
        }

        NativeColumn = nativeColumn;
    }

    internal ArrayColumn(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    internal override void Add(object value) => Add((T)value);

    public void Add(T value)
    {
        CheckDisposed();

        if (value is not IChArray array) return;

        foreach (var item in array)
        {
            _nestedColumn.Add(item);
        }

        ColumnArrayInterop.chc_column_array_add_offset(NativeColumn, (nuint)array.Count);
    }

    public override object At(int index) => this[index]!;

    public T this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            var offset = ColumnArrayInterop.chc_column_array_get_offset(NativeColumn, (nuint)index);
            var size = GetArraySize(index);
            object[] args = [_nestedColumn, (int)offset, size, false, null];
            const BindingFlags constructorFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var value = (T)Activator.CreateInstance(typeof(T), constructorFlags, null, args, null)!;
            return value;
        }
    }

    private int GetArraySize(int index)
    {
        if (index == 0)
        {
            return (int)ColumnArrayInterop.chc_column_array_get_offset(NativeColumn, 1);
        }

        var offset = ColumnArrayInterop.chc_column_array_get_offset(NativeColumn, (nuint)index);
        var prevOffset = ColumnArrayInterop.chc_column_array_get_offset(NativeColumn, (nuint)index - 1);
        return (int)(offset - prevOffset);
    }
}