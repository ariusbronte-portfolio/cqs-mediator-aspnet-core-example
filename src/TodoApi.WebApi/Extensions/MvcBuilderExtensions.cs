using System;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.BusinessLogic.TodoItem.Commands.CreateTodoItem;

namespace TodoApi.WebApi.Extensions
{
    /// <summary>
    ///     Extension methods for setting up services in an <see cref="Microsoft.Extensions.DependencyInjection.IMvcBuilder" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMethodReturnValue.Global")]
    public static class MvcBuilderExtensions
    {
        /// <summary>
        ///     Adds Fluent Validation services to the specified <see cref="T:Microsoft.Extensions.DependencyInjection.IMvcBuilder" />.
        /// </summary>
        /// <param name="builder">An interface for configuring MVC services.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    Thrown if <see cref="Microsoft.Extensions.DependencyInjection.IMvcBuilder"/> is nullable
        /// </exception>
        /// <returns>
        ///     An <see cref="T:Microsoft.Extensions.DependencyInjection.IMvcBuilder" /> that can be used to further configure the MVC services.
        /// </returns>
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(paramName: nameof(builder));

            builder.AddFluentValidation(configurationExpression: fv =>
            {
                fv.ImplicitlyValidateChildProperties = false;
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                fv.RegisterValidatorsFromAssemblyContaining<CreateTodoItemValidator>();
            });

            return builder;
        }
    }
}