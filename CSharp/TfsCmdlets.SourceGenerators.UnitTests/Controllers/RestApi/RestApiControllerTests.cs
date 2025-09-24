namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.RestApi;

public partial class RestApiControllerTests
{
    [Fact]
    public async Task CanGenerate_GetRestClientController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetRestClientController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\RestApi\\GetRestClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_InvokeRestApiController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_InvokeRestApiController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\RestApi\\InvokeRestApi.cs"
            });
    }
}