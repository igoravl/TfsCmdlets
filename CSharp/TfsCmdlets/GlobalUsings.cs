﻿global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Composition;
global using System.IO;
global using System.Linq;
global using System.Management.Automation;
global using System.Text;
global using TfsCmdlets.Controllers;
global using TfsCmdlets.Extensions;
global using TfsCmdlets.Services;
global using TfsCmdlets.HttpClients;

global using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
global using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
global using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
global using WebApiIdentityRef = Microsoft.VisualStudio.Services.WebApi.IdentityRef;
global using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;
global using WebApiTeamProjectRef = Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference;
global using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;
global using WebApiQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;
global using WebApiWorkItemRelation = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation;
global using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;
global using WebApiTeam = Microsoft.TeamFoundation.Core.WebApi.WebApiTeam;
global using WebApiFeed = Microsoft.VisualStudio.Services.Feed.WebApi.Feed;
global using WebApiPackage = Microsoft.VisualStudio.Services.Feed.WebApi.Package;
global using WebApiBuild = Microsoft.TeamFoundation.Build.WebApi.Build;