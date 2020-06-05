---
layout: cmdlet
title: Connect-TfsTeamProjectCollection
parent: Connection
grand_parent: Cmdlets
---
## Connect-TfsTeamProjectCollection
{: .no_toc}

Connects to a TFS team project collection or Azure DevOps organization.

```powershell
# Prompt for credential
Connect-TfsTeamProjectCollection
    [-Collection] <object>
    -Cached
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]


# Cached credentials
Connect-TfsTeamProjectCollection
    [-Collection] <object>
    [-UserName] <string> [[-Password] <SecureString>]
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]


# User name and password
Connect-TfsTeamProjectCollection
    [-Collection] <object>
    -Credential <object>
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]


# Credential object
Connect-TfsTeamProjectCollection
    [-Collection] <object>
    -AccessToken <string>
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]


# Personal Access Token
Connect-TfsTeamProjectCollection
    [-Collection] <object>
    [-Interactive]
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

The Connect-TfsTeamProjectCollection cmdlet connects to a TFS Team Project Collection or Azure DevOps organization. That connection can be later reused by other TfsCmdlets commands until it's closed by a call to [Disconnect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Disconnect-TfsTeamProjectCollection).

[Go to top](#connect-tfsteamprojectcollection)
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. |
 | Cached | HELP_PARAM_CACHED_CREDENTIALS |
 | UserName | HELP_PARAM_USER_NAME |
 | Password | HELP_PARAM_PASSWORD |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call [Get-TfsCredential](https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential) with the appropriate arguments and pass its return to this argument. |
 | AccessToken | HELP_PARAM_PERSONAL_ACCESS_TOKEN |
 | Interactive | Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell. |
 | Server | Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the [Get-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/ConfigServer/Get-TfsConfigurationServer) cmdlet. |
 | Passthru | Returns the results of the command. By default, this cmdlet does not generate any output. |
 
[Go to top](#connect-tfsteamprojectcollection)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#connect-tfsteamprojectcollection)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.VisualStudio.Services.WebApi.VssConnection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.VisualStudio.Services.WebApi.VssConnection)

[Go to top](#connect-tfsteamprojectcollection)

### Examples


#### Example 1
```powershell
PS> Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection
```

Connects to a collection called "DefaultCollection" in a TF server called "tfs" using the cached credentials of the logged-on user

#### Example 2
```powershell
PS> Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Interactive
```

Connects to a collection called "DefaultCollection" in a Team Foundation server called "tfs", firstly prompting the user for credentials 
(it ignores the cached credentials for the currently logged-in user). It's equivalent to the command:


Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Credential ([Get-TfsCredential](https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential) -Interactive)


[Go to top](#connect-tfsteamprojectcollection)

### Related Topics



[Go to top](#connect-tfsteamprojectcollection)

