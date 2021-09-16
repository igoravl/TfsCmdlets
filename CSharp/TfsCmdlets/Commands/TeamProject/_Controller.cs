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