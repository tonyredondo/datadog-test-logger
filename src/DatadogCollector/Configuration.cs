// <copyright file="Configuration.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.ExtensionMethods;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;

namespace DatadogCollector;

public class Configuration
{
    private static readonly Lazy<Configuration> _configuration = new(() => new Configuration());

    public static Configuration Instance => _configuration.Value;

    public bool CpuUsageEnabled { get; private set; }

    public bool CoverageEnabled { get; private set; }

    public Configuration()
    {
        CpuUsageEnabled =
            (EnvironmentHelpers.GetEnvironmentVariable("DD_COLLECTOR_CPU_USAGE", "0") ?? "0").ToBoolean() == true;

        CoverageEnabled =
            (EnvironmentHelpers.GetEnvironmentVariable("DD_COLLECTOR_COVERAGE", "0") ?? "0").ToBoolean() == true;
    }
}