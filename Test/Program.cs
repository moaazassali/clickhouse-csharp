using ClickHouse.Driver;
using ClickHouse.Driver.Columns;

namespace Test;

class Program
{
    static void Main(string[] args)
    {
        var options = new ClickHouseClientOptions()
        {
            Host = "192.168.70.176"
        };
        using var connection = new ClickHouseConnection(options);

        var blocks = new ClickHouseBlock[1000];

        for (var i = 0; i < 1000; i++)
        {
            blocks[i] = new ClickHouseBlock();
            var col1 = new ColumnInt32();
            var col2 = new ColumnDateTime64(3);
            var col3 = new ColumnInt32();
            var col4 = new ColumnFloat64();
            var col5 = new ColumnInt64();

            for (var j = 0; j < 1000; j++)
            {
                col1.Add(i);
                col2.Add(j);
                col3.Add(30);
                col4.Add(9.4);
                col5.Add(40);
            }

            blocks[i].AppendColumn("device_id", col1);
            blocks[i].AppendColumn("ts", col2);
            blocks[i].AppendColumn("temperature", col3);
            blocks[i].AppendColumn("pressure", col4);
            blocks[i].AppendColumn("humidity", col5);
        }

        foreach (var block in blocks)
            connection.Insert("test.devices", block);


        // Parallel.For(0, connections.Length, i =>
        // {
        //     var query = $"INSERT INTO test.devices VALUES ({i}, 0, 30, 9.4, 40)";
        //     connections[i].Execute(query);
        // });

        // Parallel.For(0, connections.Length, i =>
        // {
        //     var query = $"INSERT INTO test.devices VALUES ({i}, 0, 30, 9.4, 40)";
        //     connections[i].Execute(query);
        // });
        //
        // Parallel.For(0, connections.Length, i =>
        // {
        //     var query = $"INSERT INTO test.devices VALUES ({i}, 0, 30, 9.4, 40)";
        //     connections[i].Execute(query);
        // });
        //
    }
}