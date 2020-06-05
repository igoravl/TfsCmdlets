---
layout: cmdlet
title: Get-TfsConfigurationServer
description: Gets information about a configuration server.
parent: ConfigServer
breadcrumbs: [ConfigServer]
---
## Get-TfsConfigurationServer
{: .no_toc}

Gets information about a configuration server.

```powershell
# Get by server
 
Get-TfsConfigurationServer     [-Server <object>]
     [-Credential <object>]
 # Get current
 
Get-TfsConfigurationServer     -Current

```

### Table of Contents
{: .no_toc .text-delta}

1. TOC
{:toc}

-----
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Server | Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. |
 | Current | Returns the configuration server specified in the last call to [Connect-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsConfigurationServer) (i.e. the "current" configuration server) |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call [Get-TfsCredential](https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential) with the appropriate arguments and pass its return to this argument. |
 
[Go to top](#get-tfsconfigurationserver)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#get-tfsconfigurationserver)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.TeamFoundation.Client.TfsConfigurationServer](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.TeamFoundation.Client.TfsConfigurationServer)

[Go to top](#get-tfsconfigurationserver)

### Related Topics



[Go to top](#get-tfsconfigurationserver)

