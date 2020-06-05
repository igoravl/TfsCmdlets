---
layout: cmdlet
title: Connect-TfsTeamProject
parent: Connection
grand_parent: Cmdlets
---
## Connect-TfsTeamProject
{: .no_toc}

Connects to a Team Project.

```powershell
# Explicit credentials
Connect-TfsTeamProject
    [-Project] <object>
    -Cached
    [-Collection <object>]
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]


# Cached credentials
Connect-TfsTeamProject
    [-Project] <object>
    [-UserName] <string> [[-Password] <SecureString>]
    [-Collection <object>]
    [-Passthru <SwitchParameter>]
    [-Server <object>]
    [<CommonParameter>]


# User name and password
Connect-TfsTeamProject
    [-Project] <object>
    -Credential <object>
    [-Collection <object>]
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]


# Credential object
Connect-TfsTeamProject
    [-Project] <object>
    -AccessToken <string>
    [-Collection <object>]
    [-Passthru]
    [-Server <object>]
    [<CommonParameter>]


# Personal Access Token
Connect-TfsTeamProject
    [-Project] <object>
    [-Collection <object>]
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
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Project | Specifies the name of the Team Project, its ID (a GUID), or a [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject) object to connect to. |
 | Collection | Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the [Get-TfsTeamProjectCollection](/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) cmdlet. When omitted, it defaults to the connection set by [Connect-TfsTeamProjectCollection](/Cmdlets/Connection/Connect-TfsTeamProjectCollection) (if any). |
 | Cached | HELP_PARAM_CACHED_CREDENTIALS |
 | UserName | HELP_PARAM_USER_NAME |
 | Password | HELP_PARAM_PASSWORD |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call [Get-TfsCredential](/Cmdlets/Connection/Get-TfsCredential) with the appropriate arguments and pass its return to this argument. |
 | AccessToken | HELP_PARAM_PERSONAL_ACCESS_TOKEN |
 | Interactive | Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell. |
 | Server | Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the [Get-TfsConfigurationServer](/Cmdlets/ConfigServer/Get-TfsConfigurationServer) cmdlet. |
 | Passthru | Returns the results of the command. By default, this cmdlet does not generate any output. |
 
[Go to top](#connect-tfsteamproject)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#connect-tfsteamproject)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.Core.WebApi.TeamProject](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Core.WebApi.TeamProject)

[Go to top](#connect-tfsteamproject)

### Related Topics



[Go to top](#connect-tfsteamproject)

