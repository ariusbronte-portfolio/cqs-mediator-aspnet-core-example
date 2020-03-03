using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TodoApi.WebApi.Extensions
{
    /// <summary>
    ///     Extension methods for setting up services in an <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds Swagger services to the specified <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    The services must not be null.
        /// </exception>
        /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(paramName: nameof(services));

            services.AddSwaggerGen(setupAction: c =>
            {
                c.SwaggerDoc(name: "v1", info: new OpenApiInfo
                {
                    Title = "TodoApi",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(path1: AppContext.BaseDirectory, path2: xmlFile);
                c.IncludeXmlComments(filePath: xmlPath);
            });

            return services;
        }
    }
}