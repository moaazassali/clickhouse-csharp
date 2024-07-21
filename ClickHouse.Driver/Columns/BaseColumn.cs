using ClickHouse.Driver.Interop.Columns;

namespace ClickHouse.Driver.Columns;

internal class BaseColumn<T> : NativeColumnWrapper<T> where T : IChType
{
    internal BaseColumn()
    {
        T value = default;
        NativeColumn = value switch
        {
            ChUInt8 => ColumnUInt8Interop.chc_column_uint8_create(),
            ChUInt16 => ColumnUInt16Interop.chc_column_uint16_create(),
            ChUInt32 => ColumnUInt32Interop.chc_column_uint32_create(),
            ChUInt64 => ColumnUInt64Interop.chc_column_uint64_create(),
            ChInt8 => ColumnInt8Interop.chc_column_int8_create(),
            ChInt16 => ColumnInt16Interop.chc_column_int16_create(),
            ChInt32 => ColumnInt32Interop.chc_column_int32_create(),
            ChInt64 => ColumnInt64Interop.chc_column_int64_create(),
            ChInt128 => ColumnInt128Interop.chc_column_int128_create(),
            ChUuid => ColumnUuidInterop.chc_column_uuid_create(),
            ChFloat32 => ColumnFloat32Interop.chc_column_float32_create(),
            ChFloat64 => ColumnFloat64Interop.chc_column_float64_create(),
            ChDate => ColumnDateInterop.chc_column_date_create(),
            ChDate32 => ColumnDate32Interop.chc_column_date32_create(),
            ChDateTime => ColumnDateTimeInterop.chc_column_datetime_create(),
            ChDateTime64 => ColumnDateTime64Interop.chc_column_datetime64_create(3),
            IChEnum8 => ColumnEnum8Interop.chc_column_enum8_create(),
            IChEnum16 => ColumnEnum16Interop.chc_column_enum16_create(),
            ChString => ColumnStringInterop.chc_column_string_create(),
            ChFixedString => ColumnFixedStringInterop.chc_column_fixed_string_create(5),
            ChIPv4 => ColumnIPv4Interop.chc_column_ipv4_create(),
            ChIPv6 => ColumnIPv6Interop.chc_column_ipv6_create(),
        };
    }

    // second argument is a dummy to distinguish from the constructor above internally within the library
    internal BaseColumn(nint nativeColumn, bool _)
    {
        NativeColumn = nativeColumn;
    }

    internal override void Add(object value) => Add((T)value);

    internal override void Add(T value)
    {
        CheckDisposed();
        ((IChTypeAddable)value).AddToColumn(NativeColumn);
    }

    internal override object At(int index) => this[index];

    internal override T this[int index]
    {
        get
        {
            CheckDisposed();
            if ((uint)index >= (uint)Count)
            {
                throw new IndexOutOfRangeException();
            }

            T value = default;
            var indexNuint = (nuint)index;

            switch (value)
            {
                case ChUInt8 _:
                    return (T)(object)ColumnUInt8Interop.chc_column_uint8_at(NativeColumn, indexNuint);
                case ChUInt16 _:
                    return (T)(object)(ChUInt16)ColumnUInt16Interop.chc_column_uint16_at(NativeColumn, indexNuint);
                case ChUInt32 _:
                    return (T)(object)(ChUInt32)ColumnUInt32Interop.chc_column_uint32_at(NativeColumn, indexNuint);
                case ChUInt64 _:
                    return (T)(object)(ChUInt64)ColumnUInt64Interop.chc_column_uint64_at(NativeColumn, indexNuint);
                case ChInt8 _:
                    return (T)(object)(ChInt8)ColumnInt8Interop.chc_column_int8_at(NativeColumn, indexNuint);
                case ChInt16 _:
                    return (T)(object)(ChInt16)ColumnInt16Interop.chc_column_int16_at(NativeColumn, indexNuint);
                case ChInt32 _:
                    return (T)(object)(ChInt32)ColumnInt32Interop.chc_column_int32_at(NativeColumn, indexNuint);
                case ChInt64 _:
                    return (T)(object)(ChInt64)ColumnInt64Interop.chc_column_int64_at(NativeColumn, indexNuint);
                case ChInt128 _:
                    return (T)(object)(ChInt128)ColumnInt128Interop.chc_column_int128_at(NativeColumn, indexNuint)
                        .ToInt128();
                case ChUuid _:
                    return (T)(object)ChUuid.FromUuidInterop(
                        ColumnUuidInterop.chc_column_uuid_at(NativeColumn, indexNuint));
                case ChFloat32 _:
                    return (T)(object)(ChFloat32)ColumnFloat32Interop.chc_column_float32_at(NativeColumn, indexNuint);
                case ChFloat64 _:
                    return (T)(object)(ChFloat64)ColumnFloat64Interop.chc_column_float64_at(NativeColumn, indexNuint);
                case ChDate _:
                    return (T)(object)(ChDate)ColumnDateInterop.chc_column_date_at(NativeColumn, indexNuint);
                case ChDate32 _:
                    return (T)(object)(ChDate32)ColumnDate32Interop.chc_column_date32_at(NativeColumn, indexNuint);
                case ChDateTime _:
                    return (T)(object)(ChDateTime)ColumnDateTimeInterop.chc_column_datetime_at(NativeColumn,
                        indexNuint);
                case ChDateTime64 _:
                    return (T)(object)(ChDateTime64)ColumnDateTime64Interop.chc_column_datetime64_at(NativeColumn,
                        indexNuint);
                case IChEnum8 enum8:
                    enum8.Value = ColumnEnum8Interop.chc_column_enum8_at(NativeColumn, indexNuint);
                    return (T)enum8;
                case IChEnum16 enum16:
                    enum16.Value = ColumnEnum8Interop.chc_column_enum8_at(NativeColumn, indexNuint);
                    return (T)enum16;
                case ChString _:
                    return (T)(object)(ChString)ColumnStringInterop.chc_column_string_at(NativeColumn, indexNuint)
                        .ToString();
                case ChFixedString _:
                    return (T)(object)(ChFixedString)ColumnFixedStringInterop.chc_column_fixed_string_at(NativeColumn,
                        indexNuint).ToString();
                case ChIPv4 _:
                    return (T)(object)(ChIPv4)ColumnIPv4Interop.chc_column_ipv4_at(NativeColumn, indexNuint);
                case ChIPv6 _:
                    return (T)(object)ChIPv6.FromIn6AddrInterop(
                        ColumnIPv6Interop.chc_column_ipv6_at(NativeColumn, indexNuint));
            }

            throw new ArgumentException(value.GetType().ToString());
        }
    }
}