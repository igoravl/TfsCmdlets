/*

.SYNOPSIS
    Imports a process template definition from disk.
    
.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [Cmdlet(VerbsData.Import, "ProcessTemplate", ConfirmImpact = ConfirmImpact.Medium)]
    public class ImportProcessTemplate : BaseCmdlet
    {
        /*
                [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
                [ValidateScript({Test-Path _  -PathType Container})]
                public string SourcePath { get; set; }

                [Parameter()]
                [ValidateSet("Visible")]
                public string State { get; set; } = "Visible",

                [Parameter()]
                public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                if (! (Test-Path (Join-Path SourcePath "ProcessTemplate.xml")))
                {
                    throw new Exception($"Invalid path. Source path ""{SourcePath}"" must be a directory and must contain a file named ProcessTemplate.xml.")
                }

                tpc = Get-TfsTeamProjectCollection Collection
                processTemplateSvc = tpc.GetService([type]"Microsoft.TeamFoundation.Server.IProcessTemplates")

                tempFile = New-TemporaryFile
                zipFile = $"{tempFile}.zip"
                Rename-Item tempFile -NewName (Split-Path zipFile -Leaf)

                Compress-Archive -Path $"{SourcePath}/**" -DestinationPath zipFile -Force

                ptFile = (Join-Path SourcePath "ProcessTemplate.xml")
                ptXml = [xml] (Get-Content ptFile)

                name = ptXml.ProcessTemplate.metadata.name
                description = ptXml.ProcessTemplate.metadata.description
                metadata = ptXml.ProcessTemplate.metadata.OuterXml

                processTemplateSvc.AddUpdateTemplate(name, description, metadata, State, zipFile);

                Remove-Item zipFile
            }
        }
        */
    }
}
