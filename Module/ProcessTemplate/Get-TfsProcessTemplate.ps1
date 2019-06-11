<#

.SYNOPSIS
    Gets information from one or more process templates.

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
	Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
#>
Function Get-TfsProcessTemplate
{
    [CmdletBinding()]
    [OutputType('Microsoft.TeamFoundation.Server.TemplateHeader')]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [string]
        $Name = "*",

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $processTemplateSvc = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.IProcessTemplates")
        $templateHeaders = $processTemplateSvc.TemplateHeaders() | Where-Object Name -Like $Name

        foreach($templateHeader in $templateHeaders)
        {
            $templateHeader | Add-Member Collection $tpc.DisplayName -PassThru
        }
    }
}
