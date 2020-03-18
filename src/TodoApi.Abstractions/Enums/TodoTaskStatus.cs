namespace TodoApi.Abstractions.Enums
{
    /// <summary>
    ///     Task statuses
    /// </summary>
    public enum TodoTaskStatus
    {
        /// <summary>
        ///     Task is opened
        /// </summary>
        Open,
        
        /// <summary>
        ///     Task is deferred
        /// </summary>
        Deferred,
        
        /// <summary>
        ///     Task is closed
        /// </summary>
        Closed
    }
}