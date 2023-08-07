using Xunit.Abstractions;

namespace XunitSample;

public class UnitTest1
{
    private ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test1()
    {
        _output.WriteLine("Test 1 message");
    }

    [Fact]
    public void Test2()
    {
        _output.WriteLine("Test 2 message");
    }

    [Fact]
    public void Test3()
    {
        _output.WriteLine("Test 3 message");
    }

    [Fact]
    public void Test4()
    {
        _output.WriteLine("Test 4 message");
    }

    [Fact]
    public void Test5()
    {
        _output.WriteLine("Test 5 message");
        for (var i = 0; i < 25_000; i++)
        {
            try
            {
                ThrowingStuff();
            }
            catch
            {
                // .
            }
        }

        ThrowingStuff();

        static void ThrowingStuff()
        {
            var k = 0;
            var y = 18;
            var z = y / k;
        }
    }
}