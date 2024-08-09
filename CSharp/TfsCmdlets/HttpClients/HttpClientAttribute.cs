namespace TfsCmdlets.HttpClients
{
    internal class HttpClientAttribute : Attribute
    {
        public HttpClientAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}