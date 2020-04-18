using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Abstractions.Dto.TodoItemDtos;
using TodoApi.DataAccess;

namespace TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItems
{
    public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, IEnumerable<TodoItemDto>>
    {
        private readonly TodoApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTodoItemsQueryHandler(TodoApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(paramName: nameof(mapper));
        }

        public async Task<IEnumerable<TodoItemDto>> Handle(
            GetTodoItemsQuery request,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entities = await _dbContext.TodoItems
                .OrderBy(keySelector: x => x.Id)
                .ToArrayAsync(cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<TodoItemDto>>(source: entities);
        }
    }
}