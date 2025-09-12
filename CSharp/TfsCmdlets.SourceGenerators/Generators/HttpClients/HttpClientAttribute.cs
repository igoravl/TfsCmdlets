using System;
using System.Collections.Generic;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public static class HttpClientAttribute
    {
        public const string CODE = """
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
                                   """;
    }
}
