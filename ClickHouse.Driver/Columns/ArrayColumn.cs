using System.Reflection;
using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

internal class ArrayColumn<T> : NativeColumnWrapper<T>
{
    private readonly NativeColumnWrapper _nestedColumn;

    internal ArrayColumn()
    {
        if (!typeof(T).IsGenericType || typeof(T).GetGenericTypeDefinition() != typeof(ChArray<>))
        {
            throw new ArgumentException("T must be ChArray<U>");
        }

        var elementType = typeof(T).GenericTypeArguments[0]; // This gets the U in ChArray<U>
        const BindingFlags constructorFlags = BindingFlags.Instance | BindingFlags.NonPublic; // internal constructors
        Type columnType;

        // Dynamically create the correct column type based on elementType
        if (typeof(IChBaseType).IsAssignableFrom(elementType))
        {
            columnType = typeof(BaseColumn<>).MakeGenericType(elementType);
        }
        else if (typeof(IChNullable).IsAssignableFrom(elementType))
        {
            columnType = typeof(NullableColumn<>).MakeGenericType(elementType);
        }
        else if (typeof(IChLowCardinality).IsAssignableFrom(elementType))
        {
            columnType = typeof(LowCardinalityColumn<>).MakeGenericType(elementType);
        }
        else if (typeof(IChArray).IsAssignableFrom(elementType))
        {
            columnType = typeof(ArrayColumn<>).MakeGenericType(elementType);
        }
        else
        {
            throw new ArgumentException("Unsupported type");
        }

        _nestedColumn = (NativeColumnWrapper)Activator.CreateInstance(columnType, constructorFlags, null, null, null)!;

        var resultStatus = ColumnArrayInterop.chc_column_array_create(_nestedColumn.NativeColumn, out var nativeColumn);

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

    internal override void Add(T value)
    {
        CheckDisposed();

        if (value is not IChArray array) return;

        foreach (var item in array)
        {
            _nestedColumn.Add(item);
        }

        ColumnArrayInterop.chc_column_array_add_offset(NativeColumn, (nuint)array.Count);
    }

    internal override object At(int index) => this[index]!;

    internal override T this[int index]
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
            return (T)Activator.CreateInstance(typeof(T), constructorFlags, null, args, null)!;
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