---
layout: cmdlet
title: Get-TfsCredential
description: Provides credentials to use when you connect to a Team Foundation Server or Azure DevOps organization.
parent: Connection
breadcrumbs: [Connection]
---
## Get-TfsCredential
{: .no_toc}

Provides credentials to use when you connect to a Team Foundation Server or Azure DevOps organization.

```powershell
# Cached credentials
 
Get-TfsCredential     [-Cached]
 # User name and password
 
Get-TfsCredential     -UserName <string>
     [-Password <SecureString>]
 # Credential object
 
Get-TfsCredential     -Credential <object>
 # Personal Access Token
 
Get-TfsCredential     -AccessToken <string>
 # Prompt for credential
 
Get-TfsCredential     -Interactive

```

### Table of Contents
{: .no_toc .text-delta}

1. TOC
{:toc}

-----
### Parameters

| Parameter | Description |
|:----------|-------------|
 | Cached | HELP_PARAM_CACHED_CREDENTIALS |
 | UserName | HELP_PARAM_USER_NAME |
 | Password | HELP_PARAM_PASSWORD |
 | Credential | Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its return to this argument. |
 | AccessToken | HELP_PARAM_PERSONAL_ACCESS_TOKEN |
 | Interactive | Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell. |
 
[Go to top](#get-tfscredential)

### Outputs

The output type is the type of the objects that the cmdlet emits.

* [Microsoft.VisualStudio.Services.Client.VssClientCredentials](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.VisualStudio.Services.Client.VssClientCredentials)

[Go to top](#get-tfscredential)

### Related Topics



[Go to top](#get-tfscredential)

