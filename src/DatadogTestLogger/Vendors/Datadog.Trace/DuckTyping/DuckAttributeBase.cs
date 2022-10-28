//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DuckAttributeBase.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

using System;
using System.Reflection;

namespace Datadog.Trace.Vendors.Datadog.Trace.DuckTyping
{
    /// <summary>
    /// Duck attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
    internal abstract class DuckAttributeBase : Attribute
    {
        /// <summary>
        /// Gets or sets the underlying type member name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the binding flags
        /// </summary>
        public BindingFlags BindingFlags { get; set; } = DuckAttribute.DefaultFlags;

        /// <summary>
        /// Gets or sets the generic parameter type names definition for a generic method call (required when calling generic methods and instance type is non public)
        /// </summary>
        public string[]? GenericParameterTypeNames { get; set; }

        /// <summary>
        /// Gets or sets the parameter type names of the target method (optional / used to disambiguation)
        /// </summary>
        public string[]? ParameterTypeNames { get; set; }

        /// <summary>
        /// Gets or sets the explicit interface type name
        /// </summary>
        public string? ExplicitInterfaceTypeName { get; set; }
    }
}