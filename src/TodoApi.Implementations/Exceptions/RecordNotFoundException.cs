using System;
using System.Runtime.Serialization;

namespace TodoApi.Implementations.Exceptions
{
    /// <inheritdoc />
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        public override string Message { get; }

        /// <inheritdoc />
        public RecordNotFoundException()
        {
        }

        /// <inheritdoc />
        public RecordNotFoundException(string message) : base(message)
        {
        }
        
        /// <inheritdoc />
        public RecordNotFoundException(string entity, object key) 
        {
            Message = $"Entity '{entity}' does not matter with the '{key}' key.";
        }

        /// <inheritdoc />
        public RecordNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        protected RecordNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}