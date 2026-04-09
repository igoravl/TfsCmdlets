---
title: Set-TfsPersonalAccessToken
breadcrumbs: [ "Identity", "PersonalAccessToken" ]
parent: "Identity.PersonalAccessToken"
description: "Edits the properties of an existing personal access token (PAT). "
remarks: "This command updates the metadata of a token (display name, scope, expiration date) without regenerating its value. To regenerate a token, use Update-TfsPersonalAccessToken. The token must be valid (not revoked) to be updated. "
parameterSets: 
  "_All_": [ AllOrganizations, Collection, Passthru, PersonalAccessToken, Scope, Server, ValidTo ] 
  "__AllParameterSets":  
    PersonalAccessToken: 
      type: "object"  
      position: "0"  
      required: true  
    AllOrganizations: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Scope: 
      type: "string[]"  
    Server: 
      type: "object"  
    ValidTo: 
      type: "DateTime" 
parameters: 
  - name: "PersonalAccessToken" 
    description: "Specifies the personal access token to update. Accepts a Guid (authorizationId) or a PatToken object. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "Name" 
    description: "Specifies the personal access token to update. Accepts a Guid (authorizationId) or a PatToken object. This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "DisplayName" 
    description: "Specifies the personal access token to update. Accepts a Guid (authorizationId) or a PatToken object. This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "Pat" 
    description: "Specifies the personal access token to update. Accepts a Guid (authorizationId) or a PatToken object. This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name,DisplayName,Pat ] 
  - name: "Scope" 
    description: "Specifies the new scope for the token. " 
    globbing: false 
    type: "string[]" 
  - name: "ValidTo" 
    description: "Specifies the new expiration date for the token. " 
    globbing: false 
    type: "DateTime" 
    defaultValue: "1/1/0001 12:00:00 AM" 
  - name: "AllOrganizations" 
    description: "When set, the token will be valid for all of the user's accessible organizations. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
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
    description: "Specifies the personal access token to update. Accepts a Guid (authorizationId) or a PatToken object. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/PersonalAccessToken/Set-TfsPersonalAccessToken"
aliases: 
examples: 
---
