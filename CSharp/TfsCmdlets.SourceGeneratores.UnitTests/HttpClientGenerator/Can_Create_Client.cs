namespace TfsCmdlets.SourceGenerators.UnitTests.HttpClientGenerator;

public partial class HttpClientGeneratorTests
{
    [Fact]
    public async Task Can_Create_Client()
    {
        var source = """
                   using Microsoft.VisualStudio.Services.Licensing.Client;

                   namespace TfsCmdlets.HttpClients
                   {
                       [HttpClient(typeof(AccountLicensingHttpClient))]
                       partial interface IAccountLicensingHttpClient
                       {
                       }
                   }
                   """;

        await TestHelper.Verify<Generators.HttpClients.HttpClientGenerator>(nameof(Can_Create_Client), source);
    }

}