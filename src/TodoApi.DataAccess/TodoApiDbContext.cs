using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.DataAccess
{
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
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

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(modelBuilder: builder);
        }
        
        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(optionsBuilder: builder);
        }
    }
}