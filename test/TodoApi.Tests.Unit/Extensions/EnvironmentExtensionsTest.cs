using FluentAssertions;
using Microsoft.Extensions.Hosting;
using TodoApi.WebApi.Extensions;
using Xunit;

namespace TodoApi.Tests.Unit.Extensions
{
    public class EnvironmentExtensionsTest
    {
        [Fact]
        public void IsDevelopment()
        {
            // Arrange
            SetEnvironmentVariable(Environments.Development);

            // Act
            var value = Environment.IsDevelopment();
            
            // Assert
            value.Should().BeTrue();
        }
        
        [Fact]
        public void IsProduction()
        {
            // Arrange
            SetEnvironmentVariable(Environments.Production);

            // Act
            var value = Environment.IsProduction();
            
            // Assert
            value.Should().BeTrue();
        }
        
        [Fact]
        public void IsStaging()
        {
            // Arrange
            SetEnvironmentVariable(Environments.Staging);

            // Act
            var value = Environment.IsStaging();
            
            // Assert
            value.Should().BeTrue();
        }

        private static void SetEnvironmentVariable(string value)
        {
            System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", value);
        }
    }
}