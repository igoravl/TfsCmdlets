<#

.SYNOPSIS
    Queues a XAML Build.

.PARAMETER BuildDefinition
    Build Definition Name that you want to queue.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.EXAMPLE
    Start-TfsBuild -BuildDefinition "My Build Definition" -Project "MyTeamProject"
    This example queue a Build Definition "My Build Definition" of Team Project "MyTeamProject".

#>
Function Start-TfsXamlBuild
{
    [CmdletBinding(ConfirmImpact='Medium')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0)]
        [object] 
        $BuildDefinition,

        [Parameter(ValueFromPipeline=$true, Mandatory=$true)]
        [object]
        [ValidateNotNull()]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [string]
        [ValidateSet("LatestOnQueue", "LatestOnBuild", "Custom")]
        $GetOption = "LatestOnBuild",

        [Parameter()]
        [string]
        $GetVersion,

        [Parameter()]
        [string]
        $DropLocation,

        [Parameter()]
        [hashtable]
        $Parameters
    )

    Process
    {

        $tp = Get-TfsTeamProject $Project $Collection
        $tpc = $tp.Store.TeamProjectCollection

        $buildServer = $tpc.GetService([type]"Microsoft.TeamFoundation.Build.Client.IBuildServer")

        if ($BuildDefinition -is [Microsoft.TeamFoundation.Build.Client.IBuildDefinition])
        {
            $buildDef = $BuildDefinition
        }
        else
        {
            $buildDef = $buildServer.GetBuildDefinition($tp.Name, $BuildDefinition);
        }

        $req = $buildDef.CreateBuildRequest()
        $req.GetOption = [Microsoft.TeamFoundation.Build.Client.GetOption] $GetOption;

        if ($GetOption -eq "Custom")
        {
            $req.CustomGetVersion = $GetVersion
        }

        if ($DropLocation)
        {
            $req.DropLocation = $DropLocation
        }

        $buildServer.QueueBuild($req)
    }
}
