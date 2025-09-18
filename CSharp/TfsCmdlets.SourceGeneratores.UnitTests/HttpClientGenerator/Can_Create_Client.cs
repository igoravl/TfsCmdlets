namespace TfsCmdlets.SourceGenerators.UnitTests.HttpClientGenerator;

public partial class HttpClientGeneratorTests
{
    [Fact]
    public async Task Can_Create_HttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(Can_Create_HttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IAccountLicensingHttpClient.cs"
            });
    }

}