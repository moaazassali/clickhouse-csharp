using System.Runtime.InteropServices;
using ClickHouse.Driver.Interop.Structs;

namespace ClickHouse.Driver.Driver;

public class ClickHouseResultStatus
{
    public int Code { get; }
    public string Message { get; }

    internal ClickHouseResultStatus(ResultStatusInterop resultStatusInterop)
    {
        Code = resultStatusInterop.Code;
        var message = Marshal.PtrToStringUTF8(resultStatusInterop.Message);
        ResultStatusInterop.chc_result_status_free(ref resultStatusInterop);
        Message = message ?? string.Empty;
    }
}
