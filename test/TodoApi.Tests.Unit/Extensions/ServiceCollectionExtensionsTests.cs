using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.DataAccess;
using TodoApi.DataAccess.Extensions;
using TodoApi.WebApi.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace TodoApi.Tests.Unit.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ServiceCollectionExtensionsTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AddDbContext()
        {
            // Arrange
            const string connectionString = "connectionString";
            const string migrationsAssembly = "migrationsAssembly";
            
            // Act
            var services = new ServiceCollection()
                .AddDbContext(connectionString, migrationsAssembly);

            var dbContext = services.BuildServiceProvider().GetService<TodoApiDbContext>();
            
            // Assert
            services.Should().NotBeEmpty();
            dbContext.Should().NotBeNull();
            
            LogServices(services);
        }

        [Fact]
        public void AddSwaggerGenerator()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddSwaggerGenerator();
            
            // Assert
            services.Should().NotBeEmpty();
            
            LogServices(services);
        }

        [Fact]
        public void AddHellangProblemDetails()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddHellangProblemDetails();
            
            // Assert
            services.Should().NotBeEmpty();
            
            LogServices(services);
        }

        [Fact]
        public void AddAutoMapper()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddAutoMapper();
            
            // Assert
            services.Should().NotBeEmpty();
            
            LogServices(services);
        }
        
        private void LogServices(IServiceCollection services)
        {
            _testOutputHelper.WriteLine($"Total Services Registered: {services.Count}");
            foreach (var service in services)
            {
                _testOutputHelper.WriteLine($"\n Service: {service.ServiceType.FullName}\n Lifetime: {service.Lifetime}\n Instance: {service.ImplementationType?.FullName}");
            }
        }
    }
}