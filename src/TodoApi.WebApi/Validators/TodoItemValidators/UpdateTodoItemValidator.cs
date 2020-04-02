using FluentValidation;
using TodoApi.BusinessLogic.TodoItem.Commands.UpdateTodoItems;

namespace TodoApi.WebApi.Validators.TodoItemValidators
{
    /// <summary>
    ///     Validator for <see cref="UpdateTodoItemCommand"/>.
    /// </summary>
    public class UpdateTodoItemValidator : AbstractValidator<UpdateTodoItemCommand>
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