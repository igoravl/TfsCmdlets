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
                _client = new HttpClientInfo(Type, Context);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error initializing HttpClientInfo for {Type.FullName()}");
                Ignore = true;
            }
        }
        
        public override string GenerateCode() => _client.GenerateCode();
    }
}
