namespace XunitSample;

public class UnitTest4
{
    [Fact]
    public void Test16()
    {
    }

    [Fact]
    public void Test17()
    {
    }

    [Fact]
    public void Test18()
    {
    }

    [Fact]
    public void Test19()
    {
    }

    [Fact]
    public void Test20()
    {
    }
    
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    public void Test21(int x, int y, int res)
    {
        Assert.Equal(res, x + y);
    }
}