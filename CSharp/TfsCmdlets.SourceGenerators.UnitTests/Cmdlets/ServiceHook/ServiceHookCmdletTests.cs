namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.ServiceHook;

public partial class ServiceHookCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetServiceHookSubscriptionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetServiceHookSubscriptionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookSubscription.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetServiceHookNotificationHistoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetServiceHookNotificationHistoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookNotificationHistory.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetServiceHookPublisherCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetServiceHookPublisherCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookPublisher.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetServiceHookConsumerCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetServiceHookConsumerCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ServiceHook\\GetServiceHookConsumer.cs"
            });
    }
}