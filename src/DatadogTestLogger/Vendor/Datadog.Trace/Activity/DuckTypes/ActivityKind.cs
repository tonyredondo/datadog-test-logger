// <copyright file="ActivityKind.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

namespace Vendor.Datadog.Trace.Activity.DuckTypes
{
    internal enum ActivityKind
    {
        Internal = 0,
        Server = 1,
        Client = 2,
        Producer = 3,
        Consumer = 4
    }
}
