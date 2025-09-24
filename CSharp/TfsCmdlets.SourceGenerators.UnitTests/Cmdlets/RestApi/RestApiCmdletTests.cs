namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.RestApi;

public partial class RestApiCmdletTests
{
    [Fact]
    public async Task CanGenerate_InvokeRestApiCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_InvokeRestApiCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\RestApi\\InvokeRestApi.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetRestClientCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetRestClientCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\RestApi\\GetRestClient.cs"
            });
    }
}