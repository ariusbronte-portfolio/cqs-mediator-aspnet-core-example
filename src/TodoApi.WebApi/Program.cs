using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TodoApi.WebApi
{
    /// <summary>
    ///     Entry point class of programme.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Entry method of programme.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args: args).Build().RunAsync();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Microsoft.Extensions.Hosting.HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args: args)
                .ConfigureWebHostDefaults(configure: webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
