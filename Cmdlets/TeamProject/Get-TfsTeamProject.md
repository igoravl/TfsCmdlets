---
layout: cmdlet
title: Get-TfsTeamProject
parent: TeamProject
grand_parent: Cmdlets
---
## Get-TfsTeamProject
{: .no_toc}

Gets information about one or more team projects.

```powershell
# Get by project
Get-TfsTeamProject [[-Project] <object>] [[-Collection] <object>]
    [<CommonParameter>]


# Get current
Get-TfsTeamProject
    -Current
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

The Get-TfsTeamProject cmdlets gets one or more Team Project objects (an instance of [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject)) from the supplied Team Project Collection.

[Go to top](#get-tfsteamproject)
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Project | Specifies the name of a Team Project. Wildcards are supported. When omitted, all team projects in the supplied collection are returned. |
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 | Current | Returns the team project specified in the last call to [Connect-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProject) (i.e. the "current" team project) |
 
[Go to top](#get-tfsteamproject)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsteamproject)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject)

[Go to top](#get-tfsteamproject)

### Related Topics



[Go to top](#get-tfsteamproject)

