/*
.SYNOPSIS
	Clones a test plan and, optionally, its test suites and test cases

.DESCRIPTION
	The Copy-TfsTestPlan copies ("clones") a test plan to help duplicate test suites and/or test cases. Cloning is useful if you want to branch your application into two versions: after copying, the tests for the two versions can be changed without affecting each other.

.EXAMPLE
	Copy-TfsTestPlan -TestPlan "My test plan" -Project "SourceProject" -Destination "TargetProject" -NewName "My new test plan"

.INPUTS
	Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan

.OUTPUTS
	Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan

.NOTES
	When you clone a test suite, the following objects are copied from the source test plan to the destination test plan: 

    * Test cases (note: Each new test case retains its shared steps. A link is made between the source and new test cases. The new test cases do not have test runs, bugs, test results, and build information);
    * Shared steps referenced by cloned test cases;
    * Test suites (note: The following data is retained - Names and hierarchical structure of the test suites; Order of the test cases; Assigned testers; Configurations)
    * Action Recordings linked from a cloned test case
    * Links and Attachments
    * Test configuration
	
	The items below are only copied when using -CloneRequirements:

    * Requirements-based suites;
    * Requirements work items (product backlog items or user stories);
    * Bug work items, when in a project that uses the Scrum process template or any other project in which the Bug work item type is in the Requirements work item category. In other projects, bugs are not cloned.

.PARAMETER TestPlan

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER NewName

.PARAMETER DestinationProject

.PARAMETER AreaPath

.PARAMETER IterationPath

.PARAMETER DeepClone

.PARAMETER CopyAllSuites

.PARAMETER CopyAncestorHierarchy

.PARAMETER DestinationWorkItemType

.PARAMETER SuiteIds

.PARAMETER RelatedLinkComment

.PARAMETER Passthru
	Returns the results of the command. By default, this cmdlet does not generate any output. 

.PARAMETER Collection
	Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

*/

using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    [Cmdlet(VerbsCommon.Copy, "TestPlan")]
    [OutputType(typeof(TestPlan))]
    public class CopyTestPlan: BaseCmdlet
    {
/*
        [Parameter(ValueFromPipeline=true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object TestPlan { get; set; }

        [Parameter()]
        public object Project,

        [Parameter()]
        [string] 
        NewName,

		[Parameter()]
		[Alias("Destination")]
        public object DestinationProject,

        [Parameter()]
        public string AreaPath,

        [Parameter()]
        public string IterationPath,

        [Parameter()]
        [SwitchParameter] 
        DeepClone,

		[Parameter()]
		[Alias("Recurse")]
        [SwitchParameter] 
        CopyAllSuites,

        [Parameter()]
        [SwitchParameter] 
        CopyAncestorHierarchy,

        [Parameter()]
        [SwitchParameter] 
        CloneRequirements,

        [Parameter()]
        public string DestinationWorkItemType = "Test Case",

        [Parameter()]
        [int[]] 
		SuiteIds,
		
		[Parameter()]
		public string RelatedLinkComment { get; set; }

        [Parameter()]
		[ValidateSet("Original", "Copy", "None")]
        public string Passthru { get; set; } = "None",

        [Parameter()]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
		#_ImportRequiredAssembly -AssemblyName "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
		#_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.TestManagement.WebApi"
		ns = "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
    }

    protected override void ProcessRecord()
    {
		plan = Get-TfsTestPlan -TestPlan TestPlan -Project Project -Collect Collection

		if(! plan)
		{
            throw new Exception($"Invalid or non-existent test plan {TestPlan}")
		}

		destTp = Get-TfsTeamProject -Project DestinationProject -Collection Collection

		if(! destTp)
		{
            throw new Exception($"Invalid or non-existent team project {DestinationProject}")
		}

		if (! Project)
		{
			Project = plan.Project.Name
		}

		tp = this.GetProject();
		
		if(! tp)
		{
            throw new Exception($"Invalid or non-existent team project {Project}")
		}

		if (! NewName)
		{
			if(tp.Name != destTp.Name)
			{
				NewName = plan.Name
			}
			else
			{
				NewName = $"{{plan}.Name} (cloned $(DateTime.Now.ToShortDateString()))"
			}
		}

		if (! AreaPath)
		{
			AreaPath = destTp.Name
		}

		if (! IterationPath)
		{
			IterationPath = destTp.Name
		}

        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
		client = Get-TfsRestClient $"{ns}.TestPlanHttpClient" -Collection tpc

		cloneParams = New-Object $"{ns}.CloneTestPlanParams" -Property @{
			sourceTestPlan = New-Object $"{ns}.SourceTestPlanInfo" -Property @{
				Id = plan.Id
			};
			destinationTestPlan = New-Object $"{ns}.DestinationTestPlanCloneParams" -Property @{
				Project = destTp.Name;
				Name = NewName;
				AreaPath = AreaPath;
				Iteration = IterationPath
			};
			cloneOptions = new Microsoft.TeamFoundation.TestManagement.WebApi.CloneOptions() -Property @{
				RelatedLinkComment = RelatedLinkComment;
				CopyAllSuites = CopyAllSuites.IsPresent;
				CopyAncestorHierarchy = CopyAncestorHierarchy;
				DestinationWorkItemType = DestinationWorkItemType;
				CloneRequirements = CloneRequirements;
				OverrideParameters = _NewDictionary @([string],[string]) @{
					"System.AreaPath" = AreaPath;
					"System.IterationPath" = IterationPath
				}
			}
		}
		
		task = client.CloneTestPlanAsync(cloneParams, tp.Name, DeepClone.IsPresent)

		result = task.Result; if(task.IsFaulted) { _throw new Exception("Error cloning test plan" task.Exception.InnerExceptions })

		opInfo = result

		do
		{
			Start-Sleep -Seconds 1
			opInfo = client.GetCloneInformationAsync(tp.Name, opInfo.CloneOperationResponse.opId)
		}
		while (opInfo.CloneOperationResponse.CloneOperationState -match "Queued|InProgress")

		if (opInfo.CloneOperationResponse.CloneOperationState == "Failed")
		{
			throw new Exception($"Error cloning test plan "{{plan}.Name}": $(opInfo.CloneOperationResponse.Message)")
		}
		else
		{
			copy = opInfo.DestinationTestPlan	
		}

		if (Passthru = = "Original")
		{
			WriteObject(plan); return;
		}
		
		if(Passthru = = "Copy")
		{
			WriteObject(copy); return;
		}
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
