using MediatR;
using TodoApi.Abstractions.Dto.TodoItemDtos;

namespace TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItem
{
    public class GetTodoItemQuery : IRequest<TodoItemDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItem.GetTodoItemQuery"/> class.
        /// </summary>
        /// <param name="id">The primary key for this task.</param>
        public GetTodoItemQuery(long id)
        {
            Id = id;
        }

        /// <inheritdoc cref="TodoApi.Domain.Entities.TodoItemEntity.Id"/>
        public long Id { get; }
    }
}