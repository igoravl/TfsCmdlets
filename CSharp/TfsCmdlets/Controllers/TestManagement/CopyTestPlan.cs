using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Cmdlets.TestManagement;
using System.Threading;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
// using Microsoft.TeamFoundation.TestManagement.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    [CmdletController(typeof(TestPlan))]
    partial class CopyTestPlanController
    {
        public override IEnumerable<TestPlan> Invoke()
        {
            var plan = GetItem();
            var destinationProject = Parameters.Get<string>(nameof(CopyTestPlan.Destination));
            var destTp = Data.GetItem<WebApiTeamProject>(new { Project = destinationProject });
            var tp = Data.GetProject(new { Project = plan.Project.Name });
            var newName = Parameters.Get<string>(nameof(CopyTestPlan.NewName), $"{plan.Name}{(tp.Name.Equals(destTp.Name, StringComparison.OrdinalIgnoreCase) ? $" (cloned {DateTime.Now.ToShortDateString()})" : string.Empty)}");
            var areaPath = Parameters.Get<string>(nameof(CopyTestPlan.AreaPath), destTp.Name);
            var iterationPath = Parameters.Get<string>(nameof(CopyTestPlan.IterationPath), destTp.Name);
            var deepClone = Parameters.Get<bool>(nameof(CopyTestPlan.DeepClone));
            var passthru = Parameters.Get<string>(nameof(CopyTestPlan.Passthru));
            var relatedLinkComment = Parameters.Get<string>(nameof(CopyTestPlan.RelatedLinkComment));
            var copyAllSuites = Parameters.Get<bool>(nameof(CopyTestPlan.Recurse));
            var copyAncestorHierarchy = Parameters.Get<bool>(nameof(CopyTestPlan.CopyAncestorHierarchy));
            var destinationWorkItemType = Parameters.Get<string>(nameof(CopyTestPlan.DestinationWorkItemType));
            var cloneRequirements = Parameters.Get<bool>(nameof(CopyTestPlan.CloneRequirements));

            var client = Data.GetClient<TestPlanHttpClient>();

            var cloneParams = new CloneTestPlanParams()
            {
                sourceTestPlan = new SourceTestPlanInfo()
                {
                    id = plan.Id
                },
                destinationTestPlan = new DestinationTestPlanCloneParams()
                {
                    Project = destTp.Name,
                    Name = newName,
                    AreaPath = areaPath,
                    Iteration = iterationPath
                },
                cloneOptions = new Microsoft.TeamFoundation.TestManagement.WebApi.CloneOptions()
                {
                    RelatedLinkComment = relatedLinkComment,
                    CopyAllSuites = copyAllSuites,
                    CopyAncestorHierarchy = copyAncestorHierarchy,
                    DestinationWorkItemType = destinationWorkItemType,
                    CloneRequirements = cloneRequirements,
                    OverrideParameters = new Dictionary<string, string>()
                    {
                        ["System.AreaPath"] = areaPath,
                        ["System.IterationPath"] = iterationPath
                    }
                }
            };

            var result = client.CloneTestPlanAsync(cloneParams, tp.Name, deepClone)
                .GetResult($"Error cloning test plan '{plan.Name}' to '{destTp.Name}'");

            var opInfo = result;

            do
            {
                Thread.Sleep(5000);
                opInfo = client.GetCloneInformationAsync(tp.Name, opInfo.cloneOperationResponse.opId)
                    .GetResult($"Error getting operation status");
            } while (opInfo.cloneOperationResponse.state.Equals("Queued") ||
                     opInfo.cloneOperationResponse.state.Equals("InProgress"));


            if (opInfo.cloneOperationResponse.state.Equals("Failed"))
            {
                throw new Exception($"Error cloning test plan {plan.Name}: {opInfo.cloneOperationResponse.message}");
            }

            yield return (passthru == "Original") ? plan : Data.GetItem<TestPlan>(new { Plan = opInfo.destinationTestPlan });
        }
    }
}