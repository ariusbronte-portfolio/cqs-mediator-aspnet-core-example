using FluentValidation;
using TodoApi.Abstractions.Dto.TodoItemDtos;

namespace TodoApi.BusinessLogic.TodoItem.Commands.CreateTodoItem
{
    /// <summary>
    ///     Validator for <see cref="TodoApi.BusinessLogic.TodoItem.Commands.CreateTodoItem.CreateTodoItemCommand"/>.
    /// </summary>
    public class CreateTodoItemValidator : AbstractValidator<CreateTodoItemDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateTodoItemValidator"/> class.
        /// </summary>
        public CreateTodoItemValidator()
        {
            RuleFor(expression: x => x.Title)
                .NotEmpty()
                .Length(min: 10, max: 256);

            RuleFor(expression: x => x.Status)
                .IsInEnum();
        }
    }
}