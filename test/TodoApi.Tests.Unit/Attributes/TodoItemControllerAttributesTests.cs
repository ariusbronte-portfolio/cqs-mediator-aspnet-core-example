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
            _attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        [Fact]
        public void Controller()
        {
            var customAttributes = Attribute.GetCustomAttributes(typeof(TodoItemController));
            var attributes = _attribute.GetTCustomAttributes();

            attributes[0].Should().BeOfType<ApiControllerAttribute>();
            
            attributes[1].Should().BeOfType<RouteAttribute>();
            attributes[1].Should().WithRouteTemplate("[controller]");
            
            attributes[2].Should().BeOfProducesAttribute(MediaTypeNames.Application.Json);
            attributes[3].Should().BeOfType<ControllerAttribute>();
        }
        
        [Fact]
        public void GetTodoItems()
        {
            _attribute.GetMethod(nameof(TodoItemController.GetTodoItems));

            var attributes = _attribute.GetCustomAttributes();

            attributes[2].Should().BeOfHttpAttribute<HttpGetAttribute>(WebRequestMethods.Http.Get);
            attributes[3].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status200OK);
        }
        
        [Fact]
        public void GetTodoItem()
        {
            _attribute.GetMethod(nameof(TodoItemController.GetTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[2].Should().BeOfHttpAttribute<HttpGetAttribute>(WebRequestMethods.Http.Get);
            attributes[2].Should().WithHttpTemplate<HttpGetAttribute>("{id}");

            attributes[3].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status200OK);
            attributes[4].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status400BadRequest);
            attributes[5].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status404NotFound);
        }
        
        [Fact]
        public void CreateTodoItem()
        {
            _attribute.GetMethod(nameof(TodoItemController.CreateTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[2].Should().BeOfHttpAttribute<HttpPostAttribute>(WebRequestMethods.Http.Post);

            attributes[3].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status201Created);
            attributes[4].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public void UpdateTodoItem()
        {
            _attribute.GetMethod(nameof(TodoItemController.UpdateTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[2].Should().BeOfHttpAttribute<HttpPutAttribute>(WebRequestMethods.Http.Put);

            attributes[3].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status204NoContent);
            attributes[4].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status400BadRequest);
            attributes[5].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void DeleteTodoItem()
        {
            _attribute.GetMethod(nameof(TodoItemController.DeleteTodoItem));

            var attributes = _attribute.GetCustomAttributes();

            attributes[2].Should().BeOfHttpAttribute<HttpDeleteAttribute>("DELETE");
            attributes[2].Should().WithHttpTemplate<HttpDeleteAttribute>("{id}");

            attributes[3].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status204NoContent);
            attributes[4].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status400BadRequest);
            attributes[5].Should().BeOfProducesResponseTypeAttribute(StatusCodes.Status404NotFound);
        }
    }
}