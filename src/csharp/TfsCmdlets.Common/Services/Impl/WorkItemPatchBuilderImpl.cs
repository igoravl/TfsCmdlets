using System.Reflection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IWorkItemPatchBuilder))]
    public class WorkItemPatchBuilderImpl : IWorkItemPatchBuilder
    {
        private IParameterManager Parameters { get; }
        private IDataManager Data { get; }
        private INodeUtil NodeUtil { get; }
        private IPowerShellService PowerShell { get; }

        public JsonPatchDocument GetJson(WebApiWorkItem wi)
        {
            var projectName = (string)wi.Fields["System.TeamProject"];
            var workItemType = (string)wi.Fields["System.WorkItemType"];

            var patch = new JsonPatchDocument();

            if (wi.Rev > 0)
            {
                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Test,
                    Path = "/rev",
                    Value = wi.Rev
                });
            }

            patch.AddRange(ParseCmdlet(PowerShell.CurrentCmdlet, projectName, workItemType));

            var fields = Parameters.Get<Hashtable>("Fields").ToDictionary<string, object>();

            if (fields != null && fields.Count > 0)
            {
                var wit = Data.GetItem<WebApiWorkItemType>(new { Type = wi.Fields["System.WorkItemType"], WorkItem = 0, Project = projectName });
                patch.AddRange(ParseFields(fields, wit, projectName, workItemType));
            }

            return patch;
        }

        private IEnumerable<JsonPatchOperation> ParseCmdlet(Cmdlet cmdlet, string projectName, string workItemType)
        {
            var properties = cmdlet.GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<WorkItemFieldAttribute>() != null && Parameters.HasParameter(p.Name));

            foreach (var pi in properties)
            {
                var attr = pi.GetCustomAttribute<WorkItemFieldAttribute>();

                var fieldName = attr.Name;
                var fieldType = attr.Type;
                var value = ParseValue(Parameters.Get<object>(pi.Name), fieldName, fieldType, projectName, workItemType);

                yield return CreateOperation(fieldName, value);
            }
        }

        private IEnumerable<JsonPatchOperation> ParseFields(IDictionary<string, object> fields, WebApiWorkItemType wit, string projectName, string workItemType)
        {
            var fieldDefinitions = wit.Fields.Distinct(new WorkItemTypeFieldInstanceComparer()).ToDictionary(f => f.ReferenceName, f => f);

            foreach (var field in fields)
            {
                var fieldName = field.Key;
                var fieldDef = fieldDefinitions[fieldName];
                var value = ParseValue(field.Value, fieldDef, projectName, workItemType);

                yield return CreateOperation(fieldName, value);
            }
        }

        private object ParseValue(object value, string fieldName, FieldType fieldType, string projectName, string workItemType)
        {
            switch (fieldType)
            {
                case FieldType.TreePath:
                    return NodeUtil.NormalizeNodePath((string)value, projectName, includeTeamProject: true, includeLeadingSeparator: true);
                case FieldType.Identity:
                    if (value is string s && s.Equals(string.Empty)) return null;
                    var identity = Data.GetItem<Models.Identity>(new { Identity = value });
                    return identity.DisplayName;
                case FieldType.PlainText when value is ICollection<string> enumerable && enumerable.Count == 0:
                    return null;
                case FieldType.PlainText when value is IEnumerable<string> enumerable:
                    return string.Join(";", enumerable);
                case { } when fieldName.Equals("System.BoardColumn"):
                case { } when fieldName.Equals("System.BoardColumnDone"):
                case { } when fieldName.Equals("System.BoardLane"):
                    return GetBoardValue(value, fieldName, workItemType);
            }

            return value;
        }

        private object ParseValue(object value, WorkItemTypeFieldInstance fieldRef, string projectName, string workItemType)
        {
            if (fieldRef.IsIdentity)
            {
                if (value is string s && s.Equals(string.Empty)) return null;
                var identity = Data.GetItem<Models.Identity>(new { Identity = value });
                return identity.DisplayName;
            }

            return fieldRef.ReferenceName switch
            {
                "System.AreaPath" or "System.IterationPath" => NodeUtil.NormalizeNodePath((string)value, projectName, includeTeamProject: true, includeLeadingSeparator: true),
                "System.Tags" when value is ICollection<string> enumerable && enumerable.Count == 0 => null,
                "System.Tags" when value is IEnumerable<string> enumerable => string.Join(";", enumerable),
                "System.BoardColumn" or "System.BoardColumnDone" or "System.BoardLane" => GetBoardValue(value, fieldRef.ReferenceName, workItemType),
                _ => value,
            };
        }

        private JsonPatchOperation CreateOperation(string fieldName, object value)
        {
            if (value is JsonPatchOperation jsonOp)
            {
                return jsonOp;
            }

            var op = value == null ? Operation.Remove : Operation.Add;

            return new JsonPatchOperation()
            {
                Operation = op,
                Path = $"/fields/{fieldName}",
                Value = value
            };
        }

        private JsonPatchOperation GetBoardValue(object value, string referenceName, string workItemType)
        {
            var board = FindBoard(workItemType);

            var boardFieldName = referenceName switch
            {
                "System.BoardColumn" => board.Fields.ColumnField.ReferenceName,
                "System.BoardColumnDone" => board.Fields.DoneField.ReferenceName,
                "System.BoardLane" => board.Fields.RowField.ReferenceName,
                _ => throw new InvalidOperationException($"Unknown board field reference name: {referenceName}")
            };

            return new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = $"/fields/{boardFieldName}",
                Value = value
            };
        }

        private WebApiBoard FindBoard(string workItemType)
        {
            var boards = Data.GetItems<Models.Board>(new { Board = "*" });

            foreach (var b in boards)
            {
                var keys = b.InnerObject.AllowedMappings.Values.SelectMany(o => o.Keys).ToList();

                if (keys.Any(t => t.Equals(workItemType, StringComparison.OrdinalIgnoreCase)))
                {
                    return b.InnerObject;
                }
            }

            throw new Exception("Unable to find a board belonging to team " +
                $"'{Data.GetTeam().Name}' that contains a mapping to the work item type '{workItemType}'");
        }

        private class WorkItemTypeFieldInstanceComparer : IEqualityComparer<WorkItemTypeFieldInstance>
        {
            public bool Equals(WorkItemTypeFieldInstance x, WorkItemTypeFieldInstance y)
            {
                return x.ReferenceName == y.ReferenceName;
            }

            public int GetHashCode(WorkItemTypeFieldInstance obj)
            {
                return obj.ReferenceName.GetHashCode();
            }
        }

        [ImportingConstructor]
        public WorkItemPatchBuilderImpl(IParameterManager parameters, IDataManager dataManager, INodeUtil nodeUtil, IPowerShellService powerShell)
        {
            Parameters = parameters;
            Data = dataManager;
            NodeUtil = nodeUtil;
            PowerShell = powerShell;
        }
    }
}