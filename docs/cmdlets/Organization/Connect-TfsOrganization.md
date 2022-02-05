---
title: Connect-TfsOrganization
breadcrumbs: [ "Organization" ]
parent: "Organization"
description: "Connects to an Azure DevOps organization or a TFS Team Project Collection. "
remarks: "The Connect-TfsOrganization cmdlet connects to an Azure DevOps organization or a TFS Team Project Collection. That connection can be later reused by other TfsCmdlets commands until it's closed by a call to Disconnect-TfsOrganization. "
parameterSets: 
  "_All_": [ Cached, Credential, Interactive, Organization, Passthru, Password, PersonalAccessToken, Server, UserName ] 
  "Cached credentials":  
    Organization: 
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
    Organization: 
      type: "object"  
      position: "0"  
      required: true  
    Password: 
      type: "SecureString"  
      required: true  
    UserName: 
      type: "string"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "Credential object":  
    Organization: 
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
    Organization: 
      type: "object"  
      position: "0"  
      required: true  
    PersonalAccessToken: 
      type: "string"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "Prompt for credential":  
    Organization: 
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
  - name: "Organization" 
    description: "Specifies the URL to the Azure DevOps Organization or Team Project Collection to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organization by simply providing its name instead of the full URL. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Collection ] 
  - name: "Collection" 
    description: "Specifies the URL to the Azure DevOps Organization or Team Project Collection to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organization by simply providing its name instead of the full URL. This is an alias of the Organization parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Collection ] 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
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
    description: "Specifies the URL to the Azure DevOps Organization or Team Project Collection to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organization by simply providing its name instead of the full URL. " 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.WebApi.VssConnection" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Organization/Connect-TfsOrganization"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Connect-TfsOrganization -Collection http://tfs:8080/tfs/DefaultCollection" 
    remarks: "Connects to a collection called \"DefaultCollection\" in a TF server called \"tfs\" using the cached credentials of the logged-on user" 
  - title: "----------  EXAMPLE 2  ----------" 
    code: "PS> Connect-TfsOrganization -Collection http://tfs:8080/tfs/DefaultCollection -Interactive" 
    remarks: "Connects to a collection called \"DefaultCollection\" in a Team Foundation server called \"tfs\", firstly prompting the user for credentials (it ignores the cached credentials for the currently logged-in user). It's equivalent to the command: `Connect-TfsOrganization -Collection http://tfs:8080/tfs/DefaultCollection -Credential (Get-TfsCredential -Interactive)`"
---
