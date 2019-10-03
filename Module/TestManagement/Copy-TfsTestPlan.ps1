<#
.SYNOPSIS
	Clones a test plan and, optionally, its test suites and test cases

.DESCRIPTION
	The Copy-TfsTestPlan copies ("clones") a test plan to help duplicate test suites and/or test cases. Cloning is useful if you want to branch your application into two versions: after copying, the tests for the two versions can be changed without affecting each other.

.EXAMPLE
	Copy-TfsTestPlan -TestPlan 'My test plan' -Project 'SourceProject' -Destination 'TargetProject' -NewName 'My new test plan'

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
	HELP_PARAM_PROJECT

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
	HELP_PARAM_PASSTHRU

.PARAMETER Collection
	HELP_PARAM_COLLECTION

#>
Function Copy-TfsTestPlan
{
    [CmdletBinding()]
    [OutputType('Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan')]
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $TestPlan,

        [Parameter()]
        [object] 
        $Project,

        [Parameter()]
        [string] 
        $NewName,

		[Parameter()]
		[Alias('Destination')]
        [object] 
        $DestinationProject,

        [Parameter()]
        [string] 
        $AreaPath,

        [Parameter()]
        [string] 
        $IterationPath,

        [Parameter()]
        [switch] 
        $DeepClone,

		[Parameter()]
		[Alias("Recurse")]
        [switch] 
        $CopyAllSuites,

        [Parameter()]
        [switch] 
        $CopyAncestorHierarchy,

        [Parameter()]
        [switch] 
        $CloneRequirements,

        [Parameter()]
        [string] 
        $DestinationWorkItemType = 'Test Case',

        [Parameter()]
        [int[]] 
		$SuiteIds,
		
		[Parameter()]
		[string]
		$RelatedLinkComment,

        [Parameter()]
		[ValidateSet('Original', 'Copy', 'None')]
        [string]
        $Passthru = 'None',

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
		REQUIRES(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi)
		REQUIRES(Microsoft.TeamFoundation.TestManagement.WebApi)
		$ns = 'Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi'
    }

    Process
    {
		$plan = Get-TfsTestPlan -TestPlan $TestPlan -Project $Project -Collect $Collection

		if(-not $plan)
		{
            throw "Invalid or non-existent test plan $TestPlan"
		}

		$destTp = Get-TfsTeamProject -Project $DestinationProject -Collection $Collection

		if(-not $destTp)
		{
            throw "Invalid or non-existent team project $DestinationProject"
		}

		if (-not $Project)
		{
			$Project = $plan.Project.Name
		}

		$tp = Get-TfsTeamProject -Project $Project -Collection $Collection
		
		if(-not $tp)
		{
            throw "Invalid or non-existent team project $Project"
		}

		if (-not $NewName)
		{
			if($tp.Name -ne $destTp.Name)
			{
				$NewName = $plan.Name
			}
			else
			{
				$NewName = "$($plan.Name) (cloned $([DateTime]::Now.ToShortDateString()))"
			}
		}

		if (-not $AreaPath)
		{
			$AreaPath = $destTp.Name
		}

		if (-not $IterationPath)
		{
			$IterationPath = $destTp.Name
		}

        $tpc = $tp.Store.TeamProjectCollection
		
		GET_CLIENT("$ns.TestPlanHttpClient")

		$cloneParams = New-Object "$ns.CloneTestPlanParams" -Property @{
			sourceTestPlan = New-Object "$ns.SourceTestPlanInfo" -Property @{
				Id = $plan.Id
			};
			destinationTestPlan = New-Object "$ns.DestinationTestPlanCloneParams" -Property @{
				Project = $destTp.Name;
				Name = $NewName;
				AreaPath = $AreaPath;
				Iteration = $IterationPath
			};
			cloneOptions = New-Object "Microsoft.TeamFoundation.TestManagement.WebApi.CloneOptions" -Property @{
				RelatedLinkComment = $RelatedLinkComment;
				CopyAllSuites = $CopyAllSuites.IsPresent;
				CopyAncestorHierarchy = $CopyAncestorHierarchy;
				DestinationWorkItemType = $DestinationWorkItemType;
				CloneRequirements = $CloneRequirements;
				OverrideParameters = _NewDictionary @([string],[string]) @{
					'System.AreaPath' = $AreaPath;
					'System.IterationPath' = $IterationPath
				}
			}
		}
		
		$task = $client.CloneTestPlanAsync($cloneParams, $tp.Name, $DeepClone.IsPresent)

		CHECK_ASYNC($task,$result,'Error cloning test plan')

		$opInfo = $result

		do
		{
			Start-Sleep -Seconds 1
			$opInfo = $client.GetCloneInformationAsync($tp.Name, $opInfo.CloneOperationResponse.opId)
		}
		while ($opInfo.CloneOperationResponse.CloneOperationState -match 'Queued|InProgress')

		if ($opInfo.CloneOperationResponse.CloneOperationState -eq 'Failed')
		{
			throw "Error cloning test plan '$($plan.Name)': $($opInfo.CloneOperationResponse.Message)"
		}
		else
		{
			$copy = $opInfo.DestinationTestPlan	
		}

		if ($Passthru -eq 'Original')
		{
			return $plan
		}
		
		if($Passthru -eq 'Copy')
		{
			return $copy
		}
    }
}
