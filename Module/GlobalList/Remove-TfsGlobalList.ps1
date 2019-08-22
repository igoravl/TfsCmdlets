<#

.SYNOPSIS
    Deletes one or more Global Lists.

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    System.String
#>
Function Remove-TfsGlobalList
{
    [CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(ValueFromPipelineByPropertyName='Name')]
        [Alias('Name')]
        [SupportsWildcards()]
        [string] 
        $GlobalList = "*",
    
        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.Client)
    }
    
    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        $lists = Get-TfsGlobalList -Name $GlobalList -Collection $Collection
        $listsToRemove = @()

        foreach($list in $lists)
        {
            if ($PSCmdlet.ShouldProcess($list.Name, "Remove global list"))
            {
                $listsToRemove += $list
            }
        }

        if ($listsToRemove.Length -eq 0)
        {
            return
        }

        $xml = [xml] "<Package />"

        foreach($list in $listsToRemove)
        {
            $elem = $xml.CreateElement("DestroyGlobalList");
            $elem.SetAttribute("ListName", "*" + $list.Name);
            $elem.SetAttribute("ForceDelete", "true");
            [void]$xml.DocumentElement.AppendChild($elem);
        }

        $returnElem = $null

        $store.SendUpdatePackage($xml.DocumentElement, [ref] $returnElem, $false)
    }
}
