using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Connector;

public class ClickHouseException : Exception
{
    public int Code { get; }
    
    internal ClickHouseException(Native.Structs.NativeClickHouseResultStatus resultStatus)
        : base(GetMessage(resultStatus))
    {
        Code = resultStatus.Code;
    }

    private static string GetMessage(Native.Structs.NativeClickHouseResultStatus resultStatus)
    {
        var message = Marshal.PtrToStringUTF8(resultStatus.Message);
        Native.Structs.NativeClickHouseResultStatus.FreeClickHouseResultStatus(ref resultStatus);
        return $"{resultStatus.Code}: {message ?? string.Empty}";
    }
}