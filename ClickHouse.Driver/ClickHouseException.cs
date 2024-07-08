using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver;

public class ClickHouseException : Exception
{
    public int Code { get; }
    
    internal ClickHouseException(ResultStatusInterop resultStatusInterop)
        : base(GetMessage(resultStatusInterop))
    {
        Code = resultStatusInterop.Code;
    }

    private static string GetMessage(ResultStatusInterop resultStatusInterop)
    {
        var message = Marshal.PtrToStringUTF8(resultStatusInterop.Message);
        ResultStatusInterop.chc_result_status_free(ref resultStatusInterop);
        return $"{resultStatusInterop.Code}: {message ?? string.Empty}";
    }
}