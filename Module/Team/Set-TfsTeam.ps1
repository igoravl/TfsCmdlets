<#
.SYNOPSIS
Changes the details of a team.

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.Client.TeamFoundationTeam
System.String
#>
Function Set-TfsTeam
{
    [CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact="Medium")]
    [OutputType('Microsoft.TeamFoundation.Client.TeamFoundationTeam')]
    param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [Alias("Name")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TeamFoundationTeam])})] 
        [SupportsWildcards()]
        [object]
        $Team = '*',

        [Parameter()]
        [switch]
        $Default,

        [Parameter()]
        [string]
        $NewName,

        [Parameter()]
        [string]
        $Description,

        [Parameter()]
        [string]
        $TeamFieldValue,

        [Parameter()]
        [hashtable]
        $AreaPaths,

        [Parameter()]
        [string]
        $BacklogIteration,

        [Parameter()]
        [string]
        $IterationPaths,

        # Default iteration macro
        [Parameter()]
        [string]
        $DefaultIterationMacro, #= '@CurrentIteration'
    
        # Working Days. Defaults to Monday thru Friday
        [Parameter()]
        [string[]]
        $WorkingDays, #= @("monday", "tuesday", "wednesday", "thursday", "friday"),

        # Bugs behavior
        [Parameter()]
        [ValidateSet('AsTasks', 'AsRequirements', 'Off')]
        [string]
        $BugsBehavior,

        [Parameter()]
        [hashtable]
        $BacklogVisibilities,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.Work.WebApi)
    }

    Process
    {
        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection
        
        if ($Project)
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            $tpc = $tp.Store.TeamProjectCollection
        }
        else
        {
            $tpc = Get-TfsTeamProjectCollection -Collection $Collection
        }

        $teamService = $tpc.GetService([type]'Microsoft.TeamFoundation.Client.TfsTeamService')

        if ($NewName -and $PSCmdlet.ShouldProcess($Team, "Rename team to '$NewName'"))
        {
            $isDirty = $true
            $t.Name = $NewName
        }

        if ($PSBoundParameters.ContainsKey('Description') -and $PSCmdlet.ShouldProcess($Team, "Set team's description to '$Description'"))
        {
            $isDirty = $true
            $t.Description = $Description
        }

        if ($Default -and $PSCmdlet.ShouldProcess($Team, "Set team to project's default team"))
        {
            $teamService.SetDefaultTeam($t)
        }

        if($isDirty)
        {
            $teamService.UpdateTeam($t)
        }

        # Prepare for the second stage

        $client = _GetRestClient 'Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient' -Collection $tpc
        $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList @($tp.Name, $t.Name)

        # Set Team Field and Area Path settings

        $patch = New-Object 'Microsoft.TeamFoundation.Work.WebApi.TeamFieldValuesPatch'

        if($TeamFieldValue -and $PSCmdlet.ShouldProcess($Team, "Set the team's team field value to $TeamFieldValue"))
        {
            if($tpc.IsHostedServer)
            {
                _Log "Conected to Azure DevOps Server. Treating Team Field Value as Area Path"

                $TeamFieldValue = _NormalizeCssNodePath -Project $tp.Name -Path $TeamFieldValue -IncludeTeamProject

                if(-not $AreaPaths)
                {
                    _Log "AreaPaths is empty and TeamFieldValue is an area path. Adding TeamFieldValue to AreaPaths as default value."

                    $AreaPaths = @{ $TeamFieldValue = $true }
                }
            }

            _Log "Setting team field to $TeamFieldValue"

            $patch = New-Object 'Microsoft.TeamFoundation.Work.WebApi.TeamFieldValuesPatch' -Property @{
                DefaultValue = $TeamFieldValue
            }

            $values = @()

            foreach($a in $AreaPaths.GetEnumerator())
            {
                $values += New-Object 'Microsoft.TeamFoundation.Work.WebApi.TeamFieldValue' -Property @{
                    Value = _NormalizeCssNodePath -Project $tp.Name -Path $a.Key -IncludeTeamProject
                    IncludeChildren = $a.Value
                }
            }

            $patch.Values = [Microsoft.TeamFoundation.Work.WebApi.TeamFieldValue[]] $values

            $resultTask = $client.UpdateTeamFieldValuesAsync($patch, $ctx)
            $result = $resultTask.Result

            if (-not $result)
            {
                throw "Error applying team field value and/or area path settings: $($resultTask.Exception.InnerExceptions | ForEach-Object {$_.ToString()})"
            }
        }

        # Set backlog and iteration path settings

        $patch = New-Object 'Microsoft.TeamFoundation.Work.WebApi.TeamSettingsPatch'
        $isDirty = $false

        if ($BacklogIteration -and $PSCmdlet.ShouldProcess($Team, "Set the team's backlog iteration to $BacklogIteration"))
        {
            _Log "Setting backlog iteration to $BacklogIteration"
            $iteration = Get-TfsIteration -Iteration $BacklogIteration -Project $Project -Collection $Collection
            $patch.BacklogIteration = [guid] $iteration.Id
            $patch.DefaultIteration = [guid] $iteration.Id

            $isDirty = $true
        }

        if ($DefaultIteration -and $PSCmdlet.ShouldProcess($Team, "Set the team's default iteration to $DefaultIteration"))
        {
            _Log "Setting default iteration to $DefaultIteration"
            $iteration = Get-TfsIteration -Iteration $BacklogIteration -Project $Project -Collection $Collection
            $patch.DefaultIteration = [guid] $iteration.Id

            $isDirty = $true
        }

        if ($BacklogVisibilities -and $PSCmdlet.ShouldProcess($Team, "Set the team's backlog visibilities to $($BacklogVisibilities|ConvertTo-Json -Compress)"))
        {
            _Log "Setting backlog iteration to $BacklogVisibilities"
            $patch.BacklogVisibilities = _NewDictionary @([string], [bool]) $BacklogVisibilities

            $isDirty = $true
        }

        if ($DefaultIterationMacro -and $PSCmdlet.ShouldProcess($Team, "Set the team's default iteration macro to $DefaultIterationMacro"))
        {
            _Log "Setting default iteration macro to $DefaultIterationMacro"
            $patch.DefaultIterationMacro = $DefaultIterationMacro

            $isDirty = $true
        }

        if ($WorkingDays -and $PSCmdlet.ShouldProcess($Team, "Set the team's working days to $($WorkingDays|ConvertTo=-Json -Compress)"))
        {
            _Log "Setting working days to $($WorkingDays|ConvertTo=-Json -Compress)"
            $patch.WorkingDays = $WorkingDays

            $isDirty = $true
        }

        if($BugsBehavior -and $PSCmdlet.ShouldProcess($Team, "Set the team's bugs behavior to $($BugsBehavior|ConvertTo-Json -Compress)"))
        {
            _Log "Setting bugs behavior to $($BugsBehavior|ConvertTo-Json -Compress)"
            $patch.BugsBehavior = $BugsBehavior

            $isDirty = $true
        }

        if($isDirty)
        {
            $resultTask = $client.UpdateTeamSettingsAsync($patch, $ctx)
            $result = $resultTask.Result

            if (-not $result)
            {
                Throw "Error applying iteration settings: $($resultTask.Exception.InnerExceptions | ForEach-Object {$_.ToString()})"
            }
        }

        return $t
    }
}
