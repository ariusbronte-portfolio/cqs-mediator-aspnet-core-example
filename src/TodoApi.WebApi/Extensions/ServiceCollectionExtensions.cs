using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Reflection;
using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TodoApi.Abstractions.Dto.TodoItemDtos;
using TodoApi.Domain.Entities;
using TodoApi.Implementations.Exceptions;

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

        /// <summary>
        ///     Adds HellangProblemDetails services to the specified <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
        /// <param name="environment">Provides information about the web hosting environment an application is running in.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    The services must not be null.
        /// </exception>
        /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddHellangProblemDetails(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (services == null) throw new ArgumentNullException(paramName: nameof(services));
            if (environment == null) throw new ArgumentNullException(paramName: nameof(environment));

            services.AddProblemDetails(configure: options =>
            {
                // This is the default behavior; only include exception details in a development environment.
                options.IncludeExceptionDetails = ctx => environment.IsDevelopment();

                // This will map NotImplementedException to the 404 Not Found status code.
                options.Map<RecordNotFoundException>(mapping: ex => 
                    new ExceptionProblemDetails(error: ex, statusCode: StatusCodes.Status404NotFound));

                // This will map NotImplementedException to the 501 Not Implemented status code.
                options.Map<NotImplementedException>(mapping: ex =>
                    new ExceptionProblemDetails(error: ex, statusCode: StatusCodes.Status501NotImplemented));

                // This will map HttpRequestException to the 503 Service Unavailable status code.
                options.Map<HttpRequestException>(mapping: ex =>
                    new ExceptionProblemDetails(error: ex, statusCode: StatusCodes.Status503ServiceUnavailable));

                // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
                // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
                options.Map<Exception>(mapping: ex =>
                    new ExceptionProblemDetails(error: ex, statusCode: StatusCodes.Status500InternalServerError));
            });

            return services;
        }
        
        /// <summary>
        ///     Scan classes and register the configuration, mapping, and extensions with the service collection
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if <see cref="IServiceCollection"/> is nullable
        /// </exception>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(paramName: nameof(services));

            services.AddAutoMapper(configAction: cfg =>
            {
                cfg.CreateMap<TodoItemEntity, TodoItemDto>();
            }, typeof(Startup));

            return services;
        }
    }
}