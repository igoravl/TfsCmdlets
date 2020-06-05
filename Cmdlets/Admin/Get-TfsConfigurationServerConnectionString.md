---
layout: cmdlet
title: Get-TfsConfigurationServerConnectionString
description: Gets the configuration server database connection string.
parent: Admin
breadcrumbs: [Admin]
---
## Get-TfsConfigurationServerConnectionString
{: .no_toc}

Gets the configuration server database connection string.

```powershell
# Use computer name
 
Get-TfsConfigurationServerConnectionString     [-ComputerName <string>]
     [-Version <int>]
     [-Credential <PSCredential>]
 # Use session
 
Get-TfsConfigurationServerConnectionString     -Session <PSSession>
     [-Version <int>]
     [-Credential <PSCredential>]

```

### Table of Contents
{: .no_toc .text-delta}

1. TOC
{:toc}

-----
### Parameters

| Parameter | Description |
|:----------|-------------|
 | ComputerName | Specifies the name of a Team Foundation Server application tier from which to retrieve the connection string. |
 | Session | The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it's a remote machine. Optionally, a [System.Management.Automation.Runspaces.PSSession](https://docs.microsoft.com/en-us/dotnet/api/System.Management.Automation.Runspaces.PSSession) object pointing to a previously opened PowerShell Remote session can be provided instead. When omitted, defaults to the local machine where the script is being run |
 | Version | The TFS version number, represented by the year in its name. For e.g. TFS 2015, use "2015". When omitted, will default to the newest installed version of TFS / Azure DevOps Server |
 | Credential | The user credentials to be used to access a remote machine. Those credentials must have the required permission to execute a PowerShell Remote session on that computer. |
 
[Go to top](#get-tfsconfigurationserverconnectionstring)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String)

[Go to top](#get-tfsconfigurationserverconnectionstring)

### Related Topics

* [Get-TfsInstallationPath](https://tfscmdlets.dev/Cmdlets/Admin/Get-TfsInstallationPath)


[Go to top](#get-tfsconfigurationserverconnectionstring)

