<#

.SYNOPSIS
    Deletes one or more Global Lists.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.String
#>
Function Remove-TfsGlobalList
{
    [CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(ValueFromPipelineByPropertyName='Name')]
        [SupportsWildcards()]
        [string] 
        $Name = "*",
    
        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        $lists = Get-TfsGlobalList -Name $Name -Collection $Collection
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
