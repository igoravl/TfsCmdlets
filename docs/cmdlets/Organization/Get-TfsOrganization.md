---
title: Get-TfsOrganization
breadcrumbs: [ "Organization" ]
parent: "Organization"
description: "Gets one of more team project collections (organizations in Azure DevOps). "
remarks: 
parameterSets: 
  "_All_": [ Cached, Credential, Current, Interactive, Organization, Password, PersonalAccessToken, Server, UserName ] 
  "Get by organization":  
    Organization: 
      type: "object"  
      position: "0"  
    Server: 
      type: "object"  
  "Cached credentials":  
    Organization: 
      type: "object"  
      position: "0"  
    Cached: 
      type: "SwitchParameter"  
      required: true  
    Server: 
      type: "object"  
  "User name and password":  
    Organization: 
      type: "object"  
      position: "0"  
    Password: 
      type: "SecureString"  
      required: true  
    UserName: 
      type: "string"  
      required: true  
    Server: 
      type: "object"  
  "Credential object":  
    Organization: 
      type: "object"  
      position: "0"  
    Credential: 
      type: "object"  
      required: true  
    Server: 
      type: "object"  
  "Personal Access Token":  
    Organization: 
      type: "object"  
      position: "0"  
    PersonalAccessToken: 
      type: "string"  
      required: true  
    Server: 
      type: "object"  
  "Prompt for credential":  
    Organization: 
      type: "object"  
      position: "0"  
    Interactive: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "Get current":  
    Current: 
      type: "SwitchParameter"  
      required: true 
parameters: 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Collection ] 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Organization parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Collection ] 
  - name: "Current" 
    description: "Returns the organization specified in the last call to Connect-TfsOrganization (i.e. the \"current\" organization) " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Cached" 
    description: "Specifies that cached (default) credentials should be used when possible/available. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "UserName" 
    description: "Specifies a user name for authentication modes (such as Basic) that support username/password-based credentials. Must be used in conjunction with the -Password argument " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "Password" 
    description: "Specifies a password for authentication modes (such as Basic) that support username/password-based credentials. Must be used in conjunction with the -UserName argument " 
    required: true 
    globbing: false 
    type: "SecureString" 
  - name: "Credential" 
    description: "Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its return to this argument. " 
    required: true 
    globbing: false 
    type: "object" 
  - name: "PersonalAccessToken" 
    description: "Specifies a personal access token, used as an alternate credential, to authenticate to Azure DevOps " 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ Pat ] 
  - name: "Pat" 
    description: "Specifies a personal access token, used as an alternate credential, to authenticate to Azure DevOps This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ Pat ] 
  - name: "Interactive" 
    description: "Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. "
outputs: 
  - type: "Microsoft.TeamFoundation.Client.TfsTeamProjectCollection" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Organization/Get-TfsOrganization"
aliases: 
examples: 
---
