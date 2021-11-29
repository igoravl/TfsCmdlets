using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController]
    internal class NewTeamProject : ControllerBase<WebApiTeamProject>
    {
        public override IEnumerable<WebApiTeamProject> Invoke(ParameterDictionary parameters)
        {
           var tpc = Data.GetCollection(parameters);
           var project = parameters.Get<string>(nameof(Cmdlets.TeamProject.NewTeamProject.Project));

           if (!PowerShell.ShouldProcess(tpc, $"Create team project '{project}'"))
           {
               return null;
           }

           var processTemplate = parameters.Get<object>(nameof(Cmdlets.TeamProject.NewTeamProject.ProcessTemplate));
           processTemplate ??= Data.GetItem<Process>(parameters.Override(new { ProcessTemplate = "*", Default = true }));

           var description = parameters.Get<string>(nameof(Cmdlets.TeamProject.NewTeamProject.Description));
           var sourceControl = parameters.Get<string>(nameof(Cmdlets.TeamProject.NewTeamProject.SourceControl));

           var template = processTemplate switch
           {
               Process p => p,
               string s => Data.GetItem<Process>(parameters.Override(new { Process = s })),
               _ => throw new ArgumentException($"Invalid or non-existent process template '{processTemplate}'")
           };

           var client = Data.GetClient<ProjectHttpClient>(parameters);

           var tpInfo = new WebApiTeamProject
           {
               Name = project,
               Description = description,
               Capabilities = new Dictionary<string, Dictionary<string, string>>
               {
                   ["versioncontrol"] = new Dictionary<string, string>
                   {
                       ["sourceControlType"] = sourceControl
                   },
                   ["processTemplate"] = new Dictionary<string, string>
                   {
                       ["templateTypeId"] = template.Id.ToString()
                   }
               }
           };

           // Trigger the project creation

           var token = client.QueueCreateProject(tpInfo)
               .GetResult("Error queueing project creation");

           // Wait for the operation to complete

           var opsClient = Data.GetClient<OperationsHttpClient>(parameters);
           var opsToken = opsClient.GetOperation(token.Id)
               .GetResult("Error getting operation status");

           while (
               (opsToken.Status != OperationStatus.Succeeded) &&
               (opsToken.Status != OperationStatus.Failed) &&
               (opsToken.Status != OperationStatus.Cancelled))
           {
               Thread.Sleep(2);
               opsToken = opsClient.GetOperation(token.Id)
                   .GetResult("Error getting operation status");
           }

           if (opsToken.Status != OperationStatus.Succeeded)
           {
               throw new Exception($"Error creating team project {project}: {opsToken.ResultMessage}");
           }

           return GetItems(parameters);
        }

        [ImportingConstructor]
         public NewTeamProject(IPowerShellService powerShell, IDataManager data, ILogger logger) : base(powerShell, data, logger)
        {
        }
   }
}