using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace TodoApi.Tests.Unit.AssertExtensions
{
    /// <summary>
    ///     Extension methods for <see cref="FluentAssertions.Primitives.ObjectAssertions"/>.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMethodReturnValue.Global")]
    public static class ObjectAssertionsExtensions
    {
        /// <summary>
        ///     Assert that the object is an <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute"/> with the specified status code.
        /// </summary>
        public static AndConstraint<NumericAssertions<int>> BeOfProducesResponseTypeAttribute(this ObjectAssertions assert, int statusCode)
        {
            return assert.IsType<ProducesResponseTypeAttribute>().StatusCode.Should().Be(statusCode);
        }
        
        /// <summary>
        ///     Assert that the object is an <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/> with the specified template.
        /// </summary>
        public static AndConstraint<StringAssertions> WithRouteTemplate(this ObjectAssertions assert, string template)
        {
            return assert.IsType<RouteAttribute>().Template.Should().Be(template);
        }
        
        /// <summary>
        ///     Assert that the object is an <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/> with the specified mediaTypeName.
        /// </summary>
        public static AndConstraint<StringAssertions> BeOfProducesAttribute(this ObjectAssertions assert, string mediaTypeName)
        {
            return assert.IsType<ProducesAttribute>().ContentTypes.Single().Should().Be(mediaTypeName);
        }

        /// <summary>
        ///     Assert that the object is inherit of <see cref="Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute"/> with the specified httpMethod.
        /// </summary>
        public static AndConstraint<StringAssertions> BeOfHttpAttribute<T>(this ObjectAssertions assert, string httpMethod) where T : HttpMethodAttribute
        {
            return assert.IsType<T>().HttpMethods.Single().Should().Be(httpMethod);
        }

        /// <summary>
        ///     Assert that the object is inherit of <see cref="Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute"/> with the specified template.
        /// </summary>
        public static AndConstraint<StringAssertions> WithHttpTemplate<T>(this ObjectAssertions assert, string template) where T : HttpMethodAttribute
        {
            return assert.IsType<T>().Template.Should().Be(template);
        }

        /// <summary>
        ///     Safe cast object to {T}.
        /// </summary>
        public static T IsType<T>(this ObjectAssertions assert)
        {
            return assert.BeOfType<T>().And.Subject.As<T>();
        }
    }
}