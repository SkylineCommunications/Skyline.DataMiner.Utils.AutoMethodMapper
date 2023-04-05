namespace Skyline.DataMiner.Utils.AutoMethodMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Skyline.DataMiner.Utils.AutoMethodMapper.Exceptions;

    /// <summary>
    /// Provides a dictionary with all the methods of an object with a given <see cref="Attribute"/> attached to it.
    /// </summary>
    public abstract class MethodMapper
    {
        /// <summary>
        /// This class will generate a dictionary with all the methods of an object with a given <see cref="Attribute"/> attached to it.
        /// </summary>
        /// <param name="attributeType"></param>
        protected MethodMapper(Type attributeType)
        {
            this.AttributeType = attributeType;
            this.Mapping = GenerateFunctionMapping();
        }

        /// <summary>
        /// The Type of the attribute that the mapper should search for.
        /// </summary>
        protected Type AttributeType { get; }

        /// <summary>
        /// Gets the Dictionary that has the mappings of all the functions with an given Attribute.
        /// </summary>
        protected Dictionary<Attribute, MethodInfo> Mapping { get; }

        /// <summary>
        /// This will search for the right method to invoke based on the given TriggerId.
        /// </summary>
        /// <param name="args">A list of arguments for the handle function.</param>
        public abstract void Process(params object[] args);

        /// <summary>
        /// This method will retrieve all methods with a given <see cref="Attribute"/> of the current object.
        /// </summary>
        /// <returns>A Dictionary with the <see cref="Attribute"/> as key and the <see cref="MethodInfo"/> as value.</returns>
        /// <exception cref="DuplicateTriggersException">Exception when multiple methods are found for a trigger.</exception>
        protected Dictionary<Attribute, MethodInfo> GenerateFunctionMapping()
        {
            return this.GenerateFunctionMapping(this.AttributeType);
        }

        /// <summary>
        /// This method will retrieve all methods with a given <see cref="Attribute"/> of the current object.
        /// </summary>
        /// <param name="attributeType">The type of the custom Attribute used in the child class.</param>
        /// <returns>A Dictionary with the <see cref="Attribute"/> as key and the <see cref="MethodInfo"/> as value.</returns>
        /// <exception cref="DuplicateTriggersException">Exception when multiple methods are found for a trigger.</exception>
        protected Dictionary<Attribute, MethodInfo> GenerateFunctionMapping(Type attributeType)
        {
            // Get all the methods with a MapperAttribute
            var methods = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .SelectMany(method => method.GetCustomAttributes(attributeType)
                    .Select(attribute => new
                    {
                        AttributeValue = attribute,
                        Method = method,
                    }));

            // Check if there are multiple entries for one trigger
            var groups = methods.GroupBy(pair => pair.AttributeValue)
                .Where(group => group.Count() > 1)
                .Select(group => new { Trigger = group.Key, Count = group.Count() })
                .FirstOrDefault();

            if (groups != null)
                throw new DuplicateTriggersException();

            return methods.ToDictionary(entry => entry.AttributeValue, entry => entry.Method);
        }
    }
}
