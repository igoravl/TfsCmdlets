// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Management.Automation;
// using Microsoft.TeamFoundation.Server;
// using TfsCmdlets.Extensions;
// using TfsCmdlets.Services;

// namespace TfsCmdlets.Cmdlets.ProcessTemplate
// {
//     internal class GetProcessTemplate_Legacy
//     {
    //     /// <summary>
    //     /// Performs execution of the command
    //     /// </summary>
    //     protected override void DoProcessRecord()
    //     {
    //         WriteItems<TemplateHeader>();
    //     }
    // }

    // [Exports(typeof(TemplateHeader))]
    // internal class ProcessTemplateDataService : BaseDataService<TemplateHeader>
    // {
    //     protected override IEnumerable<TemplateHeader> DoGetItems()
    //     {
    //         var process = GetParameter<object>("ProcessTemplate");

    //         while(true) switch(process)
    //         {
    //             case PSObject pso:
    //             {
    //                 process = pso.BaseObject;

    //                 continue;
    //             }
    //             case TemplateHeader th:
    //             {
    //                 yield return th;

    //                 yield break;
    //             }
    //             case string s:
    //             {
    //                 var tpc = GetCollection();
    //                 var processTemplateSvc = tpc.GetService<Microsoft.TeamFoundation.Server.IProcessTemplates>();

    //                 foreach(var th in processTemplateSvc.TemplateHeaders().Where(th => th.Name.IsLike(s)))
    //                 {
    //                     yield return th;
    //                 }

    //                 yield break;
    //             }
    //             default:
    //             {
    //                 throw new ArgumentException($"Invalid or non-existent process template '{process}'");
    //             }
    //         }
    //     }
//     }
// }