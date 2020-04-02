using MediatR;

namespace TodoApi.BusinessLogic.TodoItem.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest<Unit>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoApi.BusinessLogic.TodoItem.Commands.DeleteTodoItem.DeleteTodoItemCommand"/> class.
        /// </summary>
        /// <param name="id">The primary key for this task.</param>
        public DeleteTodoItemCommand(long id)
        {
            Id = id;
        }

        /// <inheritdoc cref="TodoApi.Domain.Entities.TodoItemEntity.Id"/>
        public long Id { get; }
    }
}