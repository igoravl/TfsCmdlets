namespace TfsCmdlets.SourceGenerators.UnitTests.HttpClientGenerator;

public partial class HttpClientGeneratorTests
{
    [Fact]
    public async Task Can_Create_IIdentityHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(Can_Create_IIdentityHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IIdentityHttpClient.cs"
            });
    }

}