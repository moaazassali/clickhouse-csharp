using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Connector;

public class ClickHouseResultStatus
{
    public int Code { get; }
    public string Message { get; }

    internal ClickHouseResultStatus(Native.Structs.NativeClickHouseResultStatus resultStatus)
    {
        Code = resultStatus.Code;
        var message = Marshal.PtrToStringUTF8(resultStatus.Message);
        Native.Structs.NativeClickHouseResultStatus.FreeClickHouseError(ref resultStatus);
        Message = message ?? string.Empty;
    }
}
