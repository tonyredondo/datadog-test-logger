// <copyright file="DatadogEnvironmentVariablesReplacer.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using System.Collections;

namespace DatadogTestLogger;

internal class DatadogEnvironmentVariablesReplacer : IDisposable
{
    private readonly Dictionary<string, string?> _environmentVariables;

    public DatadogEnvironmentVariablesReplacer(string prefix)
    {
        _environmentVariables = new Dictionary<string, string?>();
        foreach (DictionaryEntry? envVar in Environment.GetEnvironmentVariables())
        {
            if (envVar?.Key is string key)
            {
                var value = envVar?.Value?.ToString();
                _environmentVariables[key] = value;

                if (key.StartsWith(prefix, StringComparison.Ordinal))
                {
                    var newKey = "DD_" + key.Substring(prefix.Length);;
                    if (Environment.GetEnvironmentVariable(newKey) is null)
                    {
                        _environmentVariables[newKey] = null;
                    }
                    
                    Environment.SetEnvironmentVariable(newKey, value);
                }
            }
        }
    }
    
    public void Dispose()
    {
        foreach (var envVar in _environmentVariables)
        {
            Environment.SetEnvironmentVariable(envVar.Key, envVar.Value);
        }
    }
}