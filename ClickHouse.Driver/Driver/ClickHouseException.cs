using System.Runtime.InteropServices;

namespace ClickHouse.Driver.Driver;

public class ClickHouseException : Exception
{
    public int Code { get; }
    
    internal ClickHouseException(Native.Structs.NativeResultStatus resultStatus)
        : base(GetMessage(resultStatus))
    {
        Code = resultStatus.Code;
    }

    private static string GetMessage(Native.Structs.NativeResultStatus resultStatus)
    {
        var message = Marshal.PtrToStringUTF8(resultStatus.Message);
        Native.Structs.NativeResultStatus.chc_result_status_free(ref resultStatus);
        return $"{resultStatus.Code}: {message ?? string.Empty}";
    }
}