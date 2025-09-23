using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    [Generator]
    public class HttpClientGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var clientsToGenerate = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "TfsCmdlets.HttpClientAttribute",
                    predicate: (_, _) => true,
                    transform: static (ctx, _) => HttpClientInfo.Create(ctx))
                .Where(static m => m is not null)
                .Select((m, _) => m!);

            context.RegisterSourceOutput(clientsToGenerate,
                static (spc, source) =>
                {
                    var result = GenerateCode(source);
                    var filename = source.FileName;
                    spc.AddSource(filename, SourceText.From(result, Encoding.UTF8));
                });
        }

        private static string GenerateCode(HttpClientInfo model)
        {
            return $$"""
                     #pragma warning disable CS8669
                     using System.Composition;
                     {{model.UsingsStatements}}

                     namespace {{model.Namespace}}
                     {
                         public partial interface {{model.Name}}: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
                         {
                     {{model.GetInterfaceBody()}}
                         }
                         
                         [Export(typeof({{model.Name}}))]
                         [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                         internal class {{model.Name}}Impl: {{model.Name}}
                         {
                             private {{model.OriginalType}} _client;
                             
                             protected IDataManager Data { get; }
                             
                             [ImportingConstructor]
                             public {{model.Name}}Impl(IDataManager data)
                             {
                                 Data = data;
                             }
                             
                             private {{model.OriginalType}} Client
                             {
                                 get
                                 {
                                     if(_client == null)
                                     {
                                         _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof({{model.OriginalType}})) as {{model.OriginalType}};
                                     }
                                     return _client;
                                 }
                             }
                             
                     {{model.GetClassBody()}}
                             public Uri BaseAddress
                                => Client.BaseAddress;
                             
                             public bool ExcludeUrlsHeader
                             {
                                get => Client.ExcludeUrlsHeader;
                                set => Client.ExcludeUrlsHeader = value;
                             }
                             
                             public Microsoft.VisualStudio.Services.WebApi.VssResponseContext LastResponseContext
                                => Client.LastResponseContext;
                                
                             public bool LightweightHeader
                             {
                                get => Client.LightweightHeader;
                                set => Client.LightweightHeader = value;
                             }
                             
                             public bool IsDisposed()
                                => Client.IsDisposed();
                             
                             public void SetResourceLocations(Microsoft.VisualStudio.Services.WebApi.ApiResourceLocationCollection resourceLocations)
                                => Client.SetResourceLocations(resourceLocations);

                             public void Dispose()
                     	        => Client.Dispose();
                        }
                     }
                     """;
        }

    }
}
