using System;
using System.Linq;
using FluentAssertions;
using FluentValidation.TestHelper;
using TodoApi.Abstractions.Dto.TodoItemDtos;
using TodoApi.Abstractions.Enums;
using TodoApi.BusinessLogic.TodoItem.Commands.UpdateTodoItems;
using TodoApi.Tests.Unit.Fixtures;
using Xunit;

namespace TodoApi.Tests.Unit.Validators
{
    public class UpdateTodoItemValidatorTests : IClassFixture<ValidatorFixture<UpdateTodoItemValidator>>
    {
        private readonly UpdateTodoItemValidator _validator;

        public UpdateTodoItemValidatorTests(ValidatorFixture<UpdateTodoItemValidator> validator)
        {
            _validator = validator?.Instance ?? throw new ArgumentNullException(nameof(validator));
        }

        [Fact]
        public void IsValidTests()
        {
            _validator.TestValidate(new UpdateTodoItemDto
            {
                Id = 1, Title = "TodoItemName", Status = TodoTaskStatus.Open
            }).IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null, "'Id' must not be empty.")]
        [InlineData(0, "'Id' must be greater than '0'.")]
        public void IdIsNull(long id, string message)
        {
            // Arrange
            var dto = new UpdateTodoItemDto
            {
                Id = id,
                Title = "TodoItemName",
                Status = TodoTaskStatus.Open
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.ErrorMessage == message).Should().NotBeNull();
        }

        [Fact]
        public void TitleIsNull()
        {
            // Arrange
            var dto = new UpdateTodoItemDto
            {
                Id = 1,
                Title = null,
                Status = TodoTaskStatus.Open
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Single().ErrorMessage.Should().Be("'Title' must not be empty.");
            result.Errors.Should().HaveCount(1);
        }

        [Theory]
        [InlineData(9, "'Title' must be between 10 and 256 characters. You entered 9 characters.")]
        [InlineData(257, "'Title' must be between 10 and 256 characters. You entered 257 characters.")]
        public void TitleRange(int length, string message)
        {
            // Arrange
            var dto = new UpdateTodoItemDto
            {
                Id = 1,
                Title = new string('*', length),
                Status = TodoTaskStatus.Open
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.ErrorMessage == message).Should().NotBeNull();
            result.Errors.Should().HaveCount(1);
        }

        [Fact]
        public void InvalidEnum()
        {
            // Arrange
            var dto = new UpdateTodoItemDto
            {
                Title = "TodoItemTitle",
                Status = (TodoTaskStatus) 99
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}