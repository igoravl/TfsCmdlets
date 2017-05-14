<#

.SYNOPSIS
    Imports a process template definition from disk.
    
.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.String
#>
Function Import-TfsProcessTemplate
{
    [CmdletBinding(ConfirmImpact='Medium')]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
        [ValidateScript({Test-Path $_  -PathType Container})]
        [string]
        $SourcePath,

        [Parameter()]
        [ValidateSet("Visible")]
        [string]
        $State = "Visible",

        [Parameter()]
        [object]
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

        Compress-Archive -Path "$SourcePath/**" -DestinationPath $zipFile -Force

        $ptFile = (Join-Path $SourcePath "ProcessTemplate.xml")
        $ptXml = [xml] (Get-Content $ptFile)

        $name = $ptXml.ProcessTemplate.metadata.name
        $description = $ptXml.ProcessTemplate.metadata.description
        $metadata = $ptXml.ProcessTemplate.metadata.OuterXml

        $processTemplateSvc.AddUpdateTemplate($name, $description, $metadata, $State, $zipFile);

        Remove-Item $zipFile
    }
}
