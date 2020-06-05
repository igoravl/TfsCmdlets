---
layout: cmdlet
title: Get-TfsVersion
parent: Admin
grand_parent: Cmdlets
---
## Get-TfsVersion
{: .no_toc}

Gets the version information about Team Foundation / Azure DevOps servers and Azure DevOps Services organizations.

```powershell
Get-TfsVersion
    [-Collection <object>]
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

The Get-TfsVersion cmdlet retrieves version information from the supplied team project collection or Azure DevOps organization. Currently supported platforms are Team Foundation Server 2015+, Azure DevOps Server 2019+ and Azure DevOps Services. When available/applicable, detailed information about installed updates, deployed sprints and so on are also provided.

[Go to top](#get-tfsversion)
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 
[Go to top](#get-tfsversion)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsversion)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* TfsCmdlets.Util.ServerVersion

[Go to top](#get-tfsversion)

### Related Topics



[Go to top](#get-tfsversion)

