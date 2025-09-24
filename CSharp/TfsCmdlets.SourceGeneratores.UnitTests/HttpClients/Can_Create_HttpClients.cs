namespace TfsCmdlets.SourceGenerators.UnitTests.HttpClients;

public partial class HttpClientGeneratorTests
{
    [Fact]
    public async Task CanGenerate_IAccountLicensingHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IAccountLicensingHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IAccountLicensingHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IBuildHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IBuildHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IBuildHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IExtensionManagementHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IExtensionManagementHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IExtensionManagementHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IFeedHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IFeedHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IFeedHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IGenericHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IGenericHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IGenericHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IGitExtendedHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IGitExtendedHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IGitExtendedHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IGitHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IGitHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IGitHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IGraphHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IGraphHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IGraphHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IIdentityHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IIdentityHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IIdentityHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IOperationsHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IOperationsHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IOperationsHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IPolicyHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IPolicyHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IPolicyHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IProcessHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IProcessHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IProcessHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IProjectHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IProjectHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IProjectHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IReleaseHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IReleaseHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IReleaseHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IReleaseHttpClient2()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IReleaseHttpClient2),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IReleaseHttpClient2.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ISearchHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_ISearchHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\ISearchHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IServiceHooksPublisherHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IServiceHooksPublisherHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IServiceHooksPublisherHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ITaggingHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_ITaggingHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\ITaggingHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ITeamAdminHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_ITeamAdminHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\ITeamAdminHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ITeamHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_ITeamHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\ITeamHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ITestPlanHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_ITestPlanHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\ITestPlanHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IVssHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IVssHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IVssHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IWikiHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IWikiHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IWikiHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IWorkHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IWorkHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IWorkHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IWorkItemTrackingHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IWorkItemTrackingHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IWorkItemTrackingHttpClient.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_IWorkItemTrackingProcessHttpClient()
    {
        await TestHelper.VerifyFiles<Generators.HttpClients.HttpClientGenerator>(
            nameof(CanGenerate_IWorkItemTrackingProcessHttpClient),
            new[]
            {
                "TfsCmdlets.Shared\\HttpClients\\IWorkItemTrackingProcessHttpClient.cs"
            });
    }
}