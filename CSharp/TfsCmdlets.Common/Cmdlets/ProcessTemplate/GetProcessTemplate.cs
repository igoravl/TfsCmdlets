/*

.SYNOPSIS
    Gets information from one or more process templates.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
	Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [Cmdlet(VerbsCommon.Get, "ProcessTemplate")]
    //[OutputType(typeof(Microsoft.TeamFoundation.Server.TemplateHeader))]
    public class GetProcessTemplate: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string ProcessTemplate { get; set; } = "*",

        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection Collection
        processTemplateSvc = tpc.GetService([type]"Microsoft.TeamFoundation.Server.IProcessTemplates")
        templateHeaders = processTemplateSvc.TemplateHeaders() | Where-Object Name -Like ProcessTemplate

        foreach(templateHeader in templateHeaders)
        {
            templateHeader | Add-Member Collection tpc.DisplayName -PassThru
        }
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
