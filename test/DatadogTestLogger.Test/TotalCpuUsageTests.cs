using System.Runtime.InteropServices;
using DatadogCollector;

namespace DatadogTestLogger.Test;

public class TotalCpuUsageTests
{
    // ------------------ Pure delta math (runs on every OS, including CI) ------------------

    [Theory]
    [InlineData(250, 1000, 75)] // quarter of the interval idle
    [InlineData(0, 1000, 100)]  // no idle ticks at all
    [InlineData(1000, 1000, 0)] // fully idle window
    [InlineData(120, 10000, 98.8)]
    [InlineData(-5, 10, 100)]   // negative idle delta (corrupted/wrapped clocks): clamped, never >100
    public void FromDeltas_ComputesUsagePercent(long idleTimeDelta, long totalTimeDelta, double expected)
    {
        var usage = CpuUsageCalculator.FromDeltas(idleTimeDelta, totalTimeDelta);

        Assert.Equal(expected, usage);
    }

    [Fact]
    public void FromDeltas_EmptyOrNegativeTotal_ReturnsZero()
    {
        Assert.Equal(0, CpuUsageCalculator.FromDeltas(0, 0));
        Assert.Equal(0, CpuUsageCalculator.FromDeltas(10, -5));
        Assert.Equal(0, CpuUsageCalculator.FromDeltas(-1, -1));
    }

    [Fact]
    public void FromDeltas_RandomValidIntervals_StaysFiniteAndWithinBounds()
    {
        // Property-style sweep guarding against NaN and out-of-range values.
        var random = new Random(1234);
        for (var i = 0; i < 500; i++)
        {
            var totalTime = random.Next(1, int.MaxValue);
            var idleTime = random.Next(0, totalTime + 1);

            var usage = CpuUsageCalculator.FromDeltas(idleTime, totalTime);

            Assert.True(double.IsFinite(usage), $"Non finite value for idle={idleTime}, total={totalTime}");
            Assert.InRange(usage, 0d, 100d);
        }
    }

    // ------------- Real platform implementations (run when on the matching OS) -------------
    // The CI only runs Linux; Windows and macOS facts execute when tests are run locally
    // or on agents of those platforms.

    private static bool IsOS(OSPlatform platform) => RuntimeInformation.IsOSPlatform(platform);

    [Fact]
    public void Linux_GetUsage_ReturnsSaneValues()
    {
        if (!IsOS(OSPlatform.Linux))
        {
            return;
        }

        AssertUsageAcrossTwoSamples();
    }

    [Fact]
    public void MacOs_GetUsage_ReturnsSaneValues()
    {
        if (!IsOS(OSPlatform.OSX))
        {
            return;
        }

        AssertUsageAcrossTwoSamples();
    }

    [Fact]
    public void Windows_GetUsage_ReturnsSaneValues()
    {
        if (!IsOS(OSPlatform.Windows))
        {
            return;
        }

        AssertUsageAcrossTwoSamples();
    }

    private static void AssertUsageAcrossTwoSamples()
    {
        var first = TotalCpuUsage.GetUsage();

        // Even the first sample must be valid thanks to the priming performed when the
        // platform implementation is created.
        AssertFirstSampleIsSane(first);

        System.Threading.Thread.Sleep(millisecondsTimeout: 1100);

        var second = TotalCpuUsage.GetUsage();
        AssertFirstSampleIsSane(second);

        Assert.True(second >= -1, $"Unexpected negative usage: {second}");
    }

    private static void AssertFirstSampleIsSane(double value)
    {
        Assert.True(double.IsFinite(value), $"Expected a finite usage but got {value}");
        Assert.InRange(value, -1d, 100d); // -1 is the API failure sentinel, never NaN/garbage
    }
}
