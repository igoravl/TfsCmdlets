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
		[Alias('Destination')]
        [object] 
        $DestinationProject,

        [Parameter()]
        [string] 
        $NewName,

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
        $DestinationWorkItemType = 'Test Plan',

        [Parameter()]
        [int[]] 
		$SuiteIds,
		
		[Parameter()]
		[string]
		$RelatedLinkComment,

        [Parameter()]
		[ValidateSet('Original', 'Copy', 'None')]
        [string]
        $Passthru = 'Copy',

        [Parameter()]
        [object] 
        $Project,

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

		if (-not $AreaPath)
		{
			$AreaPath = $destTp.Name
		}

		if (-not $IterationPath)
		{
			$IterationPath = $destTp.Name
		}

        $tpc = $tp.Store.TeamProjectCollection
		
		$client = _GetRestClient "$ns.TestPlanHttpClient" -Collection $tpc

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
					'System.IteratioPath' = $IterationPath
					'projectName' = $destTp.Name
				}
			}
		}
		
		$resultTask = $client.CloneTestPlanAsync($cloneParams, $tp.Name, $DeepClone.IsPresent)
		$opInfo = $resultTask.Result

		if (-not $opInfo)
		{
			throw "Error cloning test plan: $($resultTask.Exception.InnerExceptions | ForEach-Object {$_.ToString()})"
		}


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
