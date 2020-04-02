using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TodoApi.Abstractions.Enums;

namespace TodoApi.Domain.Entities
{
    /// <summary>
    ///     Presents the essence of the tasks.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    [SuppressMessage(category: "ReSharper", checkId: "UnusedAutoPropertyAccessor.Local")]
    [SuppressMessage(category: "ReSharper", checkId: "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage(category: "ReSharper", checkId: "AutoPropertyCanBeMadeGetOnly.Local")]
    public class TodoItemEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoApi.Domain.Entities.TodoItemEntity"/> class.
        /// </summary>
        /// <param name="title">Task title.</param>
        /// <param name="status">Task status.</param>
        public TodoItemEntity(string title, TodoTaskStatus status) : this()
        {
            SetTitle(title: title);
            SetStatus(status: status);
        }

        /// <summary>
        ///     Default values for creating a record in the database.
        /// </summary>
        public TodoItemEntity()
        {
            CreationHistory = DateTimeOffset.UtcNow;
        }

        /// <summary>
        ///     Gets the primary key for this task.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        ///     Gets task title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     Gets normalized task title.
        /// </summary>
        public string NormalizedTitle { get; private set; }

        /// <summary>
        ///     Sets task title.
        /// </summary>
        /// <param name="title">New task title.</param>
        /// <exception cref="System.ArgumentException">The title must not be null.</exception>
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(value: title))
                throw new ArgumentException(message: "Value cannot be null or whitespace.", paramName: nameof(title));

            Title = title.Trim();
            NormalizedTitle = title.Normalize().ToUpperInvariant().Trim();
        }

        /// <summary>
        ///     Gets task status.
        /// </summary>
        public TodoTaskStatus Status { get; private set; }

        /// <summary>
        ///     Sets task status.
        /// </summary>
        /// <param name="status">New task status.</param>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The status is invalid.</exception>
        public void SetStatus(TodoTaskStatus status)
        {
            if (!Enum.IsDefined(enumType: typeof(TodoTaskStatus), value: status))
                throw new InvalidEnumArgumentException(argumentName: nameof(status), invalidValue: (int) status, enumClass: typeof(TodoTaskStatus));

            Status = status;
        }

        /// <summary>
        ///     Gets system creation history time.
        /// </summary>
        /// <remarks>Created in Universal Coordinated Universal Time (UTC).</remarks>
        public DateTimeOffset CreationHistory { get; private set; }
    }
}