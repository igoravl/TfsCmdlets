---
layout: cmdlet
title: Connect-TfsConfigurationServer
parent: Connection
grand_parent: Cmdlets
---
## Connect-TfsConfigurationServer
{: .no_toc}

Connects to a configuration server.

```powershell
# Prompt for credential
Connect-TfsConfigurationServer
    [-Server] <object>
    -Cached
    [-Passthru] [<CommonParameters>]Connect-TfsConfigurationServer
    [-Server] <object>
    [-UserName] <string> [[-Password] <Secure
    [<CommonParameter>]


# Cached credentials
[<CommonParameters>]Connect-TfsConfigurationServer
    [-Server] <object>
    -Credential <object>
    [-Passthru]
    [<CommonParameter>]


# User name and password
Connect-TfsConfigurationServer
    [-Server] <object>
    -AccessToken <string>
    [-Passthru] [<CommonParameters>]Connect-TfsConfigurationServer
    [-Server] <object>
    [-Interactive]
    [-Passthru]
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, it is represented by the Tfs_Configuration database. Operations that should be performed on a server level (such as setting server-level permissions) require a connection to a TFS configuration server. Internally, this connection is represented by an instance of the [Microsoft.TeamFoundation.Client.TfsConfigurationServer](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Client.TfsConfigurationServer). NOTE: Currently it is only supported in Windows PowerShell.

[Go to top](#connect-tfsconfigurationserver)
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Server | Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized TfsConfigurationServer object. When using a URL, it must be fully qualified. To connect to a Team Foundation Server instance by using its name, it must have been previously registered. |
 | Cached | HELP_PARAM_CACHED_CREDENTIALS |
 | UserName | HELP_PARAM_USER_NAME |
 | Password | HELP_PARAM_PASSWORD |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call [Get-TfsCredential](https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential) with the appropriate arguments and pass its return to this argument. |
 | AccessToken | HELP_PARAM_PERSONAL_ACCESS_TOKEN |
 | Interactive | Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell. |
 | Passthru | Returns the results of the command. By default, this cmdlet does not generate any output. |
 
[Go to top](#connect-tfsconfigurationserver)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#connect-tfsconfigurationserver)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.Client.TfsConfigurationServer](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Client.TfsConfigurationServer)

[Go to top](#connect-tfsconfigurationserver)

### Examples


#### Example 1
```powershell
PS> Connect-TfsConfigurationServer -Server http://vsalm:8080/tfs
```

Connects to the TFS server specified by the URL in the Server argument

#### Example 2
```powershell
PS> Connect-TfsConfigurationServer -Server vsalm
```

Connects to a previously registered TFS server by its user-defined name "vsalm". For more information, see 
[Get-TfsRegisteredConfigurationServer](https://tfscmdlets.dev/Cmdlets/ConfigServer/Get-TfsRegisteredConfigurationServer)


[Go to top](#connect-tfsconfigurationserver)

### Related Topics



[Go to top](#connect-tfsconfigurationserver)

