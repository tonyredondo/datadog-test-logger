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

    [Theory]
    [InlineData(@"D:\a\_work\1\s\src\SampleTests.cs")]
    [InlineData("D:/a/_work/1/s/src/SampleTests.cs")]
    [InlineData("/home/vsts/work/1/s/src/SampleTests.cs")]
    [InlineData("file:///D:/a/_work/1/s/src/SampleTests.cs")]
    [InlineData("../../../_/src/SampleTests.cs")]
    public void ResolvesRelocatedCompilerSourcePath(string sourcePath)
    {
        using var repositoryDirectory = new TemporaryDirectory();
        var sourceDirectory = Path.Combine(repositoryDirectory.Path, "src");
        Directory.CreateDirectory(sourceDirectory);
        File.WriteAllText(Path.Combine(repositoryDirectory.Path, "CODEOWNERS"), "/src/ @repository-owner");
        File.WriteAllText(Path.Combine(sourceDirectory, "SampleTests.cs"), "class SampleTests {}");
        var resolver = new CodeOwnersResolver(repositoryDirectory.Path, repositoryDirectory.Path, Repository, "github");

        var ownership = resolver.Resolve(sourcePath, useOSSeparator: false);

        Assert.True(ownership.IsRepositoryRelative);
        Assert.Equal("src/SampleTests.cs", ownership.RepositoryRelativePath);
        Assert.Equal(new[] { "@repository-owner" }, ownership.MatchingOwners);
    }

    [Fact]
    public void ResolvesExistingCompilerSourcePathOutsideRepositoryByItsRepositorySuffix()
    {
        using var repositoryDirectory = new TemporaryDirectory();
        using var buildDirectory = new TemporaryDirectory();
        var repositorySourceDirectory = Path.Combine(repositoryDirectory.Path, "src");
        var buildSourceDirectory = Path.Combine(buildDirectory.Path, "src");
        Directory.CreateDirectory(repositorySourceDirectory);
        Directory.CreateDirectory(buildSourceDirectory);
        File.WriteAllText(Path.Combine(repositoryDirectory.Path, "CODEOWNERS"), "/src/ @repository-owner");
        File.WriteAllText(Path.Combine(repositorySourceDirectory, "SampleTests.cs"), "class SampleTests {}");
        var compilerSourcePath = Path.Combine(buildSourceDirectory, "SampleTests.cs");
        File.WriteAllText(compilerSourcePath, "class SampleTests {}");
        var resolver = new CodeOwnersResolver(repositoryDirectory.Path, repositoryDirectory.Path, Repository, "github");

        var ownership = resolver.Resolve(compilerSourcePath, useOSSeparator: false);

        Assert.True(ownership.IsRepositoryRelative);
        Assert.Equal("src/SampleTests.cs", ownership.RepositoryRelativePath);
        Assert.Equal(new[] { "@repository-owner" }, ownership.MatchingOwners);
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

    [Fact]
    public void UsesValidatedLocalCheckoutForMirroredCiWorkspace()
    {
        using var repositoryDirectory = new TemporaryDirectory();
        using var remoteCiDirectory = new TemporaryDirectory();
        var repositoryRoot = repositoryDirectory.Path;
        var sourceDirectory = Path.Combine(repositoryRoot, "src");
        Directory.CreateDirectory(Path.Combine(repositoryRoot, ".git"));
        Directory.CreateDirectory(Path.Combine(repositoryRoot, ".github"));
        Directory.CreateDirectory(sourceDirectory);
        File.WriteAllText(Path.Combine(repositoryRoot, ".github", "CODEOWNERS"), "/src/ @repository-owner");
        File.WriteAllText(Path.Combine(sourceDirectory, "SampleTests.cs"), "class SampleTests {}");
        var resolver = new CodeOwnersResolver(
            remoteCiDirectory.Path,
            remoteCiDirectory.Path,
            "https://gitlab.example.com/mirror/datadog-test-logger.git",
            "gitlab",
            repositoryRoot,
            Repository);

        var ownership = resolver.Resolve(@"D:\build\datadog-test-logger\src\SampleTests.cs", useOSSeparator: false);

        Assert.True(resolver.HasCodeOwners);
        Assert.True(ownership.IsRepositoryRelative);
        Assert.Equal("src/SampleTests.cs", ownership.RepositoryRelativePath);
        Assert.Equal(new[] { "@repository-owner" }, ownership.MatchingOwners);
    }

    [Fact]
    public void DoesNotUseLocalCheckoutWhenItWasNotValidated()
    {
        using var repositoryDirectory = new TemporaryDirectory();
        using var remoteCiDirectory = new TemporaryDirectory();
        var repositoryRoot = repositoryDirectory.Path;
        Directory.CreateDirectory(Path.Combine(repositoryRoot, ".git"));
        Directory.CreateDirectory(Path.Combine(repositoryRoot, ".github"));
        File.WriteAllText(Path.Combine(repositoryRoot, ".github", "CODEOWNERS"), "* @repository-owner");
        var resolver = new CodeOwnersResolver(
            remoteCiDirectory.Path,
            remoteCiDirectory.Path,
            "https://gitlab.example.com/mirror/datadog-test-logger.git",
            "gitlab",
            localRepositoryRoot: null,
            localRepository: null);

        Assert.False(resolver.HasCodeOwners);
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
