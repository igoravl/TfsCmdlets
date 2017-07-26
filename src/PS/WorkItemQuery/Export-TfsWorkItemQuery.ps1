<#
.SYNOPSIS
Exports a saved work item query to XML.

.DESCRIPTION
Work item queries can be exported to XML files (.WIQ extension) in order to be shared and reused. Visual Studio Team Explorer has the ability to open and save WIQ files. Use this cmdlet to generate WIQ files compatible with the format supported by Team Explorer.

.PARAMETER Query
Name of the work item query to be exported. Wildcards are supported.

.PARAMETER Folder
Full path of the folder containing the query(ies) to be exported. Wildcards are supported.

.PARAMETER Destination
Path to the target directory where the exported work item query (WIQL file) will be saved. The original folder structure (as defined in TFS/VSTS) will be preserved.

.PARAMETER Encoding
XML encoding of the generated WIQL files. If omitted, defaults to UTF-8.

.PARAMETER Collection
${HelpParam_Project}

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

.EXAMPLE
Export-TfsWorkItemQuery 
.LINK
https://www.visualstudio.com/en-us/docs/work/reference/process-templates/define-work-item-query-process-template

.NOTES
For queries made against Team Services, the WIQL length must not exceed 32K characters. The system won't allow you to create or run queries that exceed that length.
#>
Function Export-TfsWorkItemQuery
{
    [CmdletBinding(DefaultParameterSetName='Export to output stream', SupportsShouldProcess=$true)]
    [OutputType([xml])]
    Param
    (
        [Parameter(ValueFromPipeline=$true, Position=0)]
        [SupportsWildcards()]
        [string] 
        $Query = "**/*",

        [Parameter()]
        [ValidateSet('Personal', 'Shared', 'Both')]
        [string]
        $Scope = 'Both',
    
		[Parameter(ParameterSetName="Export to file", Mandatory=$true)]
		[string]
		$Destination,
    
		[Parameter(ParameterSetName="Export to file")]
		[string]
		$Encoding = "UTF-8",
    
		[Parameter(ParameterSetName="Export to file")]
		[switch]
		$FlattenFolders,
    
		[Parameter(ParameterSetName="Export to file")]
		[switch]
		$Force,
    
		[Parameter(ParameterSetName="Export to output stream")]
		[switch]
		$AsXml,
    
        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if($PSCmdlet.ParameterSetName -eq 'Export to output stream')
        {
            $Encoding = 'UTF-16'
        }
        else
        {
            if (-not (Test-Path $Destination -PathType Container))
            {
                Write-Verbose "Destination path '$Destination' not found."

                if ($Force)
                {
                    Write-Verbose "-Force switch specified. Creating output directory."

                    if($PSCmdlet.ShouldProcess($Destination, 'Create output directory'))
                    {
                        New-Item $Destination -ItemType Directory | Write-Verbose
                    }
                }
                else
                {
                    throw "Invalid output path $Destination"
                }
            }
        }

		$queries = Get-TfsWorkItemQuery -Query $Query -Scope $Scope -Project $Project -Collection $Collection
        
        if (-not $queries)
        {
            throw "No work item queries match the specified `"$Query`" pattern supplied."
        }

		foreach($q in $queries)
		{
			$xml = @"
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
                if ($AsXml)
                {
    				Write-Output [xml]$xml
                }
                else 
                {
    				Write-Output $xml
                }
                continue
			}

            if ($FlattenFolders)
            {
                $queryPath = $q.Name
            }
            else
            {
                $queryPath = $q.Path.Substring($q.Path.IndexOf('/')+1)
            }

            $fileName = Join-Path $Destination "$queryPath.wiql" 
            $filePath = Split-Path $fileName -Parent

            Write-Verbose "Exporting query $($q.Name) to path '$fileName'"

            if (-not (Test-Path $filePath -PathType Container))
            {
                if($PSCmdlet.ShouldProcess($filePath, "Create folder '$(Split-Path $filePath -Leaf)'"))
                {
                    New-Item $filePath -ItemType Directory -Force | Write-Verbose
                }
            }

            if((Test-Path $fileName) -and (-not $Force))
            {
                throw "File $fileName already exists. To overwrite an existing file, use the -Force switch"
            }

            if($PSCmdlet.ShouldProcess($fileName, "Save query '$($q.Name)'"))
            {
                $xml.Save($fileName)
            }
		}
	}
}
