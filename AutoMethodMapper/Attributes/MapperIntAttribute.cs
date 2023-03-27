namespace Skyline.DataMiner.Utils.AutoMethodMapper
{
    using System;

    /// <summary>
    /// Specifies that a method can be mapped using the MethodMapper class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class MapperIntAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperIntAttribute"/> class with the specified key.
        /// </summary>
        /// <param name="key">The key used to map the method.</param>
        public MapperIntAttribute(int key)
        {
            Key = key;
        }

        /// <summary>
        /// Gets the key used to map the method.
        /// </summary>
        public int Key { get; }
    }
}
