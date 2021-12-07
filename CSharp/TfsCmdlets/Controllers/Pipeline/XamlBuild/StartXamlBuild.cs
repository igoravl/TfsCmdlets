using System;

namespace TfsCmdlets.Controllers.Pipeline.XamlBuild
{
    /// <summary>
    /// Queues a XAML Build.
    /// </summary>
    [CmdletController]
    partial class StartXamlBuild
    {
        public override object InvokeCommand()
        {
            throw new NotImplementedException();
        }

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
