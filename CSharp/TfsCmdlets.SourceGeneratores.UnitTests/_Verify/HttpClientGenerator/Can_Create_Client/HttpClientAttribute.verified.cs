//HintName: HttpClientAttribute.cs
using System;

namespace TfsCmdlets
{
    public class HttpClientAttribute : Attribute
    {
        public HttpClientAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}