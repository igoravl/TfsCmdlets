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
