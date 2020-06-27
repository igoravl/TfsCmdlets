---
title: Get-TfsCredential
breadcrumbs: [ "Connection" ]
parent: "Connection"
description: "Provides credentials to use when you connect to a Team Foundation Server or Azure DevOps organization."
remarks: 
parameterSets: 
  "_All_": [ AccessToken, Cached, Credential, Interactive, Password, UserName ] 
  "Cached credentials":  
    Cached: 
      type: "SwitchParameter"  
  "User name and password":  
    UserName: 
      type: "string"  
      position: "1"  
      required: true  
    Password: 
      type: "SecureString"  
      position: "2"  
  "Credential object":  
    Credential: 
      type: "object"  
      required: true  
  "Personal Access Token":  
    AccessToken: 
      type: "string"  
      required: true  
  "Prompt for credential":  
    Interactive: 
      type: "SwitchParameter"  
      required: true 
parameters: 
  - name: "Cached" 
    description: "Specifies that cached (default) credentials should be used when possible/available." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "UserName" 
    description: "Specifies a user name for authentication modes (such as Basic) that support username/password-based credentials. Must be used in conjunction with the -Password argument" 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Password" 
    description: "Specifies a password for authentication modes (such as Basic) that support username/password-based credentials. Must be used in conjunction with the -UserName argument" 
    globbing: false 
    position: 2 
    type: "SecureString" 
  - name: "Credential" 
    description: "Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its return to this argument." 
    required: true 
    globbing: false 
    type: "object" 
  - name: "AccessToken" 
    description: "Specifies a personal access token, used as an alternate credential, to authenticate to Azure DevOps" 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ Pat,PersonalAccessToken ] 
  - name: "Pat" 
    description: "Specifies a personal access token, used as an alternate credential, to authenticate to Azure DevOpsThis is an alias of the AccessToken parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ Pat,PersonalAccessToken ] 
  - name: "PersonalAccessToken" 
    description: "Specifies a personal access token, used as an alternate credential, to authenticate to Azure DevOpsThis is an alias of the AccessToken parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ Pat,PersonalAccessToken ] 
  - name: "Interactive" 
    description: "Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell." 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
outputs: 
  - type: "Microsoft.VisualStudio.Services.Client.VssClientCredentials" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Connection/Get-TfsCredential"
aliases: 
examples: 
---
