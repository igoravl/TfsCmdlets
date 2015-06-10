Function Get-TfsProcessTemplate
{
    Param
    (
        [Parameter(Position=0)]
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
        $templateHeaders = $processTemplateSvc.TemplateHeaders() | ? Name -Like $Name

        foreach($templateHeader in $templateHeaders)
        {
            $templateHeader | Add-Member Collection $tpc.DisplayName -PassThru
        }
    }
}

Function Import-TfsProcessTemplate
{
    Param
    (
        [Parameter(Position=0, Mandatory=$true)]
        [ValidateScript({Test-Path $_  -PathType Container})]
        [string]
        $SourcePath,

        [Parameter()]
        [ValidateSet("Visible")]
        [string]
        $State = "Visible",

        [Parameter(ValueFromPipeline=$true)]
        [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
        $Collection
    )

    Process
    {
        if (-Not (Test-Path (Join-Path $SourcePath "ProcessTemplate.xml")))
        {
            throw "Invalid path. Source path ""$SourcePath"" must be a directory and must contain a file named ProcessTemplate.xml."
        }

        $tpc = Get-TfsTeamProjectCollection $Collection
        $processTemplateSvc = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.IProcessTemplates")

        $tempFile = New-TemporaryFile
        $zipFile = "$tempFile.zip"
        Rename-Item $tempFile -NewName (Split-Path $zipFile -Leaf)

        Compress-Archive -Path $SourcePath -DestinationPath $zipFile -Force

        $ptFile = (Join-Path $SourcePath "ProcessTemplate.xml")
        $ptXml = [xml] (Get-Content $ptFile)

        $name = $ptXml.ProcessTemplate.metadata.name
        $description = $ptXml.ProcessTemplate.metadata.description
        $metadata = $ptXml.ProcessTemplate.metadata.OuterXml

        $processTemplateSvc.AddUpdateTemplate($name, $description, $metadata, $State, $tempFile);

        Remove-Item $zipFile
    }
}

Function Export-TfsProcessTemplate
{
    Param
    (
        [Parameter(Position=0)]
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