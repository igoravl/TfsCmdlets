using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.XamlBuild
{
    /// <summary>
    /// Queues a XAML Build.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TfsXamlBuild", SupportsShouldProcess = true)]
    public class StartXamlBuild : CmdletBase
    {
        // TODO

        //         [Parameter(Mandatory=true, Position=0)]
        // public object BuildDefinition,

        //         [Parameter(ValueFromPipeline=true, Mandatory=true)]
        //         [object]
        //         [ValidateNotNull()]
        //         [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])}) 
        //         Project,

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         [Parameter()]
        //         [string]
        //         [ValidateSet("LatestOnQueue", "LatestOnBuild", "Custom")]
        //         GetOption = "LatestOnBuild",

        //         [Parameter()]
        //         public string GetVersion { get; set; }

        //         [Parameter()]
        //         public string DropLocation { get; set; }

        //         [Parameter()]
        //         public hashtable Parameters { get; set; }

        //     protected override void BeginProcessing()
        //     {
        //         if (PSVersionTable.PSEdition != "Desktop") { throw new Exception("This cmdlet requires does not work in PowerShell Core. It uses TFS Client Object Model, which only works in Windows PowerShell" })
        //     }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void DoProcessRecord()
        //     {
        //         if(ShouldProcess(BuildDefinition, "Queue new build"))
        //         {
        //             tp = Get-TfsTeamProject Project Collection
        //             tpc = tp.Store.TeamProjectCollection

        //             buildServer = tpc.GetService([type]"Microsoft.TeamFoundation.Build.Client.IBuildServer")

        //             if (BuildDefinition is Microsoft.TeamFoundation.Build.Client.IBuildDefinition)
        //             {
        //                 buildDef = BuildDefinition
        //             }
        //             else
        //             {
        //                 buildDef = buildServer.GetBuildDefinition(tp.Name, BuildDefinition);
        //             }

        //             req = buildDef.CreateBuildRequest()
        //             req.GetOption = [Microsoft.TeamFoundation.Build.Client.GetOption] GetOption;

        //             if (GetOption = = "Custom")
        //             {
        //                 req.CustomGetVersion = GetVersion
        //             }

        //             if (DropLocation)
        //             {
        //                 req.DropLocation = DropLocation
        //             }

        //             buildServer.QueueBuild(req)
        //         }
        //     }
        // }
    }
}
