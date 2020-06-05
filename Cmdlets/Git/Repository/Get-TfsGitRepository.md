---
layout: cmdlet
title: Get-TfsGitRepository
description: Gets information from one or more Git repositories in a team project.
parent: Git/Repository
breadcrumbs: [Git,Repository]
---
## Get-TfsGitRepository
{: .no_toc}

Gets information from one or more Git repositories in a team project.

```powershell

Get-TfsGitRepository     [-Repository <object>]
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
 | Repository | Specifies the name or ID (a GUID) of a Git repository. Wildcards are supported. When omitted, all Git repositories in the supplied team project are returned. |
 | Project | Specifies the name of the Team Project, its ID (a GUID), or a [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject) object to connect to. When omitted, it defaults to the connection set by [Connect-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProject) (if any). For more details, see the [Get-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/TeamProject/Get-TfsTeamProject) cmdlet. |
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 
[Go to top](#get-tfsgitrepository)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsgitrepository)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository)

[Go to top](#get-tfsgitrepository)

### Related Topics



[Go to top](#get-tfsgitrepository)

