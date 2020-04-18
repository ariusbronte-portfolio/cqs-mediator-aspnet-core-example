using System;
using Microsoft.Extensions.Hosting;

namespace TodoApi.WebApi.Extensions
{
    /// <summary>
    ///     Extension methods for setting up services in an <see cref="System.Environment" />.
    /// </summary>
    public static partial class Environment
    {
        /// <summary>
        ///     Checks if the current host environment name is <see cref="Microsoft.Extensions.Hosting.EnvironmentName.Development"/>.
        /// </summary>
        /// <returns>True if the environment name is <see cref="Microsoft.Extensions.Hosting.EnvironmentName.Development"/>, otherwise false.</returns>
        public static bool IsDevelopment()
        {
            return IsEnvironment(environmentName: Environments.Development);
        }

        /// <summary>
        ///     Checks if the current host environment name is <see cref="Microsoft.Extensions.Hosting.EnvironmentName.Staging"/>.
        /// </summary>
        /// <returns>True if the environment name is <see cref="Microsoft.Extensions.Hosting.EnvironmentName.Staging"/>, otherwise false.</returns>
        public static bool IsStaging()
        {
            return IsEnvironment(environmentName: Environments.Staging);
        }

        /// <summary>
        ///     Checks if the current host environment name is <see cref="Microsoft.Extensions.Hosting.EnvironmentName.Production"/>.
        /// </summary>
        /// <returns>True if the environment name is <see cref="Microsoft.Extensions.Hosting.EnvironmentName.Production"/>, otherwise false.</returns>
        public static bool IsProduction()
        {
            return IsEnvironment(environmentName: Environments.Production);
        }

        /// <summary>
        ///     Compares the current host environment name against the specified value.
        /// </summary>
        /// <param name="environmentName">Environment name to validate against.</param>
        /// <returns>True if the specified name is the same as the current environment, otherwise false.</returns>
        private static bool IsEnvironment(string environmentName)
        {
            return string.Equals(a: GetEnvironment(), b: environmentName, comparisonType: StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    ///     Extension methods for setting up services in an <see cref="System.Environment" />.
    /// </summary>
    public static partial class Environment
    {
        /// <summary>
        ///     Retrieves the value of an ASPNETCORE_ENVIRONMENT variable from the current process
        /// </summary>
        public static string GetEnvironment()
        {
            return System.Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT");
        }
    }
}