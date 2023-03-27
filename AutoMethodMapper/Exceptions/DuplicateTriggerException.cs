namespace Skyline.DataMiner.Utils.AutoMethodMapper.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when duplicate trigger attributes are found.
    /// </summary>
    [Serializable]
    public class DuplicateTriggersException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a default error message.
        /// </summary>
        public DuplicateTriggersException() : base("Duplicate trigger attributes found.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified trigger ID.
        /// </summary>
        /// <param name="trigger">The ID of the trigger that was duplicated.</param>
        public DuplicateTriggersException(int trigger) : base($"Duplicate trigger attributes found for trigger: {trigger}.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified trigger ID and count.
        /// </summary>
        /// <param name="trigger">The ID of the trigger that was duplicated.</param>
        /// <param name="count">The number of times the trigger was duplicated.</param>
        public DuplicateTriggersException(int trigger, int count) : base($"Duplicate trigger attributes found for trigger: {trigger}, number of duplications: {count}.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DuplicateTriggersException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified error message and trigger ID.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="trigger">The ID of the trigger that was duplicated.</param>
        public DuplicateTriggersException(string message, int trigger) : base(message + Environment.NewLine + $"Trigger: {trigger}.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified error message, trigger ID, and count.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="trigger">The ID of the trigger that was duplicated.</param>
        /// <param name="count">The number of times the trigger was duplicated.</param>
        public DuplicateTriggersException(string message, int trigger, int count) : base(message + Environment.NewLine + $"Trigger: {trigger}, duplications: {count}") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DuplicateTriggersException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified error message, trigger ID, and a reference to the inner exception that is the cause
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="trigger">The ID of the trigger that was duplicated.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DuplicateTriggersException(string message, int trigger, Exception innerException) : base(message + Environment.NewLine + $"Trigger: {trigger}.", innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with a specified error message, trigger ID, count and a reference to the inner exception that is the cause
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="trigger">The ID of the trigger that was duplicated.</param>
        /// <param name="count">The number of times the trigger was duplicated.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DuplicateTriggersException(string message, int trigger, int count, Exception innerException) : base(message + Environment.NewLine + $"Trigger: {trigger}, duplications: {count}", innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTriggersException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected DuplicateTriggersException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
