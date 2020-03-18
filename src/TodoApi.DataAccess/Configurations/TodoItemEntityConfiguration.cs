using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApi.Domain.Entities;

namespace TodoApi.DataAccess.Configurations
{
    /// <inheritdoc />
    public class TodoItemEntityConfiguration : IEntityTypeConfiguration<TodoItemEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<TodoItemEntity> builder)
        {
            builder.HasKey(keyExpression: x => x.Id);

            builder.HasIndex(indexExpression: x => x.Title)
                .IsUnique();

            builder.Property(propertyExpression: x => x.Title)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(maxLength: 256);

            builder.Property(propertyExpression: x => x.Status)
                .IsRequired();

            builder.Property(propertyExpression: x => x.CreationHistory)
                .IsRequired();
        }
    }
}