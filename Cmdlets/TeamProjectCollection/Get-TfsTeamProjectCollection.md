---
layout: cmdlet
title: Get-TfsTeamProjectCollection
parent: TeamProjectCollection
grand_parent: Cmdlets
---
## Get-TfsTeamProjectCollection
{: .no_toc}



```powershell
# Get by collection
Get-TfsTeamProjectCollection [[-Collection] <object>]
    [-Credential <object>]
    [-Server <object>]
    [<CommonParameter>]


# Get current
Get-TfsTeamProjectCollection
    -Current
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
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 | Server | Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the [Get-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/ConfigServer/Get-TfsConfigurationServer) cmdlet. |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call [Get-TfsCredential](https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential) with the appropriate arguments and pass its return to this argument. |
 | Current | Returns the team project collection specified in the last call to [Connect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (i.e. the "current" project collection) |
 
[Go to top](#get-tfsteamprojectcollection)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsteamprojectcollection)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.VisualStudio.Services.WebApi.VssConnection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.VisualStudio.Services.WebApi.VssConnection)

[Go to top](#get-tfsteamprojectcollection)

### Related Topics

* 


[Go to top](#get-tfsteamprojectcollection)

