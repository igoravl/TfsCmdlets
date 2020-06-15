using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Gets one or more Work Item Areas from a given Team Project.
    /// </summary>
    /// <example>
    ///   <code>Get-TfsArea</code>
    ///   <para>Returns all area paths in the currently connected Team Project (as defined by a previous call to Connect-TfsTeamProject)</para>
    /// </example>
    /// <example>
    ///   <code>Get-TfsArea '\\**\\Support' -Project Tailspin</code>
    ///   <para>Performs a recursive search and returns all area paths named 'Support' that may exist in a team project called Tailspin</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "TfsArea")]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class GetArea : GetClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public override object Node { get; set; } = @"\**";

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Gets one or more Iterations from a given Team Project.
    /// </summary>
    /// <example>
    ///   <code>Get-TfsIteration</code>
    ///   <para>Returns all aiterations in the currently connected Team Project (as defined by a previous call to Connect-TfsTeamProject)</para>
    /// </example>
    /// <example>
    ///   <code>Get-TfsIteration '\\**\\Support' -Project Tailspin</code>
    ///   <para>Performs a recursive search and returns all iterations named 'Support' that may exist in a team project called Tailspin</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "TfsIteration")]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class GetIteration : GetClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public override object Node { get; set; } = @"\**";

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }

    /// <summary>
    /// Base implementation for Get-Area and Get-Iteration
    /// </summary>
    public abstract class GetClassificationNode : BaseCmdlet
    {
        /// <summary>
        /// Specifies the name and/or path of the node (area or iteration)
        /// </summary>
        public virtual object Node { get; set; } = @"\**";

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        [Parameter()]
        protected abstract TreeStructureGroup StructureGroup { get; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteItems<ClassificationNode>();
        }
    }

    [Exports(typeof(ClassificationNode))]
    internal partial class ClassificationNodeDataService : BaseDataService<ClassificationNode>
    {
        protected override IEnumerable<ClassificationNode> DoGetItems()
        {
            var node = GetParameter<object>(nameof(GetClassificationNode.Node));
            var structureGroup = GetParameter<TreeStructureGroup>("StructureGroup");
            var (_, tp) = this.GetCollectionAndProject();
            bool done = false;
            string path = null;

            while (!done) switch (node)
                {
                    case PSObject pso:
                        {
                            node = pso.BaseObject;
                            continue;
                        }
                    case WorkItemClassificationNode n:
                        {
                            yield return new ClassificationNode(n, tp.Name, null);
                            yield break;
                        }
                    case string s when s.Equals("\\") || s.Equals("/"):
                        {
                            path = "\\";
                            done = true;
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s) && s.IsWildcard():
                        {
                            path = NodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString(), true, false, true, false, true);
                            done = true;
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            path = NodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString(), false, false, true, false, false);
                            done = true;
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent node {node}");
                        }
                }

            var client = GetClient<WorkItemTrackingHttpClient>();
            int depth = 0;

            if (path.IsWildcard())
            {
                depth = 2;
                Logger.Log($"Preparing to recursively search for pattern '{path}'");

                var root = new ClassificationNode(client.GetClassificationNodeAsync(tp.Name, structureGroup, "\\", depth)
                        .GetResult($"Error retrieving {structureGroup} from path '{path}'"),
                    tp.Name, client);

                foreach (var n in root.GetChildren(path, true))
                {
                    yield return n;
                }

                yield break;
            }

            Logger.Log($"Getting {structureGroup} under path '{path}'");

            yield return new ClassificationNode(client.GetClassificationNodeAsync(tp.Name, structureGroup, path, depth)
                .GetResult($"Error retrieving {structureGroup} from path '{path}'"), tp.Name, null);

        }
    }
}