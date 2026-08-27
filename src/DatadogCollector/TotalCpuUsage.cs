using System.Runtime.InteropServices;

namespace DatadogCollector;

public static class TotalCpuUsage
{
    private static readonly Lazy<IUsage?> LazyUsage = new(() =>
    {
        IUsage? usage = null;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            usage = Windows.Instance;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            usage = Linux.Instance;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            usage = MacOs.Instance;
        }

        // Prime the deltas with an initial sample so consumers never see a bogus first
        // value (with unset previous ticks Linux reported ~100% and Windows a
        // boot-to-now average).
        _ = usage?.GetUsage();

        return usage;
    });

    private static long _totalErrors;

    public static double GetUsage()
    {
        try
        {
            if (Interlocked.Read(ref _totalErrors) > 10)
            {
                return -1;
            }

            return LazyUsage.Value?.GetUsage() ?? 0;
        }
        catch
        {
            Interlocked.Increment(ref _totalErrors);
            return -1;
        }
    }

    class Windows : IUsage
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetSystemTimes(out long idle, out long kernel, out long user);

        private long _prevIdleTime = 0;
        private long _prevKernelTime = 0;
        private long _prevUserTime = 0;
        
        private static readonly Lazy<Windows> LazyInstance = new(() => new Windows());

        public static Windows Instance => LazyInstance.Value;
        
        private Windows()
        {
        }
        
        public double GetUsage()
        {
            if (GetSystemTimes(out var idleTime, out var kernelTime, out var userTime))
            {
                var usr = userTime - _prevUserTime;
                var ker = kernelTime - _prevKernelTime;
                var idl = idleTime - _prevIdleTime;
                var sys = ker + usr;

                _prevUserTime = userTime;
                _prevKernelTime = kernelTime;
                _prevIdleTime = idleTime;

                return CpuUsageCalculator.FromDeltas(idl, sys);
            }

            return -1;
        }
    }

    class Linux : IUsage
    {
        private readonly string[] _separator = new[] { " " };
        private long _prevIdleTime = 0;
        private long _prevTotalTime = 0;
     
        private static readonly Lazy<Linux> LazyInstance = new(() => new Linux());

        public static Linux Instance => LazyInstance.Value;

        private Linux()
        {
        }

        public double GetUsage()
        {
            var cpuDataLine = File.ReadAllText("/proc/stat").Split('\n')[0];
            var cpuDataParts = cpuDataLine.Split(_separator, StringSplitOptions.RemoveEmptyEntries);

            // cpuDataParts[0] is the "cpu" label, then: user nice system idle iowait irq softirq steal ...
            // Parse dynamically so new kernel columns don't break the parser, and include steal
            // (cycles lost to the hypervisor) in the total so overcommitted VMs don't report >100%.
            // guest/guest_nice (index 9+) are excluded: the kernel already counts them inside user/nice.
            if (cpuDataParts.Length < 6)
            {
                return -1;
            }

            var idleTime = long.Parse(cpuDataParts[4]) + long.Parse(cpuDataParts[5]);
            var totalFields = Math.Min(cpuDataParts.Length, 9);
            var totalTime = 0L;
            for (var i = 1; i < totalFields; ++i)
            {
                totalTime += long.Parse(cpuDataParts[i]);
            }

            var idleTimeDelta = idleTime - _prevIdleTime;
            var totalTimeDelta = totalTime - _prevTotalTime;

            _prevIdleTime = idleTime;
            _prevTotalTime = totalTime;

            return CpuUsageCalculator.FromDeltas(idleTimeDelta, totalTimeDelta);
        }
    }

    class MacOs : IUsage
    {
        private const int HostCpuLoadInfoFlavor = 3; // HOST_CPU_LOAD_INFO from mach/host_info.h

        // host_statistics takes a mach_msg_type_number_t* which counts in natural_t units,
        // i.e. element count (CPU_STATE_MAX == 4), not byte size.
        private static readonly uint HostCpuLoadInfoCount =
            (uint)(Marshal.SizeOf<HostCpuLoadInfo>() / sizeof(uint));

        // Cache the host port send right once: mach_host_self() grants a new right on each call.
        private static readonly IntPtr HostPort = mach_host_self();

        private long _prevIdleTime = 0;
        private long _prevTotalTime = 0;

        private static readonly Lazy<MacOs> LazyInstance = new(() => new MacOs());

        public static MacOs Instance => LazyInstance.Value;

        private MacOs()
        {
        }

        public unsafe double GetUsage()
        {
            var count = HostCpuLoadInfoCount;
            if (host_statistics(HostPort, HostCpuLoadInfoFlavor, out var info, ref count) != 0)
            {
                return -1;
            }

            // CpuTicks follows the CPU_STATE_* layout: USER, SYSTEM, IDLE, NICE.
            var idleTime = (long)info.CpuTicks[2] + (long)info.CpuTicks[3];
            var totalTime = (long)info.CpuTicks[0] + (long)info.CpuTicks[1] + idleTime;

            var idleTimeDelta = idleTime - _prevIdleTime;
            var totalTimeDelta = totalTime - _prevTotalTime;

            _prevIdleTime = idleTime;
            _prevTotalTime = totalTime;

            return CpuUsageCalculator.FromDeltas(idleTimeDelta, totalTimeDelta);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HostCpuLoadInfo
        {
            public unsafe fixed uint CpuTicks[4]; // CPU_STATE_MAX from mach/host_info.h
        }

        [DllImport("libSystem.dylib")]
        private static extern int host_statistics(
            IntPtr hostPrivPort,
            int flavor,
            out HostCpuLoadInfo info,
            ref uint count);

        [DllImport("libSystem.dylib")]
        private static extern IntPtr mach_host_self();
    }

    interface IUsage
    {
        double GetUsage();
    }
}
