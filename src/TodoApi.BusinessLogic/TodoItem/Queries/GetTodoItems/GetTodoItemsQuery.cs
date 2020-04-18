using System.Collections.Generic;
using MediatR;
using TodoApi.Abstractions.Dto.TodoItemDtos;

namespace TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItems
{
    public class GetTodoItemsQuery : IRequest<IEnumerable<TodoItemDto>>
    {
    }
}