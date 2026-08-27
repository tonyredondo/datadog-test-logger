using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Pack);

    [Solution] readonly Solution Solution;

    [Parameter("Configuration to build - Default is 'Release'")]
    readonly Configuration Configuration = Configuration.Release;

    [Parameter("Where the NuGet package should be published")]
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "artifacts";

    [Parameter] public string Version = "0.0.59";

    Target Clean => _ => _
        .Executes(() =>
        {
            DotNetClean(x => x
                .SetProject(Solution)
                .SetConfiguration(Configuration)
            );
            
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .After(Clean)
        .Executes(() =>
        {
            DotNetRestore(x => x
                .SetProjectFile(Solution)
                .SetProperty("configuration", Configuration.ToString())
            );
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Requires(() => Version)
        .Executes(() =>
        {
            DotNetBuild(x => x
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .SetVersion(Version)
            );
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Requires(() => Version)
        .Executes(() =>
        {
            var project = Solution.GetProject("DatadogTestLogger.Test");
            DotNetTest(x => x
                .SetProjectFile(project)
                .SetConfiguration(Configuration)
                .EnableNoBuild());
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .DependsOn(Test)
        .Requires(() => Version, () => ArtifactsDirectory)
        .Executes(() =>
        {
            DotNetPack(x => x
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetVersion(Version)
                // Stamp the exact commit into the nuspec repository metadata so every
                // package is traceable to the source that produced it.
                .SetProperty("RepositoryCommit", GitTasks.Git("rev-parse HEAD").First().Text.Trim())
                .SetProperty("PackageOutputPath", ArtifactsDirectory)
            );

            NormalizePackages();
        });

    /// <summary>
    /// Normalizes the OPC container so packages are byte-for-byte reproducible: NuGet names
    /// the core-properties metadata part with a random GUID on every pack, so it is renamed
    /// to a fixed part (patching the .rels reference) and every entry timestamp is pinned to
    /// the ZIP epoch. Everything else in the packages is already deterministic.
    /// </summary>
    private void NormalizePackages()
    {
        var packages = ArtifactsDirectory.GlobFiles("*.nupkg")
            .Concat(ArtifactsDirectory.GlobFiles("*.snupkg"));

        foreach (var package in packages)
        {
            NormalizePackage(package);
            Logger.Normal($"Normalized {package.Name}");
        }
    }

    private static void NormalizePackage(AbsolutePath packagePath)
    {
        const string MetadataPartDirectory = "package/services/metadata/core-properties/";
        const string FixedMetadataPart = MetadataPartDirectory + "metadata.psmdcp";
        var zipEpoch = new DateTimeOffset(1980, 1, 1, 0, 0, 0, TimeSpan.Zero);

        var normalizedPath = $"{packagePath}.normalized";
        using (var sourceStream = File.OpenRead(packagePath))
        using (var source = new ZipArchive(sourceStream, ZipArchiveMode.Read))
        using (var targetStream = File.Create(normalizedPath))
        using (var target = new ZipArchive(targetStream, ZipArchiveMode.Create))
        {
            foreach (var entry in source.Entries)
            {
                var entryName = entry.FullName;
                string? rewrittenContent = null;

                if (entryName.EndsWith(".psmdcp", StringComparison.Ordinal))
                {
                    entryName = FixedMetadataPart;
                }
                else if (entryName.EndsWith(".rels", StringComparison.Ordinal))
                {
                    using var reader = new StreamReader(entry.Open());
                    var rels = reader.ReadToEnd();

                    var directoryIndex = rels.IndexOf(MetadataPartDirectory, StringComparison.Ordinal);
                    if (directoryIndex >= 0)
                    {
                        var nameStart = directoryIndex + MetadataPartDirectory.Length;
                        var extensionIndex = rels.IndexOf(".psmdcp", nameStart, StringComparison.Ordinal);
                        if (extensionIndex >= 0)
                        {
                            rels = rels.Remove(nameStart, extensionIndex - nameStart)
                                .Insert(nameStart, "metadata");
                        }
                    }

                    // Relationship ids are randomly generated on every pack: normalize them
                    // to sequential ids so the container bytes are stable across builds.
                    var relationshipIdIndex = 0;
                    rels = Regex.Replace(rels, "Id=\"R[0-9A-F]+\"", _ => $"Id=\"R{++relationshipIdIndex}\"");
                    rewrittenContent = rels;
                }

                var newEntry = target.CreateEntry(entryName, CompressionLevel.Optimal);
                newEntry.LastWriteTime = zipEpoch;
                using (var entryStream = entry.Open())
                using (var newEntryStream = newEntry.Open())
                {
                    if (rewrittenContent is not null)
                    {
                        var bytes = Encoding.UTF8.GetBytes(rewrittenContent);
                        newEntryStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        entryStream.CopyTo(newEntryStream);
                    }
                }
            }
        }

        File.Move(normalizedPath, packagePath, overwrite: true);
    }

    Target VendorDatadogTrace => _ => _
        .Executes(async () =>
        {
            await UpdateVendorsTool.UpdateVendorsTestLogger(TemporaryDirectory, RootDirectory / "src" / "DatadogTestLogger" / "Vendors");
            await UpdateVendorsTool.UpdateVendorsDataCollector(TemporaryDirectory, RootDirectory / "src" / "DatadogCollector" / "Vendors");
        });
}
