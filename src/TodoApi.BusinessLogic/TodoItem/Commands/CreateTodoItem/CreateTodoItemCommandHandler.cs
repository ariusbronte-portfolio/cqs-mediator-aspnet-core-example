using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoApi.Abstractions.Dto.TodoItemDtos;
using TodoApi.DataAccess;
using TodoApi.Domain.Entities;

namespace TodoApi.BusinessLogic.TodoItem.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemDto>
    {
        private readonly TodoApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateTodoItemCommandHandler(TodoApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(paramName: nameof(mapper));
        }

        public async Task<TodoItemDto> Handle(
            CreateTodoItemCommand request,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var title = request.Title;
            var status = request.Status;

            var entity = new TodoItemEntity(title: title, status: status);
            await _dbContext.TodoItems.AddAsync(entity: entity, cancellationToken: cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return _mapper.Map<TodoItemDto>(source: entity);
        }
    }
}