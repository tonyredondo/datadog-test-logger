﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class VendoredDependency
{
    const string AutoGeneratedMessage =
        @"//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
";

    static VendoredDependency()
    {
        All.Add(new()
        {
            LibraryName = "Datadog.Trace",
            DownloadUrl = "https://github.com/DataDog/dd-trace-dotnet/archive/refs/heads/master.zip",
            ZipFilePrefix = "dd-trace-dotnet-master",
            PathToSrc = new[] {"tracer", "src", "Datadog.Trace"},
            RelativeGlobsToExclude = new[]
            {
                "AppSec/**/*.*",
                "AspNet/**/*.*",
                "ClrProfiler/AutoInstrumentation/AspNet/**/*.*",
                "ClrProfiler/AutoInstrumentation/AspNetCore/**/*.*",
                "ClrProfiler/AutoInstrumentation/Azure/**/*.*",
                "ClrProfiler/AutoInstrumentation/Grpc/GrpcDotNet/**/*.*",
                "DiagnosticListeners/AspNetCoreResourceNameHelper.cs",
                "DiagnosticListeners/AspNetCoreDiagnosticObserver.cs",
                "Headers/HeadersCollectionAdapter.cs",
                "PlatformHelpers/AspNetCoreHttpRequestHandler.cs",
                "Util/Http/HttpRequestExtensions.Core.cs",
                "Util/Http/HttpRequestExtensions.Core.cs",
                "Util/Http/HttpRequestExtensions.Framework.cs",
            },
            Transform = filePath => RewriteCsFileWithStandardTransform(filePath, originalNamespace: "Datadog.Trace",
                AddPreprocessorsToGeneratedCode, AddTracerManagerFactoryHack, RenameLogFile),
        });
    }

    public static List<VendoredDependency> All { get; set; } = new List<VendoredDependency>();

    public string LibraryName { get; set; }

    public string Version { get; set; }

    public string DownloadUrl { get; set; }

    public string ZipFilePrefix { get; set; }

    public string[] PathToSrc { get; set; }

    public Action<string> Transform { get; set; }

    public string[] RelativeGlobsToExclude { get; set; } = Array.Empty<string>();

    private static string AddIfNetcoreapp31OrGreater(string filePath, string content)
    {
        return "#if NETCOREAPP3_1_OR_GREATER" + Environment.NewLine + content + Environment.NewLine + "#endif";
    }

    private static string AddPreprocessorsToGeneratedCode(string filePath, string content)
    {
        var replacements = new[]
        {
            (tfm: "net461", condition: "NETFRAMEWORK"),
            (tfm: "netstandard2.0", condition: "!NETFRAMEWORK && !NETCOREAPP3_1_OR_GREATER"),
            (tfm: "netcoreapp3.1", condition: "NETCOREAPP3_1_OR_GREATER && !NET6_0_OR_GREATER"),
            (tfm: "net6.0", condition: "NET6_0_OR_GREATER"),
        };

        foreach (var replacement in replacements)
        {
            var generatedPath = Path.Combine("tracer", "src", "Datadog.Trace", "Generated", replacement.tfm);
            if (filePath.Contains(generatedPath))
            {
                return $"#if {replacement.condition}" + Environment.NewLine + content + Environment.NewLine + "#endif";
            }
        }

        return content;
    }

    private static string AddTracerManagerFactoryHack(string filePath, string content)
    {
        if (Path.GetFileName(filePath) != "TracerManagerFactory.cs")
        {
            return content;
        }

        var lines = new[]
        {
            @"#if NETFRAMEWORK",
            @"            // System.Web.dll is only available on .NET Framework",
            @"            if (System.Web.Hosting.HostingEnvironment.IsHosted)",
            @"            {",
            @"                // if this app is an ASP.NET application, return ""SiteName/ApplicationVirtualPath"".",
            @"                // note that ApplicationVirtualPath includes a leading slash.",
            @"                siteName = (System.Web.Hosting.HostingEnvironment.SiteName + System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath).TrimEnd('/');",
            @"                return true;",
            @"            }",
            "",
            @"#endif",
        };

        // only one of these work, but play it safe
        return content
            .Replace(string.Join("\n", lines), string.Empty)
            .Replace(string.Join("\r\n", lines), string.Empty);
    }

    private static string RenameLogFile(string filePath, string content)
    {
        if (Path.GetFileName(filePath) != "DatadogLogging.cs")
        {
            return content;
        }

        return content.Replace(
            @$"var managedLogPath = Path.Combine(logDirectory, $""dotnet-tracer-managed-",
            @$"var managedLogPath = Path.Combine(logDirectory, $""datadogtestlogger-");
    }

    private static string AddNullableDirectiveTransform(string filePath, string content)
    {
        if (!content.Contains("#nullable"))
        {
            return "#nullable enable" + Environment.NewLine + content;
        }

        return content;
    }

    private static void RewriteCsFileWithStandardTransform(string filePath, string originalNamespace,
        params Func<string, string, string>[] extraTransform)
    {
        if (string.Equals(Path.GetExtension(filePath), ".cs", StringComparison.OrdinalIgnoreCase))
        {
            RewriteFileWithTransform(
                filePath,
                content =>
                {
                    foreach (var transform in extraTransform)
                    {
                        if (transform != null)
                        {
                            content = transform(filePath, content);
                        }
                    }

                    // Disable analyzer
                    var builder = new StringBuilder(AutoGeneratedMessage, content.Length * 2);
                    builder.AppendLine(GenerateWarningDisablePragma());
                    builder.Append(content);

                    builder.Replace("#if !NET461", "#if !NETFRAMEWORK");
                    builder.Replace("#if NET461", "#if NETFRAMEWORK");

                    // Debugger.Break() is a dangerous method that may crash the process. We don't
                    // want to take any risk of calling it, ever, so replace it with a noop.
                    builder.Replace("Debugger.Break();", "{}");

                    // Prevent namespace conflicts
                    builder.Replace($"using {originalNamespace}",
                        $"using DatadogTestLogger.Vendors.{originalNamespace}");
                    builder.Replace($"using static {originalNamespace}",
                        $"using static DatadogTestLogger.Vendors.{originalNamespace}");
                    builder.Replace($"namespace {originalNamespace}",
                        $"namespace DatadogTestLogger.Vendors.{originalNamespace}");
                    builder.Replace($"[CLSCompliant(false)]", $"// [CLSCompliant(false)]");

                    // HACKS
                    builder.Replace("using Datadog;", "using DatadogTestLogger;");
                    builder.Replace($"[assembly: {originalNamespace}",
                        $"[assembly: DatadogTestLogger.Vendors.{originalNamespace}");
                    builder.Replace("public EventHandler<ErrorEventArgs>? Error { get; set; }",
                        @"public EventHandler<global::DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json.Serialization.ErrorEventArgs>? Error { get; set; }");
                    builder.Replace(
                        "return new HttpResponse(statusCode, reasonPhrase, headers, new StreamContent(responseStream, length));",
                        "return new HttpResponse(statusCode, reasonPhrase, headers, new global::DatadogTestLogger.Vendors.Datadog.Trace.HttpOverStreams.HttpContent.StreamContent(responseStream, length));");
                    var replacement =
                        "            _statsd.Gauge(MetricsNames.ThreadPoolWorkersCount, ThreadPool.ThreadCount);";
                    builder.Replace(replacement,
                        "#if NETCOREAPP3_0_OR_GREATER" + Environment.NewLine + replacement + Environment.NewLine +
                        "#endif");
                    replacement = "                    _gcStart = eventData.TimeStamp;";
                    builder.Replace(replacement,
                        "#if NETCOREAPP2_2_OR_GREATER" + Environment.NewLine + replacement + Environment.NewLine +
                        "#endif");
                    replacement =
                        "                        _statsd.Timer(MetricsNames.GcPauseTime, (eventData.TimeStamp - start.Value).TotalMilliseconds);";
                    builder.Replace(replacement,
                        "#if NETCOREAPP2_2_OR_GREATER" + Environment.NewLine + replacement + Environment.NewLine +
                        "#endif");
                    builder.Replace("var lastChar = eventName[^1];", "var lastChar = eventName.Last();");
                    builder.Replace("var sw = new StreamWriter(memoryStream, leaveOpen: true);",
                        "var sw = new StreamWriter(memoryStream, encoding: global::DatadogTestLogger.EncodingCache.UTF8NoBOM, bufferSize: -1, leaveOpen: true);");
                    builder.Replace("#if !NETCOREAPP3_1_OR_GREATER && !NET461_OR_GREATER && !NETSTANDARD2_0",
                        "#if !NETCOREAPP2_0_OR_GREATER && !NET461_OR_GREATER && !NETSTANDARD2_0");
                    builder.Replace("NET7_0_OR_GREATER", "NET8_0_OR_GREATER");

                    // Fix namespace conflicts in `using alias` directives. For example, transform:
                    //      using Foo = dnlib.A.B.C;
                    // To:
                    //      using Foo = DatadogTestLogger.Vendors.dnlib.A.B.C;
                    string result =
                        Regex.Replace(
                            builder.ToString(),
                            @$"using\s+(\S+)\s+=\s+{Regex.Escape(originalNamespace)}.(.*);",
                            match =>
                                $"using {match.Groups[1].Value} = DatadogTestLogger.Vendors.{originalNamespace}.{match.Groups[2].Value};");


                    // Don't expose anything we don't intend to
                    // by replacing all "public" access modifiers with "internal"
                    return Regex.Replace(
                        result,
                        @"public(\s+((abstract|sealed|static|unsafe)\s+)*?(partial\s+)?(class|readonly\s+(ref\s+)?struct|struct|interface|enum|delegate))",
                        match => $"internal{match.Groups[1]}");
                });
        }
    }

    static string GenerateWarningDisablePragma() =>
        "#pragma warning disable " +
        "CS0618, " + // Type or member is obsolete
        "CS0649, " + // Field is never assigned to, and will always have its default value
        "CS1574, " + // XML comment has a cref attribute that could not be resolved
        "CS1580, " + // Invalid type for parameter in XML comment cref attribute
        "CS1581, " + // Invalid return type in XML comment cref attribute
        "CS1584, " + // XML comment has syntactically incorrect cref attribute
        "CS1591, " + // Missing XML comment for publicly visible type or member 'x'
        "CS1573, " + // Parameter 'x' has no matching param tag in the XML comment for 'y' (but other parameters do)
        "CS8018, " + // Within cref attributes, nested types of generic types should be qualified
        "SYSLIB0011, " + // BinaryFormatter serialization is obsolete and should not be used.
        "SYSLIB0032"; // Recovery from corrupted process state exceptions is not supported; HandleProcessCorruptedStateExceptionsAttribute is ignored."

    static string AddIgnoreNullabilityWarningDisablePragma(string filePath, string content) =>
        "#pragma warning disable " +
        "CS8600, " + // Converting null literal or possible null value to non-nullable type.
        "CS8601, " + // Possible null reference assignment
        "CS8602, " + // Dereference of a possibly null reference
        "CS8603, " + // Possible null reference return
        "CS8604, " + // Possible null reference argument for parameter 'x' in 'y'
        "CS8618, " + // Non-nullable field 'x' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
        "CS8620, " + // Argument of type 'x' cannot be used for parameter 'y' of type 'z[]' in 'a' due to differences in the nullability of reference types.
        "CS8714, " + // The type 'x' cannot be used as type parameter 'y' in the generic type or method 'z'. Nullability of type argument 'x' doesn't match 'notnull' constraint.
        "CS8762, " + // Parameter 'x' must have a non-null value when exiting with 'true'
        "CS8765, " + // Nullability of type of parameter 'x' doesn't match overridden member (possibly because of nullability attributes)
        "CS8766, " + // Nullability of reference types in return type of 'x' doesn't match implicitly implemented member 'y' (possibly because of nullability attributes)
        "CS8767, " + // Nullability of reference types in type of parameter 'x' of 'y' doesn't match implicitly implemented member 'z' (possibly because of nullability attributes)
        "CS8768, " + // Nullability of reference types in return type doesn't match implemented member 'x' (possibly because of nullability attributes)
        "CS8769, " + // Nullability of reference types in type of parameter 'x' doesn't match implemented member 'y'  (possibly because of nullability attributes)
        "CS8612, " + // Nullability of reference types in type of 'x' doesn't match implicitly implemented member 'y'.
        "CS8629, " + // Nullable value type may be null with temporary variables
        "CS8774" + // Member 'x' must have a non-null value when exiting.
        Environment.NewLine + content;

    private static void RewriteFileWithTransform(string filePath, Func<string, string> transform)
    {
        var fileContent = File.ReadAllText(filePath);
        fileContent = transform(fileContent);
        // Normalize text to use CRLF line endings so we have deterministic results
        fileContent = fileContent.Replace("\r\n", "\n").Replace("\n", "\r\n");
        File.WriteAllText(
            filePath,
            fileContent,
            new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }
}