using DatadogTestLogger.Vendors.Datadog.Trace.Ci.CodeOwnership;

namespace DatadogTestLogger.Test;

public class CodeOwnershipTests
{
    private const string Repository = "https://github.com/tonyredondo/datadog-test-logger";

    [Fact]
    public void DoesNotMatchCodeOwnersForSourceOutsideRepository()
    {
        using var repositoryDirectory = new TemporaryDirectory();
        using var externalDirectory = new TemporaryDirectory();
        File.WriteAllText(Path.Combine(repositoryDirectory.Path, "CODEOWNERS"), "* @repository-owner");
        var externalFile = Path.Combine(externalDirectory.Path, "ExternalTests.cs");
        File.WriteAllText(externalFile, "class ExternalTests {}");
        var resolver = new CodeOwnersResolver(repositoryDirectory.Path, repositoryDirectory.Path, Repository, "github");

        var ownership = resolver.Resolve(externalFile, useOSSeparator: false);

        Assert.False(ownership.IsRepositoryRelative);
        Assert.Empty(ownership.MatchingOwners);
        Assert.Null(ownership.CodeOwnersTag);
    }

    [Fact]
    public void UsesGitRootInsteadOfNestedCodeOwnersFile()
    {
        using var repositoryDirectory = new TemporaryDirectory();
        var repositoryRoot = repositoryDirectory.Path;
        var sourceRoot = Path.Combine(repositoryRoot, "src");
        Directory.CreateDirectory(Path.Combine(repositoryRoot, ".git"));
        Directory.CreateDirectory(sourceRoot);
        File.WriteAllText(Path.Combine(repositoryRoot, "CODEOWNERS"), "* @repository-owner");
        File.WriteAllText(Path.Combine(sourceRoot, "CODEOWNERS"), "* @nested-owner");
        var sourceFile = Path.Combine(sourceRoot, "SampleTests.cs");
        File.WriteAllText(sourceFile, "class SampleTests {}");
        var resolver = new CodeOwnersResolver(sourceRoot, sourceRoot, Repository, "github");

        var ownership = resolver.Resolve(sourceFile, useOSSeparator: false);

        Assert.True(ownership.IsRepositoryRelative);
        Assert.Equal("src/SampleTests.cs", ownership.RepositoryRelativePath);
        Assert.Equal(new[] { "@repository-owner" }, ownership.MatchingOwners);
    }

    private sealed class TemporaryDirectory : IDisposable
    {
        public TemporaryDirectory()
        {
            Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "datadog-test-logger-codeowners-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(Path);
        }

        public string Path { get; }

        public void Dispose()
        {
            try
            {
                Directory.Delete(Path, recursive: true);
            }
            catch
            {
                // Cleanup failure should not fail the test.
            }
        }
    }
}
