using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using Serilog;

public static class UpdateVendorsTool
{
    public static async Task UpdateVendorsTestLogger(
        AbsolutePath downloadDirectory,
        AbsolutePath vendorDirectory)
    {
        foreach (var dependency in VendoredDependency.AllTestLogger)
        {
            await UpdateVendor(dependency, downloadDirectory, vendorDirectory);
        }
    }

    public static async Task UpdateVendorsDataCollector(
        AbsolutePath downloadDirectory,
        AbsolutePath vendorDirectory)
    {
        foreach (var dependency in VendoredDependency.AllDataCollector)
        {
            await UpdateVendor(dependency, downloadDirectory, vendorDirectory);
        }
    }

    private static async Task UpdateVendor(VendoredDependency dependency, AbsolutePath downloadDirectory, AbsolutePath vendorDirectory)
    {
        var libraryName = dependency.LibraryName;
        var downloadUrl = dependency.DownloadUrl;
        var pathToSrc = dependency.PathToSrc;

        Log.Information($"Starting {libraryName} upgrade.");

        var zipLocation = Path.Combine(downloadDirectory, $"{libraryName}.zip");
        var extractLocation = Path.Combine(downloadDirectory, $"{libraryName}");
        var vendorFinalPath = Path.Combine(vendorDirectory, libraryName);
        var sourceUrlLocation = Path.Combine(vendorFinalPath, "_last_downloaded_source_url.txt");

        // Ensure the url has changed, or don't bother upgrading
        if (File.Exists(sourceUrlLocation) && !string.IsNullOrEmpty(dependency.Version))
        {
            var currentSource = File.ReadAllText(sourceUrlLocation);
            if (currentSource.Equals(downloadUrl, StringComparison.OrdinalIgnoreCase))
            {
                Log.Information($"No updates to be made for {libraryName}.");
                return;
            }
        }

        if (File.Exists(zipLocation))
        {
            Log.Information($"{libraryName} zip already downloaded to {zipLocation}. Skipping download");
        }
        else
        {
            using (var repoDownloadClient = new HttpClient())
            {
                await using var stream = await repoDownloadClient.GetStreamAsync(downloadUrl);
                await using var file = File.Create(zipLocation);
                await stream.CopyToAsync(file);
            }
            Log.Information($"Downloaded {libraryName} upgrade.");
        }

        FileSystemTasks.EnsureCleanDirectory(extractLocation);
        ZipFile.ExtractToDirectory(zipLocation, extractLocation);

        Log.Information($"Unzipped {libraryName} upgrade.");

        var srcRoot = string.IsNullOrEmpty(dependency.ZipFilePrefix)
            ? pathToSrc
            : pathToSrc.Prepend(string.Format(dependency.ZipFilePrefix, dependency.Version));

        var sourceLocation = Path.Combine(srcRoot.Prepend(extractLocation).ToArray());
        var projFile = Path.Combine(sourceLocation, $"{libraryName}.csproj");

        // Rename the proj file to a txt for reference
        File.Copy(projFile, projFile + ".txt");
        File.Delete(projFile);
        Log.Information($"Renamed {libraryName} project file.");

        // Delete the assembly properties
        var assemblyPropertiesFolder = Path.Combine(sourceLocation, @"Properties");
        FileSystemTasks.DeleteDirectory(assemblyPropertiesFolder);
        Log.Information($"Deleted {libraryName} assembly properties file.");

        Log.Information($"Running transforms on files for {libraryName}.");

        dependency.RelativeGlobsToExclude
            .SelectMany(x => ((AbsolutePath) sourceLocation).GlobFiles(x))
            .ForEach(FileSystemTasks.DeleteFile);

        var files = Directory.GetFiles(
            sourceLocation,
            "*.*",
            SearchOption.AllDirectories);

        foreach (var file in files)
        {
            dependency.Transform(file);
        }

        Log.Information($"Finished transforms on files for {libraryName}.");

        // Move it all to the vendors directory
        Log.Information($"Copying source of {libraryName} to vendor project.");
        FileSystemTasks.EnsureExistingDirectory(vendorFinalPath);
        FileSystemTasks.EnsureCleanDirectory(vendorFinalPath);
        FileSystemTasks.MoveDirectory(sourceLocation, vendorFinalPath, DirectoryExistsPolicy.Merge);
        File.WriteAllText(sourceUrlLocation, downloadUrl);
        Log.Information($"Finished {libraryName} upgrade.");
    }
}