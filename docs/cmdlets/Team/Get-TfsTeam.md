---
title: Get-TfsTeam
breadcrumbs: [ "Team" ]
parent: "Team"
description: "Gets information about one or more teams. "
remarks: 
parameterSets: 
  "_All_": [ Cached, Collection, Credential, Current, Default, IncludeMembers, IncludeSettings, Interactive, Password, PersonalAccessToken, Project, Server, Team, UserName ] 
  "Get by team":  
    Team: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "Cached credentials":  
    Team: 
      type: "object"  
      position: "0"  
    Cached: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Default: 
      type: "SwitchParameter"  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "User name and password":  
    Team: 
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
    Default: 
      type: "SwitchParameter"  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "Credential object":  
    Team: 
      type: "object"  
      position: "0"  
    Credential: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    Default: 
      type: "SwitchParameter"  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "Personal Access Token":  
    Team: 
      type: "object"  
      position: "0"  
    PersonalAccessToken: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Default: 
      type: "SwitchParameter"  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "Prompt for credential":  
    Team: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Default: 
      type: "SwitchParameter"  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter"  
    Interactive: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "Get current":  
    Current: 
      type: "SwitchParameter"  
      required: true  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter"  
  "Get default team":  
    Default: 
      type: "SwitchParameter"  
      required: true  
    IncludeMembers: 
      type: "SwitchParameter"  
    IncludeSettings: 
      type: "SwitchParameter" 
parameters: 
  - name: "Team" 
    description: "Specifies the team to return. Accepted values are its name, its ID, or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object. Wildcards are supported. When omitted, all teams in the given team project are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the team to return. Accepted values are its name, its ID, or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object. Wildcards are supported. When omitted, all teams in the given team project are returned. This is an alias of the Team parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "IncludeMembers" 
    description: "Get team members (fills the Members property with a list of Microsoft.VisualStudio.Services.WebApi.TeamMember objects). When omitted, only basic team information (such as name, description and ID) are returned. " 
    globbing: false 
    type: "SwitchParameter" 
    aliases: [ QueryMembership ] 
    defaultValue: "False" 
  - name: "QueryMembership" 
    description: "Get team members (fills the Members property with a list of Microsoft.VisualStudio.Services.WebApi.TeamMember objects). When omitted, only basic team information (such as name, description and ID) are returned. This is an alias of the IncludeMembers parameter." 
    globbing: false 
    type: "SwitchParameter" 
    aliases: [ QueryMembership ] 
    defaultValue: "False" 
  - name: "IncludeSettings" 
    description: "Gets team settings (fills the Settings, TeamField, and IterationPaths properties). " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Current" 
    description: "Returns the team specified in the last call to Connect-TfsTeam (i.e. the \"current\" team) " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Default" 
    description: "Returns the default team in the given team project. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Collection parameter." 
    globbing: false 
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
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "TfsCmdlets.Models.Team" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/Get-TfsTeam"
aliases: 
examples: 
---
