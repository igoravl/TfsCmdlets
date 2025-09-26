using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable IDE0130

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal static class Stubs
    {
        public static
            System.Threading.Tasks.Task<
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode>
            CreateOrUpdateClassificationNodeAsync(this TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client,
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode postedNode,
                string project,
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup structureGroup,
                string path = null, object userState = null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return null!;
        }

        public static
            System.Threading.Tasks.Task<
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode>
            GetClassificationNodeAsync(this TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, string project,
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup structureGroup,
                string path = null, int? depth = default(int?), object userState = null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return null!;
        }

        public static
            System.Threading.Tasks.Task<
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode>
            GetClassificationNodeAsync(this TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client,
                System.Guid project,
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup structureGroup,
                string path = null, int? depth = default(int?), object userState = null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return null!;
        }

        public static System.Threading.Tasks.Task DeleteClassificationNodeAsync(
            this TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, string project,
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup structureGroup,
            string path = null, int? reclassifyId = default(int?), object userState = null,
            System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return null!;
        }

        public static System.Threading.Tasks.Task DeleteClassificationNodeAsync(this TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, System.Guid project, Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup structureGroup, string path = null, int? reclassifyId = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return null!;
        }

        public static
            System.Threading.Tasks.Task<
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode>
            UpdateClassificationNodeAsync(this TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client,
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode postedNode,
                string project,
                Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup structureGroup,
                string path = null, object userState = null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return null!;
        }

        public static System.Threading.Tasks.Task<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode> UpdateClassificationNodeAsync(this TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode postedNode, System.Guid project, Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup structureGroup, string path = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return null!;
        }
    }
}