using ClickHouse.Driver;
using ClickHouse.Driver.ClickHouseColumns;

namespace Test;

class Program
{
    static void Main(string[] args)
    {
        // using var connection = new ClickHouseConnection("192.168.70.176");

        var blocks = new ClickHouseBlock[1000];

        for (var i = 0; i < 1000; i++)
        {
            blocks[i] = new ClickHouseBlock();
            var col1 = new ClickHouseColumnInt32();
            var col2 = new ClickHouseColumnDateTime64(3);
            var col3 = new ClickHouseColumnInt32();
            var col4 = new ClickHouseColumnFloat64();
            var col5 = new ClickHouseColumnInt64();

            for (var j = 0; j < 1000; j++)
            {
                col1.Append(i);
                col2.Append(j);
                col3.Append(30);
                col4.Append(9.4);
                col5.Append(40);
            }

            blocks[i].AppendColumn("device_id", col1);
            blocks[i].AppendColumn("ts", col2);
            blocks[i].AppendColumn("temperature", col3);
            blocks[i].AppendColumn("pressure", col4);
            blocks[i].AppendColumn("humidity", col5);
        }


        // var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        // foreach (var block in blocks)
        //     connection.Insert("test.devices", block);
        // stopwatch.Stop();
        //
        // System.Console.WriteLine($"Took {stopwatch.ElapsedMilliseconds} ms.");


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