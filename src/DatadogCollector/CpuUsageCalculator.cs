namespace DatadogCollector;

/// <summary>
/// Shared pure calculation used by every platform <see cref="TotalCpuUsage"/> implementation,
/// so the delta math is testable on any operating system.
/// </summary>
public static class CpuUsageCalculator
{
    /// <summary>
    /// Calculates the CPU usage percentage for an interval given the accumulated-ticks deltas.
    /// </summary>
    /// <param name="idleTimeDelta">Idle ticks elapsed during the interval.</param>
    /// <param name="totalTimeDelta">Total ticks elapsed during the interval.</param>
    /// <returns>Usage percent clamped into [0..100]; zero when the interval is empty or invalid.</returns>
    public static double FromDeltas(long idleTimeDelta, long totalTimeDelta)
    {
        // An empty or invalid interval would divide by zero (NaN) or claim usage out of thin air,
        // which is how uninitialized "previous ticks" used to report a bogus first sample.
        if (totalTimeDelta <= 0)
        {
            return 0;
        }

        var usage = ((double)((totalTimeDelta - idleTimeDelta) * 100) / (double)totalTimeDelta);

        // Negative idle deltas (clock resets/counter wraparounds) can push the math above 100%;
        // usage beyond the physical range is always corrupted data. Manual clamping keeps
        // compatibility down to net462 (Math.Clamp is unavailable there).
        if (usage < 0d)
        {
            return 0;
        }

        if (usage > 100d)
        {
            return 100;
        }

        return usage;
    }
}
