---
title: Connect-TfsTeamProjectCollection
breadcrumbs: [ "Connection" ]
parent: "Connection"
description: 
remarks: 
parameterSets: 
  "_All_": [ AccessToken, Cached, Collection, Credential, Interactive, Passthru, Password, Server, UserName ] 
  "Cached credentials":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    Cached: 
      type: "SwitchParameter"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "User name and password":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    UserName: 
      type: "string"  
      position: "1"  
      required: true  
    Password: 
      type: "SecureString"  
      position: "2"  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "Credential object":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    Credential: 
      type: "object"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "Personal Access Token":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    AccessToken: 
      type: "string"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "Prompt for credential":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    Interactive: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object" 
parameters: 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Cached" 
    description: "Specifies that cached (default) credentials should be used when possible/available." 
    required: true 
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
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet." 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL."
outputs: 
  - type: "Microsoft.VisualStudio.Services.WebApi.VssConnection" 
    description: 
notes: 
relatedLinks: 
aliases: 
examples: 
---