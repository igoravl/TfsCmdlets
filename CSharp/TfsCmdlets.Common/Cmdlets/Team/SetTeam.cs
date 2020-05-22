/*
.SYNOPSIS
Changes the details of a team.

.PARAMETER Project
Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet(VerbsCommon.Set, "Team", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    //[OutputType(typeof(Microsoft.TeamFoundation.Client.TeamFoundationTeam))]
    public class SetTeam : BaseCmdlet
    {
        /*
                [Parameter(Position=0, ValueFromPipeline=true)]
                [Alias("Name")]
                [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.Core.WebApi.WebApiTeam])}) 
                [SupportsWildcards()]
                public object Team { get; set; } = "*",

                [Parameter()]
                public SwitchParameter Default { get; set; }

                [Parameter()]
                public string NewName { get; set; }

                [Parameter()]
                public string Description { get; set; }

                [Parameter()]
                [Alias("TeamFieldValue")]
                public string DefaultAreaPath { get; set; }

                [Parameter()]
                public hashtable AreaPaths { get; set; }

                [Parameter()]
                public string BacklogIteration { get; set; }

                [Parameter()]
                public object IterationPaths { get; set; }

                # Default iteration macro
                [Parameter()]
                public string DefaultIterationMacro { get; set; } #= "@CurrentIteration"

                # Working Days. Defaults to Monday thru Friday
                [Parameter()]
                [string[]]
                WorkingDays, #= @("monday", "tuesday", "wednesday", "thursday", "friday"),

                # Bugs behavior
                [Parameter()]
                [ValidateSet("AsTasks", "AsRequirements", "Off")]
                public string BugsBehavior { get; set; }

                [Parameter()]
                public hashtable BacklogVisibilities { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Work.WebApi"
            }

            protected override void ProcessRecord()
            {
                t = Get-TfsTeam -Team Team -Project Project -Collection Collection

                if (Project)
                {
                    tp = Get-TfsTeamProject -Project Project -Collection Collection
                    tpc = tp.Store.TeamProjectCollection
                }
                else
                {
                    tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
                }

                teamService = tpc.GetService([type]"Microsoft.TeamFoundation.Client.TfsTeamService")

                if (NewName && ShouldProcess(Team, $"Rename team to "{NewName}""))
                {
                    isDirty = true
                    t.Name = NewName
                }

                if (PSBoundParameters.ContainsKey("Description") && ShouldProcess(Team, $"Set team"s description to "{Description}""))
                {
                    isDirty = true
                    t.Description = Description
                }

                if (Default && ShouldProcess(Team, "Set team to project"s default team"))
                {
                    teamService.SetDefaultTeam(t)
                }

                if(isDirty)
                {
                    teamService.UpdateTeam(t)
                }

                # Prepare for the second stage

                client = Get-TfsRestClient "Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient" -Collection tpc
                ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(@(tp.Name, t.Name))

                # Set Team Field and Area Path settings

                patch = new Microsoft.TeamFoundation.Work.WebApi.TeamFieldValuesPatch()

                if(DefaultAreaPath && ShouldProcess(Team, $"Set the team"s default area path (team field value in TFS) to {DefaultAreaPath}"))
                {
                    if(tpc.IsHostedServer)
                    {
                        this.Log("Conected to Azure DevOps Server. Treating Team Field Value as Area Path");

                        DefaultAreaPath = _NormalizeNodePath -Project tp.Name -Path DefaultAreaPath -Scope Areas -IncludeTeamProject
                    }

                    if(! AreaPaths)
                    {
                        this.Log("AreaPaths is empty. Adding DefaultAreaPath (TeamFieldValue) to AreaPaths as default value.");

                        AreaPaths = @{ DefaultAreaPath = true }
                    }

                    this.Log($"Setting default area path (team field) to {DefaultAreaPath}");

                    patch = new Microsoft.TeamFoundation.Work.WebApi.TeamFieldValuesPatch() -Property @{
                        DefaultValue = DefaultAreaPath
                    }

                    values = @()

                    foreach(a in AreaPaths.GetEnumerator())
                    {
                        values += new Microsoft.TeamFoundation.Work.WebApi.TeamFieldValue() -Property @{
                            Value = _NormalizeNodePath -Project tp.Name -Path a.Key -Scope Areas -IncludeTeamProject
                            IncludeChildren = a.Value
                        }
                    }

                    patch.Values = [Microsoft.TeamFoundation.Work.WebApi.TeamFieldValue[]] values

                    task = client.UpdateTeamFieldValuesAsync(patch, ctx)

                    result = task.Result; if(task.IsFaulted) { _throw new Exception("Error applying team field value and/or area path settings" task.Exception.InnerExceptions })
                }

                # Set backlog and iteration path settings

                patch = new Microsoft.TeamFoundation.Work.WebApi.TeamSettingsPatch()
                isDirty = false

                if (BacklogIteration && ShouldProcess(Team, $"Set the team"s backlog iteration to {BacklogIteration}"))
                {
                    this.Log($"Setting backlog iteration to {BacklogIteration}");
                    iteration = Get-TfsClassificationNode -StructureGroup Iterations -Iteration BacklogIteration -Project Project -Collection Collection
                    patch.BacklogIteration = [guid] iteration.Identifier
                    patch.DefaultIteration = [guid] iteration.Identifier

                    isDirty = true
                }

                if (DefaultIteration && ShouldProcess(Team, $"Set the team"s default iteration to {DefaultIteration}"))
                {
                    this.Log($"Setting default iteration to {DefaultIteration}");
                    iteration = Get-TfsClassificationNode -StructureGroup Iterations -Iteration BacklogIteration -Project Project -Collection Collection
                    patch.DefaultIteration = [guid] iteration.Identifier

                    isDirty = true
                }

                if (BacklogVisibilities && ShouldProcess(Team, $"Set the team"s backlog visibilities to {_DumpObj {BacklogVisibilities}}"))
                {
                    this.Log($"Setting backlog iteration to {BacklogVisibilities}");
                    patch.BacklogVisibilities = _NewDictionary @([string], [bool]) BacklogVisibilities

                    isDirty = true
                }

                if (DefaultIterationMacro && ShouldProcess(Team, $"Set the team"s default iteration macro to {DefaultIterationMacro}"))
                {
                    this.Log($"Setting default iteration macro to {DefaultIterationMacro}");
                    patch.DefaultIterationMacro = DefaultIterationMacro

                    isDirty = true
                }

                if (WorkingDays && ShouldProcess(Team, $"Set the team"s working days to {_DumpObj {WorkingDays}}"))
                {
                    this.Log($"Setting working days to {{WorkingDays}|ConvertTo=-Json -Compress}");
                    patch.WorkingDays = WorkingDays

                    isDirty = true
                }

                if(BugsBehavior && ShouldProcess(Team, $"Set the team"s bugs behavior to {_DumpObj {BugsBehavior}}"))
                {
                    this.Log($"Setting bugs behavior to {_DumpObj {BugsBehavior}}");
                    patch.BugsBehavior = BugsBehavior

                    isDirty = true
                }

                if(isDirty)
                {
                    task = client.UpdateTeamSettingsAsync(patch, ctx)
                    result = task.Result; if(task.IsFaulted) { _throw new Exception("Error applying iteration settings" task.Exception.InnerExceptions })
                }

                if(Passthru.IsPresent)
                {
                    WriteObject(t); return;
                }
            }
        }
        */
    }
}
