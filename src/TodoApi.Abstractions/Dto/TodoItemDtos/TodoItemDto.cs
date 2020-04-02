using TodoApi.Abstractions.Enums;

namespace TodoApi.Abstractions.Dto.TodoItemDtos
{
    /// <summary>
    ///     Presents the essence of the tasks.
    /// </summary>
    public class TodoItemDto
    {
        /// <summary>
        ///     The primary key for this task.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Task title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Normalized task title.
        /// </summary>
        public string NormalizedTitle { get; set; }

        /// <summary>
        ///     Task status.
        /// </summary>
        public TodoTaskStatus Status { get; set; }
    }
}