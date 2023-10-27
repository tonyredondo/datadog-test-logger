using System.Runtime.InteropServices;

namespace DatadogCollector;

public static class TotalCpuUsage
{
    private static readonly Lazy<IUsage?> LazyUsage = new(() =>
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Windows.Instance;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Linux.Instance;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return MacOs.Instance;
        }

        return null;
    });

    public static float GetUsage()
    {
        try
        {
            return LazyUsage.Value?.GetUsage() ?? 0;
        }
        catch
        {
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
        
        public float GetUsage()
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

                if (sys == 0)
                {
                    return 0;
                }

                return (float)((double)((sys - idl) * 100) / (double)sys);
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

        public float GetUsage()
        {
            var cpuDataLine = File.ReadAllText("/proc/stat").Split('\n')[0];
            var cpuDataParts = cpuDataLine.Split(_separator, StringSplitOptions.RemoveEmptyEntries);

            var idleTime = long.Parse(cpuDataParts[4]) + long.Parse(cpuDataParts[5]);
            long totalTime = 0;

            for (var i = 1; i <= 7; ++i)
            {
                totalTime += long.Parse(cpuDataParts[i]);
            }

            var idleTimeDelta = idleTime - _prevIdleTime;
            var totalTimeDelta = totalTime - _prevTotalTime;

            _prevIdleTime = idleTime;
            _prevTotalTime = totalTime;

            return (float)((double)((totalTimeDelta - idleTimeDelta) * 100) / (double)totalTimeDelta);
        }
    }

    class MacOs : IUsage
    {
        private IntPtr _cpuInfoSize;

        private static readonly Lazy<MacOs> LazyInstance = new(() => new MacOs());

        public static MacOs Instance => LazyInstance.Value;

        private unsafe MacOs()
        {
            _cpuInfoSize = Marshal.AllocCoTaskMem(sizeof(nint));
            Marshal.WriteInt64(_cpuInfoSize, sizeof(CpuTimeInfo));
        }

        public unsafe float GetUsage()
        {
            if (sysctlbyname("vm.loadavg", out var cpuInfo, _cpuInfoSize, IntPtr.Zero, 0) == 0)
            {
                var avg = (double)(cpuInfo.ldavg[0]) / cpuInfo.fscale;
                return (float) ((avg * 100) / Environment.ProcessorCount);
            }

            return -1;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CpuTimeInfo
        {
            public unsafe fixed uint ldavg[3];
            public long fscale;
        }

        [DllImport("libc", SetLastError = true)]
        private static extern int sysctlbyname(
            [MarshalAs(UnmanagedType.LPStr)] string property,
            out CpuTimeInfo output,
            IntPtr oldLen,
            IntPtr newp,
            uint newlen
        );
    }

    interface IUsage
    {
        float GetUsage();
    }
}
