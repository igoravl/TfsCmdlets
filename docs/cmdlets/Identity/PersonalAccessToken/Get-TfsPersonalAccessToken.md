---
title: Get-TfsPersonalAccessToken
breadcrumbs: [ "Identity", "PersonalAccessToken" ]
parent: "Identity.PersonalAccessToken"
description: "Gets one or more personal access tokens (PATs) for the current user, or lists PATs of another user when running as an administrator. "
remarks: "The PAT Lifecycle Management API only allows users to manage their own tokens. Administrators can list tokens of other users by specifying the -User parameter, which uses the Token Admin API. "
parameterSets: 
  "_All_": [ AuthorizationId, Collection, Descending, PersonalAccessToken, Server, SortBy, State, User ] 
  "Get by name":  
    PersonalAccessToken: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Descending: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
    SortBy: 
      type: "SortByOptions"  
    State: 
      type: "DisplayFilterOptions"  
  "Get for user":  
    PersonalAccessToken: 
      type: "object"  
      position: "0"  
    User: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    Descending: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
    SortBy: 
      type: "SortByOptions"  
    State: 
      type: "DisplayFilterOptions"  
  "Get by id":  
    AuthorizationId: 
      type: "Guid"  
      required: true  
    Collection: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "PersonalAccessToken" 
    description: "Specifies the personal access token to retrieve. Accepts a token display name (wildcards supported), a Guid (authorizationId), or a PatToken object. When omitted, returns all tokens matching the filter criteria. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the personal access token to retrieve. Accepts a token display name (wildcards supported), a Guid (authorizationId), or a PatToken object. When omitted, returns all tokens matching the filter criteria. This is an alias of the PersonalAccessToken parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
    defaultValue: "*" 
  - name: "DisplayName" 
    description: "Specifies the personal access token to retrieve. Accepts a token display name (wildcards supported), a Guid (authorizationId), or a PatToken object. When omitted, returns all tokens matching the filter criteria. This is an alias of the PersonalAccessToken parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
    defaultValue: "*" 
  - name: "Pat" 
    description: "Specifies the personal access token to retrieve. Accepts a token display name (wildcards supported), a Guid (authorizationId), or a PatToken object. When omitted, returns all tokens matching the filter criteria. This is an alias of the PersonalAccessToken parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
    defaultValue: "*" 
  - name: "AuthorizationId" 
    description: "Specifies the personal access token to retrieve by its authorization ID. " 
    required: true 
    globbing: false 
    type: "Guid" 
    defaultValue: "00000000-0000-0000-0000-000000000000" 
  - name: "State" 
    description: "Filters tokens by state. Valid values are Active, Revoked, Expired, and All. Defaults to Active. Possible values: Active, Revoked, Expired, All" 
    globbing: false 
    type: "DisplayFilterOptions" 
    aliases: [ DisplayFilter ] 
    defaultValue: "Active" 
  - name: "DisplayFilter" 
    description: "Filters tokens by state. Valid values are Active, Revoked, Expired, and All. Defaults to Active. Possible values: Active, Revoked, Expired, AllThis is an alias of the State parameter." 
    globbing: false 
    type: "DisplayFilterOptions" 
    aliases: [ DisplayFilter ] 
    defaultValue: "Active" 
  - name: "SortBy" 
    description: "Specifies the field to sort results by. Valid values are DisplayName, DisplayDate, and Status. Possible values: DisplayName, DisplayDate, Status" 
    globbing: false 
    type: "SortByOptions" 
    defaultValue: "DisplayName" 
  - name: "Descending" 
    description: "When set, sorts results in descending order. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "User" 
    description: "Specifies a user whose tokens should be listed. Requires administrator privileges. Accepts Identity objects or subject descriptors. When specified, uses the Token Admin API. " 
    required: true 
    globbing: false 
    type: "object" 
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
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
  - type: "Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/PersonalAccessToken/Get-TfsPersonalAccessToken"
aliases: 
examples: 
---
