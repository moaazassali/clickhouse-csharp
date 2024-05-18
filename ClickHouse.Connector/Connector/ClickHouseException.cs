using System.Runtime.InteropServices;

namespace ClickHouse.Connector.Connector;

public class ClickHouseException : Exception
{
    internal ClickHouseException(Native.Structs.NativeClickHouseResultStatus resultStatus)
        : base(GetMessage(resultStatus))
    {
    }

    private static string GetMessage(Native.Structs.NativeClickHouseResultStatus resultStatus)
    {
        var message = Marshal.PtrToStringUTF8(resultStatus.Message);
        Native.Structs.NativeClickHouseResultStatus.FreeClickHouseStatusMessage(ref resultStatus);
        return $"{resultStatus.Code}: {message ?? string.Empty}";
    }
}