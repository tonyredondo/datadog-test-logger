namespace XunitSample;

public class ParameterizedTests
{
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(3, 3, 6)]
    [InlineData(4, 4, 8)]
    [InlineData(5, 5, 10)]
    public void Sum(int x, int y, int res)
    {
        Assert.Equal(res, x + y);
    }
}