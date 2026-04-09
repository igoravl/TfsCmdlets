---
title: Remove-TfsPersonalAccessToken
breadcrumbs: [ "Identity", "PersonalAccessToken" ]
parent: "Identity.PersonalAccessToken"
description: "Revokes (removes) a personal access token (PAT). Administrators can revoke another user's tokens by specifying the -User parameter. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Force, PersonalAccessToken, Server, User ] 
  "Remove own token":  
    PersonalAccessToken: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "Remove for user":  
    PersonalAccessToken: 
      type: "object"  
      position: "0"  
      required: true  
    User: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Server: 
      type: "object" 
parameters: 
  - name: "PersonalAccessToken" 
    description: "Specifies the personal access token to revoke. Accepts a Guid (authorizationId) or a PatToken object. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "Name" 
    description: "Specifies the personal access token to revoke. Accepts a Guid (authorizationId) or a PatToken object. This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "DisplayName" 
    description: "Specifies the personal access token to revoke. Accepts a Guid (authorizationId) or a PatToken object. This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "Pat" 
    description: "Specifies the personal access token to revoke. Accepts a Guid (authorizationId) or a PatToken object. This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "User" 
    description: "Specifies a user whose token should be revoked. Requires administrator privileges. The value should be a SubjectDescriptor (e.g. 'aad.xxx' or 'msa.xxx'). " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "Force" 
    description: "Suppresses the confirmation prompt for the token revocation. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
inputs: 
  - type: "System.Object" 
    description: "Specifies the personal access token to revoke. Accepts a Guid (authorizationId) or a PatToken object. "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/PersonalAccessToken/Remove-TfsPersonalAccessToken"
aliases: 
examples: 
---
