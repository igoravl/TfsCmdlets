namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Defines an automatic parameter.
    /// </summary>
    /// <remarks>
    /// This attribute is used to mark a parameter that should be automatically populated by the underlying 
    /// infrastructure, typically by fetching an associated resource.
    /// </remarks>
    public class AutoParameterAttribute: Attribute
    {
        /// <summary>
        /// The type of the parameter
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Creates a new instance of the AutoParameterAttribute class
        /// </summary>
        /// <param name="type">The type of the parameter</param>
        public AutoParameterAttribute(Type type)
        {
            Type = type;
        }
    }
}