/*
.SYNOPSIS
Exports a process template definition to disk.

.DESCRIPTION
This cmdlet offers a functional replacement to the "Export Process Template" feature found in Team Explorer. All files pertaining to the specified process template (work item defininitons, reports, saved queries, process configuration and so on) are downloaded from the given Team Project Collection and saved in a local directory, preserving the directory structure required to later re-import it. This is specially handy to do small changes to a process template or to create a new process template based on an existing one.

.PARAMETER Process
Name of the process template to be exported. Wildcards supported.

.PARAMETER DestinationPath
Path to the target directory where the exported process template (and related files) will be saved.

.PARAMETER NewName
Saves the exported process template with a new name. Useful when exporting a base template which will be used as a basis for a new process template.

.PARAMETER NewDescription
Saves the exported process template with a new description. Useful when exporting a base template which will be used as a basis for a new process template.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.EXAMPLE
Export-TfsProcessTemplate -Process "Scrum" -DestinationPath C:\PT -Collection http://vsalm:8080/tfs/DefaultCollection
Exports the Scrum process template from the DefaultCollection project collection in the VSALM server, saving the template files to the C:\PT\Scrum directory in the local computer.

.EXAMPLE
Export-TfsProcessTemplate -Process "Scrum" -DestinationPath C:\PT -Collection http://vsalm:8080/tfs/DefaultCollection -NewName "MyScrum" -NewDescription "A customized version of the Scrum process template"
Exports the Scrum process template from the DefaultCollection project collection in the VSALM server, saving the template files to the C:\PT\MyScrum directory in the local computer. Notice that the process template is being renamed from Scrum to MyScrum, so that it can be later reimported as a new process template instead of overwriting the original one.


.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [Cmdlet(VerbsData.Export, "ProcessTemplate")]
    public class ExportProcessTemplate: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [SupportsWildcards()]
        public object Process { get; set; } = "*";

        [Parameter(Mandatory=true)]
        public string DestinationPath { get; set; }

        [Parameter()]
        [ValidateNotNullOrEmpty()]
        public string NewName { get; set; }

        [Parameter()]
        [ValidateNotNullOrEmpty()]
        public string NewDescription { get; set; }

        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection Collection
        processTemplateSvc = tpc.GetService([type]"Microsoft.TeamFoundation.Server.IProcessTemplates")

        if (Process is Microsoft.TeamFoundation.Server.TemplateHeader)
        {
            templates = @(Process)
        }
        else
        {
            templates = Get-TfsProcessTemplate Process -Collection Collection
        }

        if (NewName || NewDescription)
        {
            templates = templates | Select-Object -First 1
        }

        foreach(template in templates)
        {
            if (NewName)
            {
                templateName = NewName
            }
            else
            {
                templateName = template.Name
            }

            tempFile = processTemplateSvc.GetTemplateData(template.TemplateId)
            zipFile = $"{tempFile}.zip"
            Rename-Item -Path tempFile -NewName (Split-Path zipFile -Leaf)

            outDir = Join-Path DestinationPath templateName
            New-Item outDir -ItemType Directory -Force | Out-Null

            Expand-Archive -Path zipFile -DestinationPath outDir

            if (NewName || NewDescription)
            {
                ptFile = (Join-Path outDir "ProcessTemplate.xml")
                ptXml = [xml] (Get-Content ptFile)

                if (NewName)
                {
                    ptXml.ProcessTemplate.metadata.name = NewName
                }

                if (NewDescription)
                {
                    ptXml.ProcessTemplate.metadata.description = NewDescription
                }

                ptXml.Save(ptFile)
            }

            Remove-Item zipFile
        }
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
