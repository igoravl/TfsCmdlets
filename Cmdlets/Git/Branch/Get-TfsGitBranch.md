---
layout: cmdlet
title: Get-TfsGitBranch
description: Gets information from one or more branches in a remote Git repository.
parent: Git/Branch
breadcrumbs: [Git,Branch]
---
## Get-TfsGitBranch
{: .no_toc}

Gets information from one or more branches in a remote Git repository.

```powershell

Get-TfsGitBranch     [-Branch <object>]
     [-Repository <object>]
     [-Project <object>]
     [-Collection <object>]

```

### Table of Contents
{: .no_toc .text-delta}

1. TOC
{:toc}

-----
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Branch | Specifies the name of a branch in the supplied Git repository. Wildcards are supported. When omitted, all branches are returned. |
 | Repository | Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository) object obtained by e.g. a call to [Get-TfsGitRepository](https://tfscmdlets.dev/Cmdlets/Git/Repository/Get-TfsGitRepository). When omitted, it default to the team project name (i.e. the default repository). |
 | Project | Specifies the name of the Team Project, its ID (a GUID), or a [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject) object to connect to. When omitted, it defaults to the connection set by [Connect-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProject) (if any). For more details, see the [Get-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/TeamProject/Get-TfsTeamProject) cmdlet. |
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 
[Go to top](#get-tfsgitbranch)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsgitbranch)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats)

[Go to top](#get-tfsgitbranch)

### Related Topics



[Go to top](#get-tfsgitbranch)

