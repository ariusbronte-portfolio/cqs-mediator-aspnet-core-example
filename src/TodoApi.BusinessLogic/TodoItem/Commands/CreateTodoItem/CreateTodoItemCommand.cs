using MediatR;
using TodoApi.Abstractions.Dto.TodoItemDtos;

namespace TodoApi.BusinessLogic.TodoItem.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : CreateTodoItemDto, IRequest<TodoItemDto>
    {
        
    }
}