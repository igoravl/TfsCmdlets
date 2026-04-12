namespace TfsCmdlets.Models
{
    public class Parameter
    {
        public static Parameter Missing { get; } = new Parameter(null, null);

        public static bool IsMissing(Parameter parameter)
        {
            return parameter == Missing;
        }

        public string Name { get; set; }
        
        public object Value { get; set; }

        private Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}