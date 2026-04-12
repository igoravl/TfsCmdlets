namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.ServiceHook;

public partial class ServiceHookControllerTests
{
    [Fact]
    public async Task CanGenerate_GetServiceHookConsumerController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetServiceHookConsumerController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookConsumer.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetServiceHookNotificationHistoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetServiceHookNotificationHistoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookNotificationHistory.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetServiceHookPublisherController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetServiceHookPublisherController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookPublisher.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetServiceHookSubscriptionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetServiceHookSubscriptionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookSubscription.cs"
            });
    }
}