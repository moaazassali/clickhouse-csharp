using ClickHouse.Connector.Connector;

namespace Test;

class Program
{
    static void Main(string[] args)
    {
        using var connection = new ClickHouseConnection("192.168.70.176");
        var serverInfo = connection.GetServerInfo();
        Console.WriteLine(serverInfo);
        Console.WriteLine(serverInfo.Name);
        Console.WriteLine(serverInfo.Timezone);
        Console.WriteLine(serverInfo.DisplayName);
        Console.WriteLine(serverInfo.VersionMinor);
        Console.WriteLine(serverInfo.VersionMajor);
        Console.WriteLine(serverInfo.VersionPatch);
        Console.WriteLine(serverInfo.Revision);

        using ClickHouseBlock block = new ClickHouseBlock();
        connection.Insert("test.devices", block);
    }
}