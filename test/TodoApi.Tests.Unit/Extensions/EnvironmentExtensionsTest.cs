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
            SetEnvironmentVariable(value: Environments.Development);

            // Act
            var value = Environment.IsDevelopment();
            
            // Assert
            value.Should().BeTrue();
        }
        
        [Fact]
        public void IsProduction()
        {
            // Arrange
            SetEnvironmentVariable(value: Environments.Production);

            // Act
            var value = Environment.IsProduction();
            
            // Assert
            value.Should().BeTrue();
        }
        
        [Fact]
        public void IsStaging()
        {
            // Arrange
            SetEnvironmentVariable(value: Environments.Staging);

            // Act
            var value = Environment.IsStaging();
            
            // Assert
            value.Should().BeTrue();
        }

        private static void SetEnvironmentVariable(string value)
        {
            System.Environment.SetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT", value: value);
        }
    }
}