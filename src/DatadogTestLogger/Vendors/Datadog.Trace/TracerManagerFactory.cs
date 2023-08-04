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
using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Propagators;
using DatadogTestLogger.Vendors.Datadog.Trace.RemoteConfigurationManagement;
using DatadogTestLogger.Vendors.Datadog.Trace.RemoteConfigurationManagement.Transport;
using DatadogTestLogger.Vendors.Datadog.Trace.RuntimeMetrics;
using DatadogTestLogger.Vendors.Datadog.Trace.Sampling;
using DatadogTestLogger.Vendors.Datadog.Trace.Telemetry;
using DatadogTestLogger.Vendors.Datadog.Trace.Telemetry.Metrics;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient;
using ConfigurationKeys = DatadogTestLogger.Vendors.Datadog.Trace.Configuration.ConfigurationKeys;
using MetricsTransportType = DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Transport.TransportType;
using Stopwatch = System.Diagnostics.Stopwatch;

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
                remoteConfigurationManager: null,
                dynamicConfigurationManager: null);

            try
            {
                if (Profiler.Instance.Status.IsProfilerReady)
                {
                    NativeInterop.SetApplicationInfoForAppDomain(RuntimeId.Get(), tracer.DefaultServiceName, tracer.Settings.EnvironmentInternal, tracer.Settings.ServiceVersionInternal);
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
        /// <see cref="Tracer(TracerSettings, IAgentWriter, ITraceSampler, IScopeManager, IDogStatsd, ITelemetryController, IDiscoveryService)"/>
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
            IRemoteConfigurationManager remoteConfigurationManager,
            IDynamicConfigurationManager dynamicConfigurationManager)
        {
            settings ??= new ImmutableTracerSettings(TracerSettings.FromDefaultSourcesInternal(), true);

            var defaultServiceName = settings.ServiceNameInternal ??
                GetApplicationName(settings) ??
                UnknownServiceName;

            discoveryService ??= GetDiscoveryService(settings);

            bool runtimeMetricsEnabled = settings.RuntimeMetricsEnabled && !DistributedTracer.Instance.IsChildTracer;

            statsd = (settings.TracerMetricsEnabledInternal || runtimeMetricsEnabled)
                         ? (statsd ?? CreateDogStatsdClient(settings, defaultServiceName))
                         : null;
            sampler ??= GetSampler(settings);
            agentWriter ??= GetAgentWriter(settings, settings.TracerMetricsEnabledInternal ? statsd : null, rates => sampler.SetDefaultSampleRates(rates), discoveryService);
            scopeManager ??= new AsyncLocalScopeManager();

            if (runtimeMetricsEnabled)
            {
                runtimeMetrics ??= new RuntimeMetricsWriter(statsd, TimeSpan.FromSeconds(10), settings.IsRunningInAzureAppService);
            }
            else
            {
                runtimeMetrics = null;
            }

            var gitMetadataTagsProvider = GetGitMetadataTagsProvider(settings, scopeManager);
            logSubmissionManager = DirectLogSubmissionManager.Create(
                logSubmissionManager,
                settings.LogSubmissionSettings,
                settings.AzureAppServiceMetadata,
                defaultServiceName,
                settings.EnvironmentInternal,
                settings.ServiceVersionInternal,
                gitMetadataTagsProvider);

            telemetry ??= TelemetryFactory.Instance.CreateTelemetryController(settings, discoveryService);
            telemetry.RecordTracerSettings(settings, defaultServiceName);

            var security = Security.Instance;
            telemetry.RecordSecuritySettings(security.Settings);
            TelemetryFactory.Metrics.SetWafVersion(security.DdlibWafVersion);
            telemetry.RecordIastSettings(Datadog.Trace.Iast.Iast.Instance.Settings);
            ErrorData? initError = !string.IsNullOrEmpty(security.InitializationError)
                                       ? new ErrorData(TelemetryErrorCode.AppsecConfigurationError, security.InitializationError)
                                       : null;
            telemetry.ProductChanged(TelemetryProductType.AppSec, enabled: security.Enabled, initError);

            var profiler = Profiler.Instance;
            telemetry.RecordProfilerSettings(profiler);
            telemetry.ProductChanged(TelemetryProductType.Profiler, enabled: profiler.Status.IsProfilerReady, error: null);

            SpanContextPropagator.Instance = SpanContextPropagatorFactory.GetSpanContextPropagator(settings.PropagationStyleInject, settings.PropagationStyleExtract);

            dataStreamsManager ??= DataStreamsManager.Create(settings, discoveryService, defaultServiceName);

            if (remoteConfigurationManager == null)
            {
                var sw = Stopwatch.StartNew();

                var rcmSettings = RemoteConfigurationSettings.FromDefaultSource();
                var rcmApi = RemoteConfigurationApiFactory.Create(settings.ExporterInternal, rcmSettings, discoveryService);

                // Service Name must be lowercase, otherwise the agent will not be able to find the service
                var serviceName = TraceUtil.NormalizeTag(settings.ServiceNameInternal ?? defaultServiceName);

                remoteConfigurationManager = RemoteConfigurationManager.Create(discoveryService, rcmApi, rcmSettings, serviceName, settings, gitMetadataTagsProvider, RcmSubscriptionManager.Instance);

                TelemetryFactory.Metrics.RecordDistributionInitTime(MetricTags.InitializationComponent.Rcm, sw.ElapsedMilliseconds);
            }

            dynamicConfigurationManager ??= new DynamicConfigurationManager(RcmSubscriptionManager.Instance);

            return CreateTracerManagerFrom(
                settings,
                agentWriter,
                scopeManager,
                statsd,
                runtimeMetrics,
                logSubmissionManager,
                telemetry,
                discoveryService,
                dataStreamsManager,
                defaultServiceName,
                gitMetadataTagsProvider,
                sampler,
                GetSpanSampler(settings),
                remoteConfigurationManager,
                dynamicConfigurationManager);
        }

        protected virtual IGitMetadataTagsProvider GetGitMetadataTagsProvider(ImmutableTracerSettings settings, IScopeManager scopeManager)
        {
            return new GitMetadataTagsProvider(settings, scopeManager);
        }

        /// <summary>
        ///  Can be overriden to create a different <see cref="TracerManager"/>, e.g. <see cref="Ci.CITracerManager"/>
        /// </summary>
        protected virtual TracerManager CreateTracerManagerFrom(
            ImmutableTracerSettings settings,
            IAgentWriter agentWriter,
            IScopeManager scopeManager,
            IDogStatsd statsd,
            RuntimeMetricsWriter runtimeMetrics,
            DirectLogSubmissionManager logSubmissionManager,
            ITelemetryController telemetry,
            IDiscoveryService discoveryService,
            DataStreamsManager dataStreamsManager,
            string defaultServiceName,
            IGitMetadataTagsProvider gitMetadataTagsProvider,
            ITraceSampler traceSampler,
            ISpanSampler spanSampler,
            IRemoteConfigurationManager remoteConfigurationManager,
            IDynamicConfigurationManager dynamicConfigurationManager)
            => new TracerManager(settings, agentWriter, scopeManager, statsd, runtimeMetrics, logSubmissionManager, telemetry, discoveryService, dataStreamsManager, defaultServiceName, gitMetadataTagsProvider, traceSampler, spanSampler, remoteConfigurationManager, dynamicConfigurationManager);

        protected virtual ITraceSampler GetSampler(ImmutableTracerSettings settings)
        {
            var sampler = new TraceSampler(new TracerRateLimiter(settings.MaxTracesSubmittedPerSecondInternal));

            if (!string.IsNullOrWhiteSpace(settings.CustomSamplingRulesInternal))
            {
                foreach (var rule in CustomSamplingRule.BuildFromConfigurationString(settings.CustomSamplingRulesInternal))
                {
                    sampler.RegisterRule(rule);
                }
            }

            if (settings.GlobalSamplingRateInternal != null)
            {
                var globalRate = (float)settings.GlobalSamplingRateInternal;

                if (globalRate < 0f || globalRate > 1f)
                {
                    Log.Warning("{ConfigurationKey} configuration of {ConfigurationValue} is out of range", ConfigurationKeys.GlobalSamplingRate, settings.GlobalSamplingRateInternal);
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

        protected virtual IAgentWriter GetAgentWriter(ImmutableTracerSettings settings, IDogStatsd statsd, Action<Dictionary<string, float>> updateSampleRates, IDiscoveryService discoveryService)
        {
            var apiRequestFactory = TracesTransportStrategy.Get(settings.ExporterInternal);
            var api = new Api(apiRequestFactory, statsd, updateSampleRates, settings.ExporterInternal.PartialFlushEnabledInternal);

            var statsAggregator = StatsAggregator.Create(api, settings, discoveryService);

            return new AgentWriter(api, statsAggregator, statsd, maxBufferSize: settings.TraceBufferSize);
        }

        protected virtual IDiscoveryService GetDiscoveryService(ImmutableTracerSettings settings)
            => DiscoveryService.Create(settings.ExporterInternal);

        internal static IDogStatsd CreateDogStatsdClient(ImmutableTracerSettings settings, List<string> constantTags, string prefix = null)
        {
            try
            {
                if (settings.EnvironmentInternal != null)
                {
                    constantTags?.Add($"env:{settings.EnvironmentInternal}");
                }

                if (settings.ServiceVersionInternal != null)
                {
                    constantTags?.Add($"version:{settings.ServiceVersionInternal}");
                }

                var statsd = new DogStatsdService();
                switch (settings.ExporterInternal.MetricsTransport)
                {
                    case MetricsTransportType.NamedPipe:
                        // Environment variables for windows named pipes are not explicitly passed to statsd.
                        // They are retrieved within the vendored code, so there is nothing to pass.
                        // Passing anything through StatsdConfig may cause bugs when windows named pipes should be used.
                        Log.Information("Using windows named pipes for metrics transport.");
                        statsd.Configure(new StatsdConfig
                        {
                            ConstantTags = constantTags?.ToArray(),
                            Prefix = prefix
                        });
                        break;
#if NETCOREAPP3_1_OR_GREATER
                    case MetricsTransportType.UDS:
                        Log.Information("Using unix domain sockets for metrics transport.");
                        statsd.Configure(new StatsdConfig
                        {
                            StatsdServerName = $"{ExporterSettings.UnixDomainSocketPrefix}{settings.ExporterInternal.MetricsUnixDomainSocketPathInternal}",
                            ConstantTags = constantTags?.ToArray(),
                            Prefix = prefix
                        });
                        break;
#endif
                    case MetricsTransportType.UDP:
                    default:
                        statsd.Configure(new StatsdConfig
                        {
                            StatsdServerName = settings.ExporterInternal.AgentUriInternal.DnsSafeHost,
                            StatsdPort = settings.ExporterInternal.DogStatsdPortInternal,
                            ConstantTags = constantTags?.ToArray(),
                            Prefix = prefix
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

        private static IDogStatsd CreateDogStatsdClient(ImmutableTracerSettings settings, string serviceName)
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

            return CreateDogStatsdClient(settings, constantTags);
        }

        /// <summary>
        /// Gets an "application name" for the executing application by looking at
        /// the hosted app name (.NET Framework on IIS only), assembly name, and process name.
        /// </summary>
        /// <returns>The default service name.</returns>
        private static string GetApplicationName(ImmutableTracerSettings settings)
        {
            try
            {
                if (settings.IsRunningInAzureAppService)
                {
                    return settings.AzureAppServiceMetadata.SiteName;
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

            siteName = default;
            return false;
        }
    }
}
