namespace TfsCmdlets.SourceGenerators
{
    public class GeneratedProperty
    {
        public string Name { get; }
        public string Type { get; }
        public bool IsHidden { get; }
        private string _generatedCode;

        internal GeneratedProperty(string name, string typeName, string generatedCode)
        {
            Name = name;
            Type = typeName;
            _generatedCode = generatedCode;
        }

        internal GeneratedProperty(string name, string typeName, bool isHidden, string generatedCode)
            : this(name, typeName, generatedCode)
        {
            IsHidden = isHidden;
        }

        public override string ToString()
        {
            return _generatedCode;
        }
    }
}