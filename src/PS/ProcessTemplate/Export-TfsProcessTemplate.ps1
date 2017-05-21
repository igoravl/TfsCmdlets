<#
.SYNOPSIS
Exports a process template definition to disk.

.PARAMETER Process
Name of the process template to be exported. Wildcards supported.

.PARAMETER DestinationPath
Name of the target directory where the exported process template (and related files) will be saved.

.PARAMETER NewName
Saves the exported process template with a new name. Useful when exporting a base template which will be used as a basis for a new process template.

.PARAMETER NewDescription
Saves the exported process template with a new description. Useful when exporting a base template which will be used as a basis for a new process template.

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri
#>
Function Export-TfsProcessTemplate
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [object]
        $Process = "*",

        [Parameter(Mandatory=$true)]
        [string]
        $DestinationPath,

        [Parameter()]
        [ValidateNotNullOrEmpty()]
        [string]
        $NewName,

        [Parameter()]
        [ValidateNotNullOrEmpty()]
        [string]
        $NewDescription,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $processTemplateSvc = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.IProcessTemplates")

        if ($Process -is [Microsoft.TeamFoundation.Server.TemplateHeader])
        {
            $templates = @($Process)
        }
        else
        {
            $templates = Get-TfsProcessTemplate $Process -Collection $Collection
        }

        if ($NewName -or $NewDescription)
        {
            $templates = $templates | Select-Object -First 1
        }

        foreach($template in $templates)
        {
            if ($NewName)
            {
                $templateName = $NewName
            }
            else
            {
                $templateName = $template.Name
            }

            $tempFile = $processTemplateSvc.GetTemplateData($template.TemplateId)
            $zipFile = "$tempFile.zip"
            Rename-Item -Path $tempFile -NewName (Split-Path $zipFile -Leaf)

            $outDir = Join-Path $DestinationPath $templateName
            New-Item $outDir -ItemType Directory -Force | Out-Null

            Expand-Archive -Path $zipFile -DestinationPath $outDir

            if ($NewName -or $NewDescription)
            {
                $ptFile = (Join-Path $outDir "ProcessTemplate.xml")
                $ptXml = [xml] (Get-Content $ptFile)

                if ($NewName)
                {
                    $ptXml.ProcessTemplate.metadata.name = $NewName
                }

                if ($NewDescription)
                {
                    $ptXml.ProcessTemplate.metadata.description = $NewDescription
                }

                $ptXml.Save($ptFile)
            }

            Remove-Item $zipFile
        }
    }
}
