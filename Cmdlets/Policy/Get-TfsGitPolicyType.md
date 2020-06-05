---
layout: cmdlet
title: Get-TfsGitPolicyType
parent: Policy
grand_parent: Cmdlets
---
## Get-TfsGitPolicyType
{: .no_toc}

Gets one or more Git branch policies supported by the given team project.

```powershell
Get-TfsGitPolicyType [[-PolicyType] <object>]
    [-Collection <object>]
    [-Project <object>]
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
 | PolicyType | Specifies the display name or ID of the policy type. Wildcards are supported. When omitted, all policy types supported by the given team project are returned. |
 | Project | Specifies the name of the Team Project, its ID (a GUID), or a [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject) object to connect to. When omitted, it defaults to the connection set by [Connect-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProject) (if any). For more details, see the [Get-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/TeamProject/Get-TfsTeamProject) cmdlet. |
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 
[Go to top](#get-tfsgitpolicytype)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsgitpolicytype)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.Policy.WebApi.PolicyType](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Policy.WebApi.PolicyType)

[Go to top](#get-tfsgitpolicytype)

### Related Topics



[Go to top](#get-tfsgitpolicytype)

