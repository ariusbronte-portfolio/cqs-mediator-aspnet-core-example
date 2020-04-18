using System;
using System.Linq;
using FluentAssertions;
using FluentValidation.TestHelper;
using TodoApi.Abstractions.Dto.TodoItemDtos;
using TodoApi.Abstractions.Enums;
using TodoApi.BusinessLogic.TodoItem.Commands.CreateTodoItem;
using TodoApi.Tests.Unit.Fixtures;
using Xunit;

namespace TodoApi.Tests.Unit.Validators
{
    public class CreateTodoItemValidatorTests : IClassFixture<ValidatorFixture<CreateTodoItemValidator>>
    {
        private readonly CreateTodoItemValidator _validator;

        public CreateTodoItemValidatorTests(ValidatorFixture<CreateTodoItemValidator> validator)
        {
            _validator = validator?.Instance ?? throw new ArgumentNullException(paramName: nameof(validator));
        }

        [Fact]
        public void IsValidTests()
        {
            _validator.TestValidate(objectToTest: new CreateTodoItemDto
            {
                Title = "TodoItemName", Status = TodoTaskStatus.Open
            }).IsValid.Should().BeTrue();

            _validator.TestValidate(objectToTest: new CreateTodoItemCommand
            {
                Title = "TodoItemName", Status = TodoTaskStatus.Deferred
            }).IsValid.Should().BeTrue();
            
            _validator.TestValidate(objectToTest: new CreateTodoItemCommand
            {
                Title = "TodoItemName", Status = TodoTaskStatus.Open
            }).IsValid.Should().BeTrue();
            
            _validator.TestValidate(objectToTest: new CreateTodoItemCommand
            {
                Title = "TodoItemName", Status = (TodoTaskStatus)1
            }).IsValid.Should().BeTrue();
        }

        [Fact]
        public void TitleIsNull()
        {
            // Arrange
            var dto = new CreateTodoItemDto
            {
                Title = null,
                Status = TodoTaskStatus.Open
            };
            
            // Act
            var result = _validator.TestValidate(objectToTest: dto);
            
            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Single().ErrorMessage.Should().Be(expected: "'Title' must not be empty.");
            result.Errors.Should().HaveCount(expected: 1);
        }

        [Theory]
        [InlineData(9, "'Title' must be between 10 and 256 characters. You entered 9 characters.")]
        [InlineData(257, "'Title' must be between 10 and 256 characters. You entered 257 characters.")]
        public void TitleRange(int length, string message)
        {
            // Arrange
            var dto = new CreateTodoItemDto
            {
                Title = new string(c: '*', count: length),
                Status = TodoTaskStatus.Open
            };
            
            // Act
            var result = _validator.TestValidate(objectToTest: dto);
            
            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(predicate: x => x.ErrorMessage == message).Should().NotBeNull();
            result.Errors.Should().HaveCount(expected: 1);
        }

        [Fact]
        public void InvalidEnum()
        {
            // Arrange
            var dto = new CreateTodoItemDto
            {
                Title = "TodoItemTitle",
                Status = (TodoTaskStatus) 99
            };
            
            // Act
            var result = _validator.TestValidate(objectToTest: dto);
            
            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}