namespace Skyline.DataMiner.Utils.AutoMethodMapper.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The <see cref="TriggerNotSupportedException"/> exception is thrown when a trigger is not supported.
    /// </summary>
    [Serializable]
    public class TriggerNotSupportedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerNotSupportedException"/> class with a default message.
        /// </summary>
        public TriggerNotSupportedException() : base("The trigger is not supported.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerNotSupportedException"/> class with a specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TriggerNotSupportedException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerNotSupportedException"/> class with an inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception that caused this exception to be thrown.</param>
        public TriggerNotSupportedException(Exception innerException) : base("The trigger is not supported.", innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerNotSupportedException"/> class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The inner exception that caused this exception to be thrown.</param>
        public TriggerNotSupportedException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerNotSupportedException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected TriggerNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
