---
layout: cmdlet
title: Start-TfsIdentitySync
parent: Admin
grand_parent: Cmdlets
---
## Start-TfsIdentitySync
{: .no_toc}

Triggers an Identity Sync server job.

```powershell
Start-TfsIdentitySync [[-Server] <object>]
    [-Credential <object>]
    [-Wait]
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
 | Server | Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the [Get-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/ConfigServer/Get-TfsConfigurationServer) cmdlet. |
 | Wait | Waits until the job finishes running. If omitted, the identity sync job will run asynchronously. |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call [Get-TfsCredential](https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential) with the appropriate arguments and pass its return to this argument. |
 | WhatIf | Shows what would happen if the cmdlet runs. The cmdlet is not run. |
 | Confirm | Prompts you for confirmation before running the cmdlet. |
 
[Go to top](#start-tfsidentitysync)

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

* [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)

[Go to top](#start-tfsidentitysync)

### Related Topics



[Go to top](#start-tfsidentitysync)

