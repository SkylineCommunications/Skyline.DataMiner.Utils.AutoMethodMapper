namespace Skyline.DataMiner.Utils.AutoMethodMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Skyline.DataMiner.Utils.AutoMethodMapper.Exceptions;

    /// <summary>
    /// Provides a dictionary with all the method with an Attribute <see cref="MapperStringAttribute"/> attached to it.
    /// </summary>
    public class MethodMapperByString : MethodMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperStringAttribute"/> class.
        /// This class will generate a dictionary with all the method with an Attribute <see cref="MapperStringAttribute"/> attached to it.
        /// </summary>
        public MethodMapperByString() : base(typeof(MapperStringAttribute)) { }

        /// <summary>
        /// Gets or Sets the id of the parameter that triggered the QAction.
        /// </summary>
        public string TriggerId { get; protected set; }

        /// <summary>
        /// Gets the Dictionary that has the mappings of all the functions with an <see cref="MapperStringAttribute"/> and the given function.
        /// </summary>
        protected Dictionary<string, MethodInfo> Actions { get; }

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
        public virtual void Process(string trigger, params object[] args)
        {
            try
            {
                this.Mapping.FirstOrDefault(pair => ((MapperStringAttribute)pair.Key).Key == trigger).Value.Invoke(this, args);
            }
            catch (Exception ex)
            {
                throw new TriggerNotSupportedException(trigger);
            }
        }
    }
}
