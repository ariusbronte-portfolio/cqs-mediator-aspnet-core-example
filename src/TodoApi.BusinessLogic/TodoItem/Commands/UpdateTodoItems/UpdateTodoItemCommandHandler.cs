using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.DataAccess;
using TodoApi.Domain.Entities;
using TodoApi.Implementations.Exceptions;

namespace TodoApi.BusinessLogic.TodoItem.Commands.UpdateTodoItems
{
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, Unit>
    {
        private readonly TodoApiDbContext _dbContext;

        public UpdateTodoItemCommandHandler(TodoApiDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
        }

        public async Task<Unit> Handle(
            UpdateTodoItemCommand request,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var id = request.Id;
            var title = request.Title;
            var status = request.Status;

            var entity = await _dbContext.TodoItems.FindAsync(id);

            if (entity == null)
            {
                throw new RecordNotFoundException(entity: nameof(TodoItemEntity), key: id);
            }

            entity.SetTitle(title: title);
            entity.SetStatus(status: status);

            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}