using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TodoApi.DataAccess.Configurations;
using TodoApi.Domain.Entities;

namespace TodoApi.DataAccess
{
    /// <inheritdoc />
    [SuppressMessage(category: "ReSharper", checkId: "SuggestBaseTypeForParameter")]
    public sealed class TodoApiDbContext : DbContext
    {
        /// <inheritdoc />
        public TodoApiDbContext()
        {
        }

        /// <inheritdoc />
        public TodoApiDbContext(DbContextOptions<TodoApiDbContext> options) : base(options: options)
        {
        }

        /// <exception cref="TodoApi.Domain.Entities.TodoItemEntity" />
        public DbSet<TodoItemEntity> TodoItems { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(modelBuilder: builder);
            builder.ApplyConfiguration(configuration: new TodoItemEntityConfiguration());
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(optionsBuilder: builder);
        }
    }
}