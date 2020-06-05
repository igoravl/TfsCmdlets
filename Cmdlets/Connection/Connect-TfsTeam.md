---
layout: cmdlet
title: Connect-TfsTeam
parent: Connection
grand_parent: Cmdlets
---
## Connect-TfsTeam
{: .no_toc}

Connects to a team.

```powershell
# Prompt for credential
Connect-TfsTeam
    [-Team] <object>
    -Cached
    [-Collection <object>]
    [-Passthru]
    [-Project <object>]
    [-Server <object>]
    [<CommonParameter>]


# Cached credentials
Connect-TfsTeam
    [-Team] <object>
    [-UserName] <string> [[-Password] <SecureString>]
    [-Collection <object>]
    [-Passthru]
    [-Project <object>]
    [-Server <object>]
    [<CommonParameter>]


# User name and password
Connect-TfsTeam
    [-Team] <object>
    -Credential <object>
    [-Collection <object>]
    [-Passthru]
    [-Project <object>]
    [-Server <object>]
    [<CommonParameter>]


# Credential object
Connect-TfsTeam
    [-Team] <object>
    -AccessToken <string>
    [-Collection <object>]
    [-Passthru]
    [-Project <object>]
    [-Server <object>]
    [<CommonParameter>]


# Personal Access Token
Connect-TfsTeam
    [-Team] <object>
    [-Collection <object>]
    [-Interactive]
    [-Passthru]
    [-Project <object>]
    [-Server <object>]
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
 | Team | Specifies the name of the Team, its ID (a GUID), or a [Microsoft.TeamFoundation.Core.WebApi.WebApiTeam](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.WebApiTeam) object to connect to. For more details, see the [Get-TfsTeam](https://tfscmdlets.dev/Cmdlets/Team/Get-TfsTeam) cmdlet. |
 | Project | Specifies the name of the Team Project, its ID (a GUID), or a [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject) object to connect to. When omitted, it defaults to the connection set by [Connect-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProject) (if any). For more details, see the [Get-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/TeamProject/Get-TfsTeamProject) cmdlet. |
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 | Cached | HELP_PARAM_CACHED_CREDENTIALS |
 | UserName | HELP_PARAM_USER_NAME |
 | Password | HELP_PARAM_PASSWORD |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call [Get-TfsCredential](https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential) with the appropriate arguments and pass its return to this argument. |
 | AccessToken | HELP_PARAM_PERSONAL_ACCESS_TOKEN |
 | Interactive | Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell. |
 | Server | Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the [Get-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/ConfigServer/Get-TfsConfigurationServer) cmdlet. |
 | Passthru | Returns the results of the command. By default, this cmdlet does not generate any output. |
 
[Go to top](#connect-tfsteam)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#connect-tfsteam)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.Core.WebApi.WebApiTeam](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.WebApiTeam)

[Go to top](#connect-tfsteam)

### Related Topics



[Go to top](#connect-tfsteam)

