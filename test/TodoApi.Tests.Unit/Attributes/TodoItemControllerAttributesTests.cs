using System;
using System.Net;
using System.Net.Mime;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Tests.Unit.AssertExtensions;
using TodoApi.Tests.Unit.Fixtures;
using TodoApi.WebApi.Controllers;
using Xunit;

namespace TodoApi.Tests.Unit.Attributes
{
    public class TodoItemControllerAttributesTests : IClassFixture<AttributeFixture<TodoItemController>>
    {
        private readonly AttributeFixture<TodoItemController> _attribute;

        public TodoItemControllerAttributesTests(AttributeFixture<TodoItemController> attribute)
        {
            _attribute = attribute ?? throw new ArgumentNullException(paramName: nameof(attribute));
        }

        [Fact]
        public void Controller()
        {
            var customAttributes = Attribute.GetCustomAttributes(element: typeof(TodoItemController));
            var attributes = _attribute.GetTCustomAttributes();

            attributes[index0: 0].Should().BeOfType<ApiControllerAttribute>();
            
            attributes[index0: 1].Should().BeOfType<RouteAttribute>();
            attributes[index0: 1].Should().WithRouteTemplate(template: "[controller]");
            
            attributes[index0: 2].Should().BeOfProducesAttribute(mediaTypeName: MediaTypeNames.Application.Json);
            attributes[index0: 3].Should().BeOfType<ControllerAttribute>();
        }
        
        [Fact]
        public void GetTodoItems()
        {
            _attribute.GetMethod(method: nameof(TodoItemController.GetTodoItems));

            var attributes = _attribute.GetCustomAttributes();

            attributes[index0: 2].Should().BeOfHttpAttribute<HttpGetAttribute>(httpMethod: WebRequestMethods.Http.Get);
            attributes[index0: 3].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status200OK);
        }
        
        [Fact]
        public void GetTodoItem()
        {
            _attribute.GetMethod(method: nameof(TodoItemController.GetTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[index0: 2].Should().BeOfHttpAttribute<HttpGetAttribute>(httpMethod: WebRequestMethods.Http.Get);
            attributes[index0: 2].Should().WithHttpTemplate<HttpGetAttribute>(template: "{id}");

            attributes[index0: 3].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status200OK);
            attributes[index0: 4].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status400BadRequest);
            attributes[index0: 5].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status404NotFound);
        }
        
        [Fact]
        public void CreateTodoItem()
        {
            _attribute.GetMethod(method: nameof(TodoItemController.CreateTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[index0: 2].Should().BeOfHttpAttribute<HttpPostAttribute>(httpMethod: WebRequestMethods.Http.Post);

            attributes[index0: 3].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status201Created);
            attributes[index0: 4].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public void UpdateTodoItem()
        {
            _attribute.GetMethod(method: nameof(TodoItemController.UpdateTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[index0: 2].Should().BeOfHttpAttribute<HttpPutAttribute>(httpMethod: WebRequestMethods.Http.Put);

            attributes[index0: 3].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status204NoContent);
            attributes[index0: 4].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status400BadRequest);
            attributes[index0: 5].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status404NotFound);
        }

        [Fact]
        public void DeleteTodoItem()
        {
            _attribute.GetMethod(method: nameof(TodoItemController.DeleteTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[index0: 2].Should().BeOfHttpAttribute<HttpDeleteAttribute>(httpMethod: "DELETE");
            attributes[index0: 2].Should().WithHttpTemplate<HttpDeleteAttribute>(template: "{id}");

            attributes[index0: 3].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status204NoContent);
            attributes[index0: 4].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status400BadRequest);
            attributes[index0: 5].Should().BeOfProducesResponseTypeAttribute(statusCode: StatusCodes.Status404NotFound);
        }
    }
}