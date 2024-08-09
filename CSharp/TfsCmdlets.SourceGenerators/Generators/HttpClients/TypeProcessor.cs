using System;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public class TypeProcessor : BaseTypeProcessor
    {
        private HttpClientInfo _client;

        protected override void OnInitialize()
        {
            try
            {
                _client = new HttpClientInfo(Type, Context, Logger);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error initializing HttpClientInfo for {Type.FullName()}");
                Ignore = true;
            }
        }

        protected string GetInterfaceBody()
        {
            var sb = new StringBuilder();

            foreach (var method in _client.GenerateMethods())
            {
                sb.Append($"\t\t{method}");
                sb.AppendLine(";");
            }

            return sb.ToString();
        }

        private string GetClassBody()
        {
            var sb = new StringBuilder();

            foreach (var method in _client.GenerateMethods())
            {
                sb.Append($"\t\t{method.ToString($"\t\t\t=> Client.{method.Name}{method.SignatureNamesOnly};")}");
            }

            return sb.ToString();
        }


        public override string GenerateCode()
        {
            var client = _client;

            return $$"""
                    using System.Composition;
                    
                    namespace {{client.Namespace}}
                    {
                        public partial interface {{client.Name}}: IVssHttpClient
                        {
                    {{GetInterfaceBody()}}
                        }
                        
                        [Export(typeof({{client.Name}}))]
                        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                        internal class {{client.Name}}Impl: {{client.Name}}
                        {
                            private {{client.OriginalType}} _client;
                            
                            protected IDataManager Data { get; }
                            
                            [ImportingConstructor]
                            public {{client.Name}}Impl(IDataManager data)
                            {
                                Data = data;
                            }
                            
                            private {{client.OriginalType}} Client
                            {
                                get
                                {
                                    if(_client == null)
                                    {
                                        _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof({{client.OriginalType}})) as {{client.OriginalType}};
                                    }
                                    return _client;
                                }
                            }
                            
                    {{GetClassBody()}}
                        }
                    }
                    """;
        }
    }
}
