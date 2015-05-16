﻿// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Net;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Splunk;
using Splunk.Client;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.SplunkViaHttp() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationSplunkExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via http.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="context">The Splunk context to log to</param>
        /// <param name="batchInterval"></param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="batchSizeLimit">The size of the batch prior to writing</param>
        /// <param name="renderTemplate">If true, the message template will be rendered</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        /// <remarks>TODO: Add link to splunk configuration and wiki</remarks>
        public static LoggerConfiguration SplunkViaHttp(
            this LoggerSinkConfiguration loggerConfiguration,
            SplunkContext context,
            int batchSizeLimit,
            TimeSpan batchInterval,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
             bool renderTemplate = true)
        {
            var sink = new SplunkViaHttpSink(context, batchSizeLimit, batchInterval, formatProvider, renderTemplate);

            return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via http.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="context">The Splunk context to log to</param>
        /// <param name="password">The password of the Splunk user</param>
        /// <param name="resourceNameSpace"></param>
        /// <param name="transmitterArgs"></param>
        /// <param name="batchSizeLimit">The size of the batch prior to logging</param>
        /// <param name="batchInterval">The interval on which to log via http</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="index">The name of the Splunk index</param>
        /// <param name="userName">The name of the Splunk user</param>
        /// <param name="renderTemplate">If ture, the message template is rendered</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        /// <remarks>TODO: Add link to splunk configuration and wiki</remarks>
        public static LoggerConfiguration SplunkViaHttp(
            this LoggerSinkConfiguration loggerConfiguration,
            Splunk.Client.Context context,
            string index,
            string userName,
            string password,
            int batchSizeLimit,
            TimeSpan batchInterval,
            Namespace resourceNameSpace,
            TransmitterArgs transmitterArgs,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
             bool renderTemplate = true)
        {
            var sink = new SplunkViaHttpSink(new SplunkContext(context, index, userName, password, resourceNameSpace, transmitterArgs), batchSizeLimit,batchInterval, formatProvider);

            return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via http.
        /// </summary>
        /// <param name="loggerConfiguration">The logger config</param>
        /// <param name="host">The Splunk host that is configured for UDP logging</param>
        /// <param name="port">The UDP port</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="renderTemplate">If ture, the message template will be rendered</param>
        /// <returns></returns>
        /// <remarks>TODO: Add link to splunk configuration and wiki</remarks>
        public static LoggerConfiguration SplunkViaUdp(
            this LoggerSinkConfiguration loggerConfiguration,
            string host,
            int port,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
            bool renderTemplate = true)
        {
            var sink = new SplunkViaUdpSink(host, port, formatProvider, renderTemplate);

            return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
        }


        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via UDP.
        /// </summary>
        /// <param name="loggerConfiguration">The logger config</param>
        /// <param name="hostAddresss">The Splunk host that is configured for UDP logging</param>
        /// <param name="port">The UDP port</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="renderTemplate">If ture, the message template is rendered</param>
        /// <returns>The logger configuration</returns>
        /// <remarks>TODO: Add link to splunk configuration and wiki</remarks>
        public static LoggerConfiguration SplunkViaUdp(
            this LoggerSinkConfiguration loggerConfiguration,
            IPAddress hostAddresss,
            int port,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
            bool renderTemplate = true)
        {
            var sink = new SplunkViaUdpSink(hostAddresss, port, formatProvider, renderTemplate);

            return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via TCP.
        /// </summary>
        /// <param name="loggerConfiguration">The logger config</param>
        /// <param name="hostAddresss">The Splunk host that is configured for UDP logging</param>
        /// <param name="port">The TCP port</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="renderTemplate">If true, the message template is rendered</param>
        /// <returns></returns>
        /// <remarks>TODO: Add link to splunk configuration and wiki</remarks>
        public static LoggerConfiguration SplunkViaTcp(
            this LoggerSinkConfiguration loggerConfiguration,
            IPAddress hostAddresss,
            int port,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
             bool renderTemplate = true)
        {
            var sink = new SplunkViaTcpSink(hostAddresss, port, formatProvider, renderTemplate);

            return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via TCP.
        /// </summary>
        /// <param name="loggerConfiguration">The logger config</param>
        /// <param name="host">The Splunk host that is configured for UDP logging</param>
        /// <param name="port">The TCP port</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="renderTemplate">If ture, the message template is rendered</param>
        /// <returns></returns>
        /// <remarks>TODO: Add link to splunk configuration and wiki</remarks>
        public static LoggerConfiguration SplunkViaTcp(
            this LoggerSinkConfiguration loggerConfiguration,
            string host,
            int port,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
             bool renderTemplate = true)
        {
            var sink = new SplunkViaTcpSink(host, port, formatProvider, renderTemplate);

            return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

    }
}