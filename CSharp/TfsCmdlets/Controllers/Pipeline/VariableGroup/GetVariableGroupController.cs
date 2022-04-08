using Microsoft.TeamFoundation.DistributedTask.WebApi;
using WebApiVariableGroup = Microsoft.TeamFoundation.DistributedTask.WebApi.VariableGroup;

namespace TfsCmdlets.Controllers.Pipeline.VariableGroup
{
    [CmdletController]
    partial class GetVariableGroupController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<TaskAgentHttpClient>();

            foreach (var input in VariableGroup)
            {
                var group = input switch
                {
                    null => throw new ArgumentException("Variable group name cannot be empty", "VariableGroup"),
                    string s when string.IsNullOrEmpty(s) => throw new ArgumentException("Variable group name cannot be empty", "VariableGroup"),
                    _ => input
                };

                switch (group)
                {
                    case WebApiVariableGroup vg:
                        {
                            yield return new Models.Pipeline.VariableGroup(vg, Project);
                            break;
                        }
                    case int i:
                        {
                            yield return new Models.Pipeline.VariableGroup(
                                client.GetVariableGroupAsync(Project.Name, i)
                                    .GetResult($"Error getting variable group '{i}'"),
                                Project);
                            break;
                        }
                    case string s:
                        {
                            foreach (var vg in client.GetVariableGroupsAsync(Project.Name, s, VariableGroupActionFilter.Manage)
                                .GetResult($"Error getting variable group(s) '{s}'")
                                .Where(g => g.Name.IsLike(s)))
                            {
                                // if (IncludeDetails)
                                // {
                                    yield return new Models.Pipeline.VariableGroup(
                                        client.GetVariableGroupAsync(Project.Name, vg.Id)
                                            .GetResult($"Error getting variable group '{vg.Id}'"),
                                        Project);
                                    // break;
                            //     }
                            //     yield return new Models.Pipeline.VariableGroup(vg, Project);
                            }
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent variable group '{group}'", "VariableGroup");
                        }
                }
            }
        }
    }
}