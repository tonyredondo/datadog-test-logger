//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="HttpRequest.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace Datadog.Trace.Vendors.Datadog.Trace.HttpOverStreams
{
    internal class HttpRequest : HttpMessage
    {
        public HttpRequest(string verb, string host, string path, HttpHeaders headers, IHttpContent content)
            : base(headers, content)
        {
            Verb = verb;
            Host = host;
            Path = path;
        }

        public string Verb { get; }

        public string Host { get; }

        public string Path { get; }
    }
}