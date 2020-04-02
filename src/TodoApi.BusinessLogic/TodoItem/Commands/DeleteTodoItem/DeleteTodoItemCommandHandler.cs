using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.DataAccess;
using TodoApi.Domain.Entities;
using TodoApi.Implementations.Exceptions;

namespace TodoApi.BusinessLogic.TodoItem.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, Unit>
    {
        private readonly TodoApiDbContext _dbContext;

        public DeleteTodoItemCommandHandler(TodoApiDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
        }

        public async Task<Unit> Handle(
            DeleteTodoItemCommand request,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var id = request.Id;

            var entity = await _dbContext.TodoItems.FindAsync(id);

            if (entity == null)
            {
                throw new RecordNotFoundException(entity: nameof(TodoItemEntity), key: id);
            }

            _dbContext.TodoItems.Remove(entity: entity);
            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}