<#

.SYNOPSIS
    Exports a saved work item query to XML.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
#>
Function Export-TfsWorkItemQuery
{
    [CmdletBinding()]
    [OutputType([xml])]
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [string] 
        $Query = "*",

		[Parameter()]
		[string]
		$Folder,
    
		[Parameter()]
		[string]
		$Destination,
    
		[Parameter()]
		[string]
		$Encoding = "UTF-8",
    
        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if ($Destination -and (-not (Test-Path $Destination -PathType Container)))
        {
            throw "Invalid destination path $Destination"
        }

		$queries = Get-TfsWorkItemQuery -Query $Query -Folder $Folder -Project $Project -Collection $Collection
        
        if (-not $queries)
        {
            throw "Query path `"$Query`" is invalid or missing."
        }

		foreach($q in $queries)
		{
			$xml = [xml] @"
<?xml version="1.0" encoding="$Encoding"?>
<!-- Original Query Path: $($q.Path) -->
<WorkItemQuery Version="1">
  <TeamFoundationServer>$($q.Project.Store.TeamProjectCollection.Uri)</TeamFoundationServer>
  <TeamProject>$($q.Project.Name)</TeamProject>
  <Wiql><![CDATA[$($q.QueryText)]]></Wiql>
</WorkItemQuery>
"@
			if (-not $Destination)
			{
				$xml
			}
            else
            {
			    $queryPath = $q.Path.Substring($q.Path.IndexOf('/')+1)
                $fileName = Join-Path $Destination "$queryPath.wiql" 
                $filePath = Split-Path $fileName -Parent

                if (-not (Test-Path $filePath -PathType Container))
                {
                    New-Item $filePath -ItemType Directory -Force | Out-Null
                }

			    $xml.Save($fileName)
            }
		}
	}
}
