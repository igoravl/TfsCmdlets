---
layout: cmdlet
title: Get-TfsInstallationPath
description: Gets the installation path of a given Team Foundation Server component.
parent: Admin
breadcrumbs: [Admin]
---
## Get-TfsInstallationPath
{: .no_toc}

Gets the installation path of a given Team Foundation Server component.

```powershell
# Use computer name
 
Get-TfsInstallationPath     [-ComputerName <string>]
     [-Component <TfsComponent>]
     [-Version <int>]
     [-Credential <PSCredential>]
 # Use session
 
Get-TfsInstallationPath     -Session <PSSession>
     [-Component <TfsComponent>]
     [-Version <int>]
     [-Credential <PSCredential>]

```

### Table of Contents
{: .no_toc .text-delta}

1. TOC
{:toc}

-----

### Detailed Description 

Many times a Team Foundation Server admin needs to retrieve the location where TFS is actually installed. That can be useful, for instance, to locate tools like TfsSecurity or TfsServiceControl. That information is recorded at setup time, in a well-known location in the Windows Registry of the server where TFS is installed.

[Go to top](#get-tfsinstallationpath)
### Parameters

| Parameter | Description |
|:----------|-------------|
 | ComputerName | The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it's a remote machine. Optionally, a [System.Management.Automation.Runspaces.PSSession](https://docs.microsoft.com/en-us/dotnet/api/System.Management.Automation.Runspaces.PSSession) object pointing to a previously opened PowerShell Remote session can be provided instead. When omitted, defaults to the local machine where the script is being run |
 | Session | The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it's a remote machine. Optionally, a [System.Management.Automation.Runspaces.PSSession](https://docs.microsoft.com/en-us/dotnet/api/System.Management.Automation.Runspaces.PSSession) object pointing to a previously opened PowerShell Remote session can be provided instead. When omitted, defaults to the local machine where the script is being run |
 | Component | Indicates the TFS component whose installation path is being searched for. For the main TFS installation directory, use BaseInstallation. When omitted, defaults to BaseInstallation.   Possible values: BaseInstallation, ApplicationTier, SharePointExtensions, TeamBuild, Tools, VersionControlProxy |
 | Version | The TFS version number, represented by the year in its name. For e.g. TFS 2015, use "2015". When omitted, will default to the newest installed version of TFS / Azure DevOps Server |
 | Credential | The user credentials to be used to access a remote machine. Those credentials must have the required permission to execute a PowerShell Remote session on that computer and also the permission to access the Windows Registry. |
 
[Go to top](#get-tfsinstallationpath)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String)

[Go to top](#get-tfsinstallationpath)

### Examples


#### Example 1
```powershell
PS> Get-TfsInstallationPath -Version 2017
```

Gets the root folder (the BaseInstallationPath) of TFS in the local server where the cmdlet is being run

#### Example 2
```powershell
PS> Get-TfsInstallationPath -Computer SPTFSSRV -Version 2015 -Component SharepointExtensions -Credentials (Get-Credentials)
```

Gets the location where the SharePoint Extensions have been installed in the remote server SPTFSSRV, prompting for admin credentials to 
be used for establishing a PS Remoting session to the server


[Go to top](#get-tfsinstallationpath)

### Related Topics



[Go to top](#get-tfsinstallationpath)

