# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Datadog Test Logger is a .NET custom test logger and in-process data collector for .NET test frameworks (xUnit, NUnit, MSTest). It serializes test results and ships them to Datadog using vendored Datadog.Trace libraries. Published as a NuGet package.

## Build Commands

The project uses **Nuke** (C# build automation). The default target is `Pack` which runs: Restore → Compile → Test → Pack.

```bash
# Full build + test + NuGet pack (default pipeline)
./build.sh

# Run only tests (via dotnet directly)
dotnet test test/DatadogTestLogger.Test/DatadogTestLogger.Test.csproj -c Release

# Run a single test
dotnet test test/DatadogTestLogger.Test/DatadogTestLogger.Test.csproj -c Release --filter "FullyQualifiedName~TestMethodName"

# Build only
dotnet build -c Release

# Update vendored Datadog.Trace code
./build.sh --target VendorDatadogTrace
```

Requires .NET 7.0 SDK. `global.json` has `allowPrerelease: true`.

## Architecture

### Two main libraries

**DatadogTestLogger** (`src/DatadogTestLogger/`): The test logger.
- `DatadogTestLogger.cs` — Entry point, extends `Spekt.TestLogger.TestLogger`, registered via `[FriendlyName("datadog")]`
- `DatadogTestResultSerializer.cs` — Implements `ITestResultSerializer`, orchestrates serialization, validates `DD_API_KEY`, writes results to timestamped files
- `TestSuiteSerializer.cs` — Core logic: resolves test types/methods via reflection from assemblies, handles nested types and inheritance, uses vendored Datadog.Trace for CI metadata and direct submission
- `DatadogEnvironmentVariablesReplacer.cs` — IDisposable context manager that temporarily replaces `DD_*` env vars

**DatadogCollector** (`src/DatadogCollector/`): In-process data collector.
- `DatadogInProcCollector.cs` — Extends `InProcDataCollection`, delegates to specialized collectors
- `CpuInProcDataCollection.cs` — Tracks CPU usage per test case
- `DatadogCoverageCollector.cs` — Code coverage collection (uses vendored Mono.Cecil)
- `Configuration.cs` — Singleton (Lazy<T>), reads `DD_COLLECTOR_CPU_USAGE` (default: on) and `DD_COLLECTOR_COVERAGE` (default: off)

### Vendored dependencies

Both libraries vendor code from Datadog.Trace into `Vendors/` directories. These are updated via the `VendorDatadogTrace` Nuke target. Do not edit vendored files directly.

### Multi-targeting

Both libraries target: net7.0, net6.0, net5.0, netcoreapp3.1/3.0/2.2/2.1, net48, net472, net462. The csproj files define many conditional compilation symbols for framework-specific behavior. `AllowUnsafeBlocks` is enabled. Fody/ILMerge is used for assembly merging.

### Tests

- Unit tests in `test/DatadogTestLogger.Test/` use xUnit
- Sample projects in `src/Samples/` (XunitSample, MSTestSample, NUnitSample) demonstrate real usage

## Key Environment Variables

All Datadog variables use `DD_` prefix. Logger-specific ones use `DD_LOGGER_` prefix.
- `DD_API_KEY` — Required for submission
- `DD_COLLECTOR_CPU_USAGE` — Enable/disable CPU tracking (default: enabled)
- `DD_COLLECTOR_COVERAGE` — Enable/disable coverage (default: disabled)
