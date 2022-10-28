//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="TracerManagerFactory.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using DatadogTestLogger.Vendors.Datadog.Trace.Agent;
using DatadogTestLogger.Vendors.Datadog.Trace.Agent.DiscoveryService;
using DatadogTestLogger.Vendors.Datadog.Trace.AppSec;
using DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler;
using DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.ServerlessInstrumentation;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.ContinuousProfiler;
using DatadogTestLogger.Vendors.Datadog.Trace.DataStreamsMonitoring;
using DatadogTestLogger.Vendors.Datadog.Trace.DogStatsd;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission;
using DatadogTestLogger.Vendors.Datadog.Trace.PlatformHelpers;
using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Propagators;
using DatadogTestLogger.Vendors.Datadog.Trace.RuntimeMetrics;
using DatadogTestLogger.Vendors.Datadog.Trace.Sampling;
using DatadogTestLogger.Vendors.Datadog.Trace.Telemetry;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient;
using ConfigurationKeys = DatadogTestLogger.Vendors.Datadog.Trace.Configuration.ConfigurationKeys;
using MetricsTransportType = DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Transport.TransportType;

namespace DatadogTestLogger.Vendors.Datadog.Trace
{
    internal class TracerManagerFactory
    {
        private const string UnknownServiceName = "UnknownService";
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor<TracerManagerFactory>();

        public static readonly TracerManagerFactory Instance = new();

        /// <summary>
        /// The primary factory method, called by <see cref="TracerManager"/>,
        /// providing the previous global <see cref="TracerManager"/> instance (may be null)
        /// </summary>
        internal TracerManager CreateTracerManager(ImmutableTracerSettings settings, TracerManager previous)
        {
            // TODO: If relevant settings have not changed, continue using existing statsd/agent writer/runtime metrics etc
            var tracer = CreateTracerManager(
                settings,
                agentWriter: null,
                sampler: null,
                scopeManager: previous?.ScopeManager, // no configuration, so can always use the same one
                statsd: null,
                runtimeMetrics: null,
                logSubmissionManager: previous?.DirectLogSubmission,
                telemetry: null,
                discoveryService: null,
                dataStreamsManager: null,
                spanSampler: null);

            try
            {
                if (Profiler.Instance.Status.IsProfilerReady)
                {
                    NativeInterop.SetApplicationInfoForAppDomain(RuntimeId.Get(), tracer.DefaultServiceName, tracer.Settings.Environment, tracer.Settings.ServiceVersion);
                }
            }
            catch (Exception ex)
            {
                // We failed to retrieve the runtime from native this can be because:
                // - P/Invoke issue (unknown dll, unknown entrypoint...)
                // - We are running in a partial trust environment
                Log.Warning(ex, "Failed to set the service name for native.");
            }

            return tracer;
        }

        /// <summary>
        /// Internal for use in tests that create "standalone" <see cref="TracerManager"/> by
        /// <see cref="Tracer(TracerSettings, IAgentWriter, ITraceSampler, IScopeManager, IDogStatsd, ITelemetryController, IDiscoveryService, ISpanSampler)"/>
        /// </summary>
        internal TracerManager CreateTracerManager(
            ImmutableTracerSettings settings,
            IAgentWriter agentWriter,
            ITraceSampler sampler,
            IScopeManager scopeManager,
            IDogStatsd statsd,
            RuntimeMetricsWriter runtimeMetrics,
            DirectLogSubmissionManager logSubmissionManager,
            ITelemetryController telemetry,
            IDiscoveryService discoveryService,
            DataStreamsManager dataStreamsManager,
            ISpanSampler spanSampler)
        {
            settings ??= ImmutableTracerSettings.FromDefaultSources();

            var defaultServiceName = settings.ServiceName ??
                                     GetApplicationName() ??
                                     UnknownServiceName;

            discoveryService ??= GetDiscoveryService(settings);

            statsd = settings.TracerMetricsEnabled
                         ? (statsd ?? CreateDogStatsdClient(settings, defaultServiceName))
                         : null;
            sampler ??= GetSampler(settings);
            spanSampler ??= GetSpanSampler(settings);
            agentWriter ??= GetAgentWriter(settings, statsd, sampler, discoveryService);
            scopeManager ??= new AsyncLocalScopeManager();

            if (settings.RuntimeMetricsEnabled && !DistributedTracer.Instance.IsChildTracer)
            {
                runtimeMetrics ??= new RuntimeMetricsWriter(statsd ?? CreateDogStatsdClient(settings, defaultServiceName), TimeSpan.FromSeconds(10));
            }

            logSubmissionManager = DirectLogSubmissionManager.Create(
                logSubmissionManager,
                settings.LogSubmissionSettings,
                defaultServiceName,
                settings.Environment,
                settings.ServiceVersion);

            telemetry ??= TelemetryFactory.CreateTelemetryController(settings);
            telemetry.RecordTracerSettings(settings, defaultServiceName, AzureAppServices.Metadata);
            telemetry.RecordSecuritySettings(Security.Instance.Settings);
            telemetry.RecordIastSettings(Datadog.Trace.Iast.Iast.Instance.Settings);
            telemetry.RecordProfilerSettings(Profiler.Instance);

            SpanContextPropagator.Instance = ContextPropagators.GetSpanContextPropagator(settings.PropagationStyleInject, settings.PropagationStyleExtract);

            dataStreamsManager ??= DataStreamsManager.Create(settings, discoveryService, defaultServiceName);

            var tracerManager = CreateTracerManagerFrom(settings, agentWriter, sampler, spanSampler, scopeManager, statsd, runtimeMetrics, logSubmissionManager, telemetry, discoveryService, dataStreamsManager, defaultServiceName);
            return tracerManager;
        }

        /// <summary>
        ///  Can be overriden to create a different <see cref="TracerManager"/>, e.g. <see cref="Ci.CITracerManager"/>
        /// </summary>
        protected virtual TracerManager CreateTracerManagerFrom(
            ImmutableTracerSettings settings,
            IAgentWriter agentWriter,
            ITraceSampler sampler,
            ISpanSampler spanSampler,
            IScopeManager scopeManager,
            IDogStatsd statsd,
            RuntimeMetricsWriter runtimeMetrics,
            DirectLogSubmissionManager logSubmissionManager,
            ITelemetryController telemetry,
            IDiscoveryService discoveryService,
            DataStreamsManager dataStreamsManager,
            string defaultServiceName)
            => new TracerManager(settings, agentWriter, sampler, spanSampler, scopeManager, statsd, runtimeMetrics, logSubmissionManager, telemetry, discoveryService, dataStreamsManager, defaultServiceName);

        protected virtual ITraceSampler GetSampler(ImmutableTracerSettings settings)
        {
            var sampler = new TraceSampler(new TracerRateLimiter(settings.MaxTracesSubmittedPerSecond));

            if (!string.IsNullOrWhiteSpace(settings.CustomSamplingRules))
            {
                foreach (var rule in CustomSamplingRule.BuildFromConfigurationString(settings.CustomSamplingRules))
                {
                    sampler.RegisterRule(rule);
                }
            }

            if (settings.GlobalSamplingRate != null)
            {
                var globalRate = (float)settings.GlobalSamplingRate;

                if (globalRate < 0f || globalRate > 1f)
                {
                    Log.Warning("{ConfigurationKey} configuration of {ConfigurationValue} is out of range", ConfigurationKeys.GlobalSamplingRate, settings.GlobalSamplingRate);
                }
                else
                {
                    sampler.RegisterRule(new GlobalSamplingRule(globalRate));
                }
            }

            return sampler;
        }

        protected virtual ISpanSampler GetSpanSampler(ImmutableTracerSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.SpanSamplingRules))
            {
                return new SpanSampler(Enumerable.Empty<ISpanSamplingRule>());
            }

            return new SpanSampler(SpanSamplingRule.BuildFromConfigurationString(settings.SpanSamplingRules));
        }

        protected virtual IAgentWriter GetAgentWriter(ImmutableTracerSettings settings, IDogStatsd statsd, ITraceSampler sampler, IDiscoveryService discoveryService)
        {
            var apiRequestFactory = TracesTransportStrategy.Get(settings.Exporter);
            var api = new Api(apiRequestFactory, statsd, rates => sampler.SetDefaultSampleRates(rates), settings.Exporter.PartialFlushEnabled);

            var statsAggregator = StatsAggregator.Create(api, settings, discoveryService);

            return new AgentWriter(api, statsAggregator, statsd, maxBufferSize: settings.TraceBufferSize);
        }

        protected virtual IDiscoveryService GetDiscoveryService(ImmutableTracerSettings settings)
            => DiscoveryService.Create(settings.Exporter);

        private static IDogStatsd CreateDogStatsdClient(ImmutableTracerSettings settings, string serviceName)
        {
            try
            {
                var constantTags = new List<string>
                                   {
                                       "lang:.NET",
                                       $"lang_interpreter:{FrameworkDescription.Instance.Name}",
                                       $"lang_version:{FrameworkDescription.Instance.ProductVersion}",
                                       $"tracer_version:{TracerConstants.AssemblyVersion}",
                                       $"service:{NormalizerTraceProcessor.NormalizeService(serviceName)}",
                                       $"{Tags.RuntimeId}:{Tracer.RuntimeId}"
                                   };

                if (settings.Environment != null)
                {
                    constantTags.Add($"env:{settings.Environment}");
                }

                if (settings.ServiceVersion != null)
                {
                    constantTags.Add($"version:{settings.ServiceVersion}");
                }

                var statsd = new DogStatsdService();
                switch (settings.Exporter.MetricsTransport)
                {
                    case MetricsTransportType.NamedPipe:
                        // Environment variables for windows named pipes are not explicitly passed to statsd.
                        // They are retrieved within the vendored code, so there is nothing to pass.
                        // Passing anything through StatsdConfig may cause bugs when windows named pipes should be used.
                        Log.Information("Using windows named pipes for metrics transport.");
                        statsd.Configure(new StatsdConfig
                        {
                            ConstantTags = constantTags.ToArray()
                        });
                        break;
#if NETCOREAPP3_1_OR_GREATER
                    case MetricsTransportType.UDS:
                        Log.Information("Using unix domain sockets for metrics transport.");
                        statsd.Configure(new StatsdConfig
                        {
                            StatsdServerName = $"{ExporterSettings.UnixDomainSocketPrefix}{settings.Exporter.MetricsUnixDomainSocketPath}",
                            ConstantTags = constantTags.ToArray()
                        });
                        break;
#endif
                    case MetricsTransportType.UDP:
                    default:
                        statsd.Configure(new StatsdConfig
                        {
                            StatsdServerName = settings.Exporter.AgentUri.DnsSafeHost,
                            StatsdPort = settings.Exporter.DogStatsdPort,
                            ConstantTags = constantTags.ToArray()
                        });
                        break;
                }

                return statsd;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unable to instantiate StatsD client.");
                return new NoOpStatsd();
            }
        }

        /// <summary>
        /// Gets an "application name" for the executing application by looking at
        /// the hosted app name (.NET Framework on IIS only), assembly name, and process name.
        /// </summary>
        /// <returns>The default service name.</returns>
        private static string GetApplicationName()
        {
            try
            {
                if (AzureAppServices.Metadata.IsRelevant)
                {
                    return AzureAppServices.Metadata.SiteName;
                }

                if (Serverless.Metadata is { IsRunningInLambda: true, ServiceName: var serviceName })
                {
                    return serviceName;
                }

                try
                {
                    if (TryLoadAspNetSiteName(out var siteName))
                    {
                        return siteName;
                    }
                }
                catch (Exception ex)
                {
                    // Unable to call into System.Web.dll
                    Log.Error(ex, "Unable to get application name through ASP.NET settings");
                }

                return Assembly.GetEntryAssembly()?.GetName().Name ??
                       ProcessHelpers.GetCurrentProcessName();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating default service name.");
                return null;
            }
        }

        private static bool TryLoadAspNetSiteName(out string siteName)
        {
#if NETFRAMEWORK
            // System.Web.dll is only available on .NET Framework
            if (System.Web.Hosting.HostingEnvironment.IsHosted)
            {
                // if this app is an ASP.NET application, return "SiteName/ApplicationVirtualPath".
                // note that ApplicationVirtualPath includes a leading slash.
                siteName = (System.Web.Hosting.HostingEnvironment.SiteName + System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath).TrimEnd('/');
                return true;
            }

#endif
            siteName = default;
            return false;
        }
    }
}
