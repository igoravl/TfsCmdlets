---
title: Get-TfsTeamProject
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Gets information about one or more team projects. "
remarks: "The Get-TfsTeamProject cmdlets gets one or more Team Project objects (an instance of Microsoft.TeamFoundation.Core.WebApi.TeamProject) from the supplied Team Project Collection. "
parameterSets: 
  "_All_": [ Cached, Collection, Credential, Current, Deleted, IncludeDetails, Interactive, Password, PersonalAccessToken, Process, Project, Server, UserName ] 
  "Get by project":  
    Project: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Deleted: 
      type: "SwitchParameter"  
    IncludeDetails: 
      type: "SwitchParameter"  
    Process: 
      type: "string[]"  
    Server: 
      type: "object"  
  "Cached credentials":  
    Project: 
      type: "object"  
      position: "0"  
    Cached: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Deleted: 
      type: "SwitchParameter"  
    IncludeDetails: 
      type: "SwitchParameter"  
    Process: 
      type: "string[]"  
    Server: 
      type: "object"  
  "User name and password":  
    Project: 
      type: "object"  
      position: "0"  
    Password: 
      type: "SecureString"  
      required: true  
    UserName: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Deleted: 
      type: "SwitchParameter"  
    IncludeDetails: 
      type: "SwitchParameter"  
    Process: 
      type: "string[]"  
    Server: 
      type: "object"  
  "Credential object":  
    Project: 
      type: "object"  
      position: "0"  
    Credential: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    Deleted: 
      type: "SwitchParameter"  
    IncludeDetails: 
      type: "SwitchParameter"  
    Process: 
      type: "string[]"  
    Server: 
      type: "object"  
  "Personal Access Token":  
    Project: 
      type: "object"  
      position: "0"  
    PersonalAccessToken: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Deleted: 
      type: "SwitchParameter"  
    IncludeDetails: 
      type: "SwitchParameter"  
    Process: 
      type: "string[]"  
    Server: 
      type: "object"  
  "Prompt for credential":  
    Project: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Deleted: 
      type: "SwitchParameter"  
    IncludeDetails: 
      type: "SwitchParameter"  
    Interactive: 
      type: "SwitchParameter"  
    Process: 
      type: "string[]"  
    Server: 
      type: "object"  
  "Get current":  
    Current: 
      type: "SwitchParameter"  
      required: true  
    IncludeDetails: 
      type: "SwitchParameter"  
    Process: 
      type: "string[]" 
parameters: 
  - name: "Project" 
    description: "Specifies the name of a Team Project. Wildcards are supported. When omitted, all team projects in the supplied collection are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "Deleted" 
    description: "Lists deleted team projects present in the \"recycle bin\" " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Process" 
    description: "Returns only those team projects matching the specified process template(s). Wildcards are supported. When omitted returns all team projects, regardless of process template. " 
    globbing: false 
    type: "string[]" 
  - name: "IncludeDetails" 
    description: "Includes details about the team projects, such as the process template name and other properties. Specifying this argument signficantly increases the time it takes to complete the operation. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Current" 
    description: "Returns the team project specified in the last call to Connect-TfsTeamProject (i.e. the \"current\" team project) " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Collection parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
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
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.TeamProject" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProject/Get-TfsTeamProject"
aliases: 
examples: 
---
