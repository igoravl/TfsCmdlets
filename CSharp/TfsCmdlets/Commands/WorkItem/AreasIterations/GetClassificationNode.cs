﻿using System;
using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Services.Impl;
using TfsCmdlets.Util;

namespace TfsCmdlets.Commands.WorkItem.AreasIterations
{
    [Command]
    internal class GetClassificationNode : CommandBase<ClassificationNode>
    {
        public INodeUtil NodeUtil { get; }

        public override IEnumerable<ClassificationNode> Invoke(ParameterDictionary parameters)
        {
            var node = parameters.Get<object>("Node");
            var structureGroup = parameters.Get<TreeStructureGroup>("StructureGroup");
            var tp = Data.GetProject(parameters);
            var done = false;
            string path = null;

            while (!done) switch (node)
            {
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
                    path = NodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString().TrimEnd('s'), true, false, true, false, true);
                    done = true;
                    break;
                }
                case string s when !string.IsNullOrEmpty(s):
                {
                    path = NodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString().TrimEnd('s'), false, false, true, false, false);
                    done = true;
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid or non-existent node {node}");
                }
            }

            var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);
            var depth = 1;

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

        [ImportingConstructor]
        public GetClassificationNode(INodeUtil nodeUtil, IPowerShellService powerShell, IDataManager data, ILogger logger)
                : base(powerShell, data, logger)
        {
            NodeUtil = nodeUtil;
        }
    }
}
