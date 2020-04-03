using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using TodoApi.WebApi.Extensions;
using Environment = TodoApi.WebApi.Extensions.Environment;

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
        public static async Task<int> Main(string[] args)
        {
            var environment = Environment.GetEnvironment() ?? "Production";
            
            // Used to build key/value based configuration settings for use in an application.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddCommandLine(args: args)
                .Build();

            // Configuration object for creating <see cref="ILogger"/> instances.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration: configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            
            try
            {
                Log.Information(messageTemplate: "Starting web host...");
                await CreateHostBuilder(args: args).Build().RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(exception: ex, messageTemplate: "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Microsoft.Extensions.Hosting.HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>The <see cref="Microsoft.Extensions.Hosting.IHostBuilder"/> so that additional calls can be chained.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args: args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog();
                })
                .ConfigureWebHostDefaults(configure: webBuilder =>
                {
                    webBuilder.UseSerilog();
                    webBuilder.ConfigureKestrelHost();
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}