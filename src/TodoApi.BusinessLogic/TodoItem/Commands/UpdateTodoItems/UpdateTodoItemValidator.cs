using FluentValidation;
using TodoApi.Abstractions.Dto.TodoItemDtos;

namespace TodoApi.BusinessLogic.TodoItem.Commands.UpdateTodoItems
{
    /// <summary>
    ///     Validator for <see cref="TodoApi.BusinessLogic.TodoItem.Commands.UpdateTodoItems.UpdateTodoItemCommand"/>.
    /// </summary>
    public class UpdateTodoItemValidator : AbstractValidator<UpdateTodoItemDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateTodoItemValidator"/> class.
        /// </summary>
        public UpdateTodoItemValidator()
        {
            RuleFor(expression: x => x.Id)
                .NotEmpty()
                .GreaterThan(valueToCompare: 0);

            RuleFor(expression: x => x.Title)
                .NotEmpty()
                .Length(min: 10, max: 256);

            RuleFor(expression: x => x.Status)
                .IsInEnum();
        }
    }
}