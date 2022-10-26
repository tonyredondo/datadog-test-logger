// <copyright file="ConnectionMultiplexerExecuteAsyncImplIntegration_2_6_45.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.ComponentModel;
using Vendor.Datadog.Trace.ClrProfiler.CallTarget;
using Vendor.Datadog.Trace.Configuration;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Redis.StackExchange
{
    /// <summary>
    /// StackExchange.Redis.ConnectionMultiplexer.ExecuteAsyncImpl calltarget instrumentation
    /// </summary>
    [InstrumentMethod(
        AssemblyName = "StackExchange.Redis",
        TypeName = "StackExchange.Redis.ConnectionMultiplexer",
        MethodName = "ExecuteAsyncImpl",
        ReturnTypeName = "System.Threading.Tasks.Task`1<T>",
        ParameterTypeNames = new[] { "StackExchange.Redis.Message", "StackExchange.Redis.ResultProcessor`1[!!0]", ClrNames.Object, "StackExchange.Redis.ServerEndPoint", "!!0" },
        MinimumVersion = "2.0.0", // 2.6.45, but dll uses 2.0.0
        MaximumVersion = "2.*.*",
        IntegrationName = StackExchangeRedisHelper.IntegrationName)]
    [InstrumentMethod(
        AssemblyName = "StackExchange.Redis.StrongName",
        TypeName = "StackExchange.Redis.ConnectionMultiplexer",
        MethodName = "ExecuteAsyncImpl",
        ReturnTypeName = "System.Threading.Tasks.Task`1<T>",
        ParameterTypeNames = new[] { "StackExchange.Redis.Message", "StackExchange.Redis.ResultProcessor`1[!!0]", ClrNames.Object, "StackExchange.Redis.ServerEndPoint", "!!0" },
        MinimumVersion = "2.0.0", // 2.6.45, but dll uses 2.0.0
        MaximumVersion = "2.*.*",
        IntegrationName = StackExchangeRedisHelper.IntegrationName)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    // ReSharper disable once InconsistentNaming
    public class ConnectionMultiplexerExecuteAsyncImplIntegration_2_6_45
    {
        internal static CallTargetState OnMethodBegin<TTarget, TMessage, TProcessor, TServerEndPoint, TDefaultValue>(TTarget instance, TMessage message, TProcessor resultProcessor, object state, TServerEndPoint serverEndPoint, TDefaultValue defaultValue)
            where TTarget : IConnectionMultiplexer
            where TMessage : IMessageData
        {
            string rawCommand = message.CommandAndKey ?? "COMMAND";
            StackExchangeRedisHelper.HostAndPort hostAndPort = StackExchangeRedisHelper.GetHostAndPort(instance.Configuration);

            Scope scope = RedisHelper.CreateScope(Tracer.Instance, StackExchangeRedisHelper.IntegrationId, StackExchangeRedisHelper.IntegrationName, hostAndPort.Host, hostAndPort.Port, rawCommand);
            if (scope is not null)
            {
                return new CallTargetState(scope);
            }

            return CallTargetState.GetDefault();
        }

        internal static TResponse OnAsyncMethodEnd<TTarget, TResponse>(TTarget instance, TResponse response, Exception exception, in CallTargetState state)
        {
            state.Scope.DisposeWithException(exception);
            return response;
        }
    }
}
