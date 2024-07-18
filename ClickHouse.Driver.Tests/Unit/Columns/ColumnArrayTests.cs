using ClickHouse.Driver.Columns;

namespace ClickHouse.Driver.Tests.Unit.Columns;

public class ColumnArrayTests
{
    [Fact]
    public void Add_1DBaseArray_ReturnsSameValue()
    {
        var col = new Column<ChArray<ChInt8>>();

        col.Add(new List<ChInt8> { 42 });
        var value = col[0];

        Assert.Equal(42, value[0]);
    }

    [Fact]
    public void Add_2DBaseArray_ReturnsSameValue()
    {
        var col = new Column<ChArray<ChArray<ChInt8>>>();

        col.Add(new List<ChArray<ChInt8>> { new List<ChInt8> { 42 } });
        var value = col[0];

        Assert.Equal(42, value[0][0]);
    }

    [Fact]
    public void Add_1DNullableArray_ReturnsSameValue()
    {
        var col = new Column<ChArray<ChNullable<ChInt8>>>();


        var valToAdd = new List<ChNullable<ChInt8>> { (ChInt8)42, null };
        col.Add(valToAdd);
        var value = col[0];

        Assert.Equal(42, value[0].Value);
    }

    [Fact]
    public void Add_2DNullableArray_ReturnsSameValue()
    {
        var col = new Column<ChArray<ChArray<ChNullable<ChInt8>>>>();

        var arrayToAdd = new List<ChArray<ChNullable<ChInt8>>> { new List<ChNullable<ChInt8>> { (ChInt8)42, null } };
        col.Add(arrayToAdd);
        var value = col[0];

        Assert.Equal(42, value[0][0].Value);
    }

    [Fact]
    public void Add_1DLowCardinalityArray_ReturnsSameValue()
    {
        var col = new Column<ChArray<ChLowCardinality<ChString>>>();

        var arrayToAdd = new List<ChLowCardinality<ChString>> { (ChString)"hello" };
        col.Add(arrayToAdd);
        var actual = col[0];

        Assert.Equal("hello", (ChString)actual[0]);
    }

    [Fact]
    public void Add_2DLowCardinalityArray_ReturnsSameValue()
    {
        var col = new Column<ChArray<ChArray<ChLowCardinality<ChString>>>>();

        var arrayToAdd = new List<ChArray<ChLowCardinality<ChString>>>
            { new List<ChLowCardinality<ChString>> { (ChString)"hello" } };
        col.Add(arrayToAdd);
        var actual = col[0];

        Assert.Equal("hello", (ChString)actual[0][0]);
    }
}