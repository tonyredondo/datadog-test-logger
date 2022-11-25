// <copyright file="DatadogTestLogger.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

namespace DatadogTestLogger;

using Microsoft.VisualStudio.TestPlatform.ObjectModel;

/// <summary>
/// Datadog Test Logger
/// </summary>
[FriendlyName("datadog")]
[ExtensionUri("logger://Microsoft/TestPlatform/DatadogTestLogger/v1")]
public class DatadogTestLogger : Spekt.TestLogger.TestLogger
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatadogTestLogger"/> class.
    /// </summary>
    public DatadogTestLogger()
        : base(new DatadogTestResultSerializer())
    {
    }

    /// <inheritdoc/>
    protected override string DefaultTestResultFile => $"Datadog_TestResult_{DateTime.Now:yyyy-MM-dd_HH_mm_ss}.txt";
}