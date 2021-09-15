// TODO


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using Microsoft.TeamFoundation.Core.WebApi;
//using Microsoft.VisualStudio.Services.Operations;
//using TfsCmdlets.Extensions;
//using TfsCmdlets.Models;
//using TfsCmdlets.Services;
//using TfsCmdlets.Services.Impl;
//using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
//using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;
//using WebApiTeamProjectRef = Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference;

//namespace TfsCmdlets.Cmdlets.TeamProject
//{
//    [Exports(typeof(WebApiTeamProject))]
//    internal class TeamProjectController : ProjectLevelController<WebApiTeamProject>
//    {
//        private IRestApiService ApiSvc { get; }
//        private IController<Process> ProcessController { get; }
//        private CurrentConnections CurrentConnections { get; }

//        protected override IEnumerable<WebApiTeamProject> DoGetItems(ParameterDictionary parameters)
//        {
//            var project = parameters.Get<object>(nameof(GetTeamProject.Project));
//            var current = parameters.Get<bool>(nameof(GetTeamProject.Current));
//            var deleted = parameters.Get<bool>(nameof(GetTeamProject.Deleted));

//            if (project == null || current)
//            {
//                Logger.Log("Get currently connected team project");

//                var c = CurrentConnections.Project;
//                if (c != null) yield return c;

//                yield break;
//            }

//            var client = GetClient<ProjectHttpClient>();

//            while (true) switch (project)
//                {
//                    case Microsoft.TeamFoundation.Core.WebApi.TeamProject tp:
//                        {
//                            yield return tp;
//                            yield break;
//                        }
//                    case Guid g:
//                        {
//                            project = g.ToString();
//                            continue;
//                        }
//                    case string s when !s.IsWildcard() && !deleted:
//                        {
//                            yield return FetchProject(s, client);
//                            yield break;
//                        }
//                    case string s:
//                        {
//                            var stateFilter = deleted ? ProjectState.Deleted : ProjectState.All;
//                            var tpRefs = FetchProjects(stateFilter, client);

//                            foreach (var tpRef in tpRefs.Where(r => StringExtensions.IsLike(r.Name, s)))
//                            {
//                                var proj = deleted ?
//                                    new Microsoft.TeamFoundation.Core.WebApi.TeamProject(tpRef) :
//                                    FetchProject(tpRef.Id.ToString(), client);

//                                if (proj == null) continue;

//                                yield return proj;
//                            }

//                            yield break;
//                        }
//                    default:
//                        {
//                            Logger.LogError(new ArgumentException($"Invalid or non-existent team project {project}"));
//                            yield break;
//                        }
//                }
//        }

//        protected override WebApiTeamProject DoConnectItem(ParameterDictionary parameters)
//        {
//            var tpc = Collection;
//            var tp = Project;

//            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp);

//            // TODO: 
//            //this.Log($"Adding '{tp.Name} to the MRU list");
//            //_SetMru "Server" - Value(srv.Uri)
//            //_SetMru "Collection" - Value(tpc.Uri)
//            //_SetMru "Project" - Value(tp.Name)

//            Logger.Log($"Connected to '{tp.Name}'");

//            return tp;
//        }

//        protected override void DoDisconnectItem(ParameterDictionary parameters)
//        {
//            CurrentConnections.Set(
//                CurrentConnections.Server,
//                CurrentConnections.Collection,
//                null
//            );
//        }

//        protected override WebApiTeamProject DoNewItem(ParameterDictionary parameters)
//        {
//            var tpc = Collection;
//            var project = parameters.Get<string>(nameof(NewTeamProject.Project));

//            if (!PowerShell.ShouldProcess(tpc, $"Create team project '{project}'"))
//            {
//                return null;
//            }

//            var processTemplate = parameters.Get<object>(nameof(NewTeamProject.ProcessTemplate));
//            processTemplate ??= ProcessController.GetItem(new { ProcessTemplate = "*", Default = true });

//            var description = parameters.Get<string>(nameof(NewTeamProject.Description));
//            var sourceControl = parameters.Get<string>(nameof(NewTeamProject.SourceControl));

//            var template = processTemplate switch
//            {
//                WebApiProcess p => p,
//                string s => ProcessController.GetItem(new { Process = s }),
//                _ => throw new ArgumentException($"Invalid or non-existent process template '{processTemplate}'")
//            };

//            var client = GetClient<ProjectHttpClient>();

//            var tpInfo = new WebApiTeamProject
//            {
//                Name = project,
//                Description = description,
//                Capabilities = new Dictionary<string, Dictionary<string, string>>
//                {
//                    ["versioncontrol"] = new Dictionary<string, string>
//                    {
//                        ["sourceControlType"] = sourceControl
//                    },
//                    ["processTemplate"] = new Dictionary<string, string>
//                    {
//                        ["templateTypeId"] = template.Id.ToString()
//                    }
//                }
//            };

//            // Trigger the project creation

//            var token = client.QueueCreateProject(tpInfo)
//                .GetResult("Error queueing project creation");

//            // Wait for the operation to complete

//            var opsClient = GetClient<OperationsHttpClient>();
//            var opsToken = opsClient.GetOperation(token.Id)
//                .GetResult("Error getting operation status");

//            while (
//                (opsToken.Status != OperationStatus.Succeeded) &&
//                (opsToken.Status != OperationStatus.Failed) &&
//                (opsToken.Status != OperationStatus.Cancelled))
//            {
//                Thread.Sleep(2);
//                opsToken = opsClient.GetOperation(token.Id)
//                    .GetResult("Error getting operation status");
//            }

//            if (opsToken.Status != OperationStatus.Succeeded)
//            {
//                throw new Exception($"Error creating team project {project}: {opsToken.ResultMessage}");
//            }

//            return GetItem(parameters);
//        }

//        protected override void DoRemoveItem(ParameterDictionary parameters)
//        {
//            var tpc = Collection;
//            var tps = GetItems(parameters);
//            var hard = parameters.Get<bool>(nameof(RemoveTeamProject.Hard));
//            var force = parameters.Get<bool>(nameof(RemoveTeamProject.Force));

//            var client = GetClient<ProjectHttpClient>();

//            foreach (var tp in tps)
//            {
//                if (!PowerShell.ShouldProcess(tpc, $"Delete team project '{tp.Name}'")) continue;

//                if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete team project '{tp.Name}'?")) continue;

//                if (hard && !PowerShell.ShouldContinue(
//                    "You are using the -Hard switch. The team project deletion is IRREVERSIBLE " +
//                    $"and may cause DATA LOSS. Are you sure you want to proceed with deleting team project '{tp.Name}'")) continue;

//                var method = hard ? "Hard" : "Soft";

//                Logger.Log($"{method}-deleting team project {tp.Name}");

//                var token = client.QueueDeleteProject(tp.Id, hard).GetResult($"Error queueing team project deletion");

//                // Wait for the operation to complete

//                var opsClient = GetClient<OperationsHttpClient>();
//                var opsToken = opsClient.GetOperation(token.Id).GetResult("Error getting operation status");

//                while (
//                    (opsToken.Status != OperationStatus.Succeeded) &&
//                    (opsToken.Status != OperationStatus.Failed) &&
//                    (opsToken.Status != OperationStatus.Cancelled))
//                {
//                    Thread.Sleep(2);
//                    opsToken = opsClient.GetOperation(token.Id).GetResult("Error getting operation status");
//                }

//                if (opsToken.Status != OperationStatus.Succeeded)
//                {
//                    throw new Exception($"Error deleting team project {tp.Name}: {opsToken.ResultMessage}");
//                }
//            }
//        }
//        protected override WebApiTeamProject DoRenameItem(ParameterDictionary parameters)
//        {
//            var tpc = Collection;
//            var tp = Project;
//            var newName = parameters.Get<string>("NewName");
//            var force = parameters.Get<bool>(nameof(RenameTeamProject.Force));

//            if (!PowerShell.ShouldProcess(tpc, $"Rename team project '{tp.Name}' to ")) return null;

//            if (!force && !PowerShell.ShouldContinue(
//                $"Renaming this project is a disruptive action that can " +
//                   "significantly impact all members. The new name will update across all version control " +
//                   "paths, work items, queries, URLs and any other project content. Project members may " +
//                   "need to react and all currently running builds may fail as a result of this change. " +
//                   $"Are you sure you want to rename team project '{tp.Name}'?")) return null;

//            Logger.Log($"Renaming team project '{tp.Name}' to '{newName}'");

//            var client = ApiSvc;

//            var token = client.QueueOperationAsync(
//                tpc,
//                $"/_apis/projects/{tp.Id}",
//                "PATCH",
//                $"{{\"name\":\"{newName}\"}}")
//                .GetResult($"Error renaming team project '{tp.Name}'");

//            // Wait for the operation to complete

//            var opsClient = GetClient<OperationsHttpClient>();
//            var opsToken = opsClient.GetOperation(token.Id)
//                .GetResult("Error getting operation status");

//            while (
//                (opsToken.Status != OperationStatus.Succeeded) &&
//                (opsToken.Status != OperationStatus.Failed) &&
//                (opsToken.Status != OperationStatus.Cancelled))
//            {
//                Thread.Sleep(2);
//                opsToken = opsClient.GetOperation(token.Id)
//                    .GetResult("Error getting operation status");
//            }

//            if (opsToken.Status != OperationStatus.Succeeded)
//            {
//                throw new Exception($"Error renaming team project '{tp.Name}': {opsToken.ResultMessage}");
//            }

//            return GetItem(new { Project = tp.Id });
//        }

//        protected override WebApiTeamProject DoSetItem(ParameterDictionary parameters)
//        {
//            var avatarImage = parameters.Get<string>(nameof(SetTeamProject.AvatarImage));

//            if (string.IsNullOrEmpty(avatarImage)) return Project;

//            if(!PowerShell.ShouldProcess(Project, $"Set avatar image to {avatarImage}")) return Project;

//            var client = GetClient<ProjectHttpClient>();
//            var projectAvatar = new ProjectAvatar();

//            if (!File.Exists(avatarImage)) throw new ArgumentException($"Invalid avatar image path '{avatarImage}'");

//            projectAvatar.Image = File.ReadAllBytes(avatarImage);

//            client.SetProjectAvatarAsync(projectAvatar, Project.Name)
//                .Wait($"Error setting team project avatar");

//            return Project;
//        }

//        protected override void DoUndoItem(ParameterDictionary parameters)
//        {
//            var project = parameters.Get<object>("Project");
//            var references = new List<WebApiTeamProjectRef>();

//            switch (project)
//            {
//                case WebApiTeamProjectRef tpRef:
//                {
//                    references.Add(tpRef);
//                    break;
//                }
//                case string s:
//                {
//                    references.AddRange(GetItems(new { Project = s, Deleted = true }));
//                    break;
//                }
//                default:
//                {
//                    throw new ArgumentException($"Invalid or non-existent team project '{Project}'");
//                }
//            }

//            foreach (var tp in references)
//            {
//                ApiSvc.InvokeAsync(
//                        Collection,
//                        $"/_apis/projects/{tp.Id}",
//                        "PATCH",
//                        $"{{\"state\":1,\"name\":\"{tp.Name}\"}}")
//                    .GetResult($"Error restoring team project '{tp.Name}'");
//            }
//        }

//        private WebApiTeamProject FetchProject(string project, ProjectHttpClient client)
//        {
//            try
//            {
//                return client.GetProject(project, true).GetResult($"Error getting team project '{project}'");
//            }
//            catch (Exception ex)
//            {
//                Logger.LogError(ex);
//            }

//            return null;
//        }

//        private IEnumerable<TeamProjectReference> FetchProjects(ProjectState stateFilter, ProjectHttpClient client)
//        {
//            try
//            {
//                return client.GetProjects(stateFilter).GetResult($"Error getting team project(s)");
//            }
//            catch (Exception ex)
//            {
//                Logger.LogError(ex);
//            }

//            return null;
//        }

//        public TeamProjectController(
//            IRestApiService apiSvc,
//            IController<WebApiProcess> processController,
//            WebApiTeamProject project,
//            CurrentConnections currentConnections,
//            TpcConnection collection,
//            ILogger logger,
//            IParameterManager parameterManager,
//            IPowerShellService powerShell)
//            : base(project, collection, logger, parameterManager, powerShell)
//        {
//            ApiSvc = apiSvc;
//            ProcessController = processController;
//            CurrentConnections = currentConnections;
//        }
//    }
//}