using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
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

    [Parameter] public string Version = "0.0.34"; 

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

    Target Pack => _ => _
        .DependsOn(Compile)
        .Requires(() => Version, () => ArtifactsDirectory)
        .Executes(() =>
        {
            DotNetPack(x => x
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetVersion(Version)
                .SetOutputDirectory(ArtifactsDirectory)
            );
        });

    Target VendorDatadogTrace => _ => _
        .Executes(async () =>
        {
            var vendors = RootDirectory / "src" / "DatadogTestLogger" / "Vendors";
            await UpdateVendorsTool.UpdateVendors(TemporaryDirectory, vendors);
        });
}
