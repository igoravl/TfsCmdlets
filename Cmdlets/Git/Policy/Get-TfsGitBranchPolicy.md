---
layout: cmdlet
title: Get-TfsGitBranchPolicy
parent: Git/Policy
grand_parent: Cmdlets
---
## Get-TfsGitBranchPolicy
{: .no_toc}

Gets the Git branch policy configuration of the given Git branches.

```powershell
Get-TfsGitBranchPolicy [[-PolicyType] <object>]
    [-Branch <object>]
    [-Collection <object>]
    [-Project <object>]
    [-Repository <object>]
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----
### Parameters

| Parameter | Description |
|:----------|-------------|
 | PolicyType | _N/A_ |
 | Branch | _N/A_ |
 | Project | Specifies the name of the Team Project, its ID (a GUID), or a [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject) object to connect to. When omitted, it defaults to the connection set by [Connect-TfsTeamProject](/Cmdlets/Connection/Connect-TfsTeamProject) (if any). For more details, see the [Get-TfsTeamProject](/Cmdlets/TeamProject/Get-TfsTeamProject) cmdlet. |
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 | Repository | Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository) object obtained by e.g. a call to [Get-TfsGitRepository](/Cmdlets/Git/Repository/Get-TfsGitRepository). When omitted, it default to the team project name (i.e. the default repository). |
 
[Go to top](#get-tfsgitbranchpolicy)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsgitbranchpolicy)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration)

[Go to top](#get-tfsgitbranchpolicy)

### Related Topics



[Go to top](#get-tfsgitbranchpolicy)

