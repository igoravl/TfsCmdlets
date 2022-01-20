namespace TfsCmdlets.SourceGenerators
{
    public class GeneratedParameterProperty: GeneratedProperty
    {
        internal GeneratedParameterProperty(string name, string typeName, string parameterAttribute, string summaryText)
            : base(name, typeName, string.Format(_ParameterProperty, name, typeName, parameterAttribute, summaryText))
        {
        }

        private static readonly string _ParameterProperty = @"
        /// <summary>
        /// {3}
        /// </summary>
        [Parameter({2}})]
        public {1} {0} { get; set; }
";
    }
}