using MediatR;
using TodoApi.Abstractions.Dto.TodoItemDtos;

namespace TodoApi.BusinessLogic.TodoItem.Commands.UpdateTodoItems
{
    public class UpdateTodoItemCommand : UpdateTodoItemDto, IRequest<Unit>
    {
    }
}