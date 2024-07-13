// using ClickHouse.Driver.Interop.Columns;
//
// namespace ClickHouse.Driver.Columns;
//
// public class ColumnArray<T> : Column where T : struct, IChType
// {
//     private readonly Column _nestedColumn;
//
//     public ColumnArray()
//     {
//         _nestedColumn = new Column<T>();
//         ColumnArrayInterop.chc_column_array_create(_nestedColumn.NativeColumn, out var nativeColumn);
//         NativeColumn = nativeColumn;
//     }
//
//     internal ColumnArray(nint nativeColumn)
//     {
//         NativeColumn = nativeColumn;
//     }
//
//     public void Add(T[] value)
//     {
//         CheckDisposed();
//
//     }
// }