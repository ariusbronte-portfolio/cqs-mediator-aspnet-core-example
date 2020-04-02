using TodoApi.Abstractions.Enums;

namespace TodoApi.Abstractions.Dto.TodoItemDtos
{
    /// <summary>
    ///     Presents the essence of the tasks.
    /// </summary>
    public class CreateTodoItemDto
    {
        /// <summary>
        ///     Task title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Task status.
        /// </summary>
        public TodoTaskStatus Status { get; set; }
    }
}