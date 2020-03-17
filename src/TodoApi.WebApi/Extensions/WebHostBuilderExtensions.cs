using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace TodoApi.WebApi.Extensions
{
    /// <summary>
    ///     Extension methods for setting up services in an <see cref="TodoApi.WebApi.Extensions.WebHostBuilderExtensions" />.
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        ///     Configures Kestrel options but does not register an IServer.
        /// </summary>
        /// <param name="hostBuilder">
        ///     The <see cref="Microsoft.AspNetCore.Hosting.IWebHostBuilder" /> to configure.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///    Thrown if hostBuilder is null.
        /// </exception>
        /// <returns>
        ///     An <see cref="Microsoft.AspNetCore.Hosting.IWebHostBuilder" />
        ///     that can be used to further configure the WebHost services.
        /// </returns>
        public static IWebHostBuilder ConfigureKestrelHost(this IWebHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(paramName: nameof(hostBuilder));
            }

            hostBuilder.ConfigureKestrel(options: serverOptions =>
            {
                serverOptions.Limits.MaxConcurrentConnections = 100;
                serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                serverOptions.Limits.MinRequestBodyDataRate =
                    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(value: 10));
                serverOptions.Limits.MinResponseDataRate =
                    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(value: 10));
                serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(value: 2);
                serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(value: 1);
            });

            return hostBuilder;
        }
    }
}