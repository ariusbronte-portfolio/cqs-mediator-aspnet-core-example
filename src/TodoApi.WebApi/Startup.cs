using System;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItems;
using TodoApi.DataAccess;
using TodoApi.DataAccess.Extensions;
using TodoApi.WebApi.Extensions;

namespace TodoApi.WebApi
{
    /// <summary>
    ///      Configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        /// <inheritdoc cref="Microsoft.Extensions.Configuration.IConfiguration"/>
        private readonly IConfiguration _configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoApi.WebApi.Startup"/> class.
        /// </summary>
        /// <param name="configuration">
        ///     Represents a set of key/value application configuration properties.
        /// </param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(paramName: nameof(configuration));
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Defines a contract for a collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register database
            var connectionString = _configuration.GetConnectionString(name: "Default");
            var migrationsAssembly = typeof(TodoApiDbContext).Assembly.FullName;
            services.AddDbContext(connectionString: connectionString, migrationsAssembly: migrationsAssembly);

            services.AddAutoMapper();
            services.AddMediatR(typeof(GetTodoItemsQuery));

            // Register the Swagger generator
            services.AddSwaggerGenerator();
            services.AddHellangProblemDetails();
            services.AddControllers().AddFluentValidation();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">
        ///     Provides the mechanisms to configure an application's request pipeline.
        /// </param>
        /// <param name="env">
        ///     Provides information about the web hosting environment an application is running in.
        /// </param>
        /// <param name="logger">
        ///     Represents a type used to perform logging.
        /// </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogDebug(message: "Configure the HTTP request pipelines in development mode.");
                app.UseDeveloperExceptionPage();

                // Middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(setupAction: c =>
                {
                    c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "TodoApiV1");
                });
            }

            app.UseCors(configurePolicy: builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseForwardedHeaders(options: new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseProblemDetails();
            app.UseRouting();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            
            // Write streamlined request completion events, instead of the more verbose ones from the framework.
            // To use the default framework request logging instead, remove this line and set the "Microsoft"
            // level in appsettings.json to "Information".
            app.UseSerilogRequestLogging();
            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}