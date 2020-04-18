using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoApi.Abstractions.Dto.TodoItemDtos;
using TodoApi.DataAccess;
using TodoApi.Domain.Entities;
using TodoApi.Implementations.Exceptions;

namespace TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItem
{
    public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemDto>
    {
        private readonly TodoApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTodoItemQueryHandler(TodoApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(paramName: nameof(mapper));
        }

        public async Task<TodoItemDto> Handle(
            GetTodoItemQuery request,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var id = request.Id;

            var entity = await _dbContext.TodoItems.FindAsync(id);

            if (entity == null)
            {
                throw new RecordNotFoundException(entity: nameof(TodoItemEntity), key: id);
            }

            return _mapper.Map<TodoItemDto>(source: entity);
        }
    }
}