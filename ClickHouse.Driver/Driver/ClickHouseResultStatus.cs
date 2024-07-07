using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Driver;

public class ClickHouseResultStatus
{
    public int Code { get; }
    public string Message { get; }

    internal ClickHouseResultStatus(Native.Structs.NativeResultStatus resultStatus)
    {
        Code = resultStatus.Code;
        var message = Marshal.PtrToStringUTF8(resultStatus.Message);
        Native.Structs.NativeResultStatus.chc_result_status_free(ref resultStatus);
        Message = message ?? string.Empty;
    }
}
