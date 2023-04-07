namespace Skyline.DataMiner.Utils.AutoMethodMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Skyline.DataMiner.Utils.AutoMethodMapper.Exceptions;

    /// <summary>
    /// Provides a dictionary with all the method with an Attribute <see cref="MapperIntAttribute"/> attached to it.
    /// </summary>
    public class MethodMapperByInt : MethodMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodMapperByInt"/> class.
        /// This class will generate a dictionary with all the method with an Attribute <see cref="MapperIntAttribute"/> attached to it.
        /// </summary>
        public MethodMapperByInt() : base(typeof(MapperIntAttribute)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodMapperByInt"/> class.
        /// This class will generate a dictionary with all the method with an Attribute <see cref="MapperIntAttribute"/> attached to it.
        /// </summary>
        /// <param name="triggerId">The id of the parameter that triggered this object.</param>
        public MethodMapperByInt(int triggerId) : base(typeof(MapperIntAttribute))
        {
            this.TriggerId = triggerId;
        }

        /// <summary>
        /// Gets or Sets the id of the parameter that triggered the QAction.
        /// </summary>
        public int TriggerId { get; protected set; }

        /// <summary>
        /// This will search for the right method to invoke based on the given TriggerId.
        /// </summary>
        /// <param name="args">A list of arguments for the handle function.</param>
        public override void Process(params object[] args)
        {
            this.Process(this.TriggerId, args);
        }

        /// <summary>
        /// This will search for the right method to invoke based on the given TriggerId.
        /// </summary>
        /// <param name="trigger">The trigger key or index of the Actions dictionary</param>
        /// <param name="args">A list of arguments for the handle function.</param>
        public virtual void Process(int trigger, params object[] args)
        {
            try
            {
                this.Mapping.FirstOrDefault(pair => ((MapperIntAttribute)pair.Key).Key == trigger).Value.Invoke(this, args);
            }
            catch (Exception ex)
            {
                throw new TriggerNotSupportedException($"The trigger with id: {trigger}, is not supported.", ex);
            }
        }
    }
}
