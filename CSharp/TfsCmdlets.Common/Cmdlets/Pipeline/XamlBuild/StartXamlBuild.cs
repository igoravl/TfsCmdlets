/*
.SYNOPSIS
Queues a XAML Build.

.PARAMETER BuildDefinition
Build Definition Name that you want to queue.

.PARAMETER Project
Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String

.EXAMPLE
Start-TfsBuild -BuildDefinition "My Build Definition" -Project "MyTeamProject"
This example queue a Build Definition "My Build Definition" of Team Project "MyTeamProject".
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.XamlBuild
{
    [Cmdlet(VerbsLifecycle.Start, "XamlBuild", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    public class StartXamlBuild : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, Position=0)]
        public object BuildDefinition,

                [Parameter(ValueFromPipeline=true, Mandatory=true)]
                [object]
                [ValidateNotNull()]
                [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])}) 
                Project,

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                [string]
                [ValidateSet("LatestOnQueue", "LatestOnBuild", "Custom")]
                GetOption = "LatestOnBuild",

                [Parameter()]
                public string GetVersion { get; set; }

                [Parameter()]
                public string DropLocation { get; set; }

                [Parameter()]
                public hashtable Parameters { get; set; }

            protected override void BeginProcessing()
            {
                if (PSVersionTable.PSEdition != "Desktop") { throw new Exception("This cmdlet requires does not work in PowerShell Core. It uses TFS Client Object Model, which only works in Windows PowerShell" })
            }

            protected override void ProcessRecord()
            {
                if(ShouldProcess(BuildDefinition, "Queue new build"))
                {
                    tp = Get-TfsTeamProject Project Collection
                    tpc = tp.Store.TeamProjectCollection

                    buildServer = tpc.GetService([type]"Microsoft.TeamFoundation.Build.Client.IBuildServer")

                    if (BuildDefinition is Microsoft.TeamFoundation.Build.Client.IBuildDefinition)
                    {
                        buildDef = BuildDefinition
                    }
                    else
                    {
                        buildDef = buildServer.GetBuildDefinition(tp.Name, BuildDefinition);
                    }

                    req = buildDef.CreateBuildRequest()
                    req.GetOption = [Microsoft.TeamFoundation.Build.Client.GetOption] GetOption;

                    if (GetOption = = "Custom")
                    {
                        req.CustomGetVersion = GetVersion
                    }

                    if (DropLocation)
                    {
                        req.DropLocation = DropLocation
                    }

                    buildServer.QueueBuild(req)
                }
            }
        }
        */
    }
}
