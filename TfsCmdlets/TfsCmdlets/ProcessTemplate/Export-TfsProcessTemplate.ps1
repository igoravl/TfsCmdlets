<#

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Export-TfsProcessTemplate
{
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
        [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
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
            $templates = $templates | select -First 1
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
