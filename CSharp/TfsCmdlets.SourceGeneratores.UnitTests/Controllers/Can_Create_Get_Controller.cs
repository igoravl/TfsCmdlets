namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_Get_Controller()
    {
        var source = """
                   using System.Management.Automation;
                   using Microsoft.TeamFoundation.SourceControl.WebApi;
                   
                   namespace TfsCmdlets.Cmdlets.Git
                   {
                       [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitRepository), DefaultParameterSetName = "Get by ID or Name")]
                       partial class GetGitRepository
                       {
                           [Parameter(Position = 0, ParameterSetName = "Get by ID or Name")]
                           [SupportsWildcards()]
                           [Alias("Name")]
                           public object Repository { get; set; } = "*";
                       
                           [Parameter(ParameterSetName = "Get default", Mandatory = true)]
                           public SwitchParameter Default { get; set; }
                       
                           [Parameter()]
                           public SwitchParameter IncludeParent { get; set; }
                       }
                       
                       [CmdletController(typeof(GitRepository), Client=typeof(IGitHttpClient))]
                       partial class GetGitRepositoryController
                       {
                           protected override IEnumerable Run()
                           {
                                return null;
                            }
                       }
                    }
                    
                   namespace TfsCmdlets.Controllers
                   {
                       public abstract class ControllerBase
                       {
                       }
                   }
                   """;

        await TestHelper.Verify<Generators.Controllers.ControllerGenerator>(nameof(Can_Create_Get_Controller), source);
    }

}