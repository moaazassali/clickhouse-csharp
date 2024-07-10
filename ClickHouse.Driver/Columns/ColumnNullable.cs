using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

public interface ISupportsNullable
{
}

public class ColumnNullable<T> : Column where T : Column, ISupportsNullable, new()
{
    public ColumnNullable()
    {
        var nestedColumn = new T();
        ColumnNullableInterop.chc_column_nullable_create(nestedColumn.NativeColumn, out var nativeColumn);
        NativeColumn = nativeColumn;
    }

    public ColumnNullable(Func<T> factory)
    {
        var nestedColumn = factory();
        ColumnNullableInterop.chc_column_nullable_create(nestedColumn.NativeColumn, out var nativeColumn);
        NativeColumn = nativeColumn;
    }

    public ColumnNullable(nint nativeColumn)
    {
        NativeColumn = nativeColumn;
    }

    public void Add(T value)
    {
        CheckDisposed();
        ColumnNullableInterop.chc_column_nullable_append(NativeColumn, value.NativeColumn);
    }
}