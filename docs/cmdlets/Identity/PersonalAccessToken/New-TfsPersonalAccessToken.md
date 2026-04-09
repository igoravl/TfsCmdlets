---
title: New-TfsPersonalAccessToken
breadcrumbs: [ "Identity", "PersonalAccessToken" ]
parent: "Identity.PersonalAccessToken"
description: "Creates a new personal access token (PAT) for the current user. "
remarks: "The token string is only available at creation time and cannot be retrieved later. Make sure to save the token value returned by this command. "
parameterSets: 
  "_All_": [ AllOrganizations, Collection, Name, Passthru, Scope, Server, ValidTo ] 
  "__AllParameterSets":  
    Name: 
      type: "string"  
      position: "0"  
      required: true  
    Scope: 
      type: "string[]"  
      position: "1"  
    ValidTo: 
      type: "DateTime"  
      position: "2"  
    AllOrganizations: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object" 
parameters: 
  - name: "Name" 
    description: "Specifies the display name of the new personal access token. " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ DisplayName,Pat ] 
  - name: "DisplayName" 
    description: "Specifies the display name of the new personal access token. This is an alias of the Name parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ DisplayName,Pat ] 
  - name: "Pat" 
    description: "Specifies the display name of the new personal access token. This is an alias of the Name parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ DisplayName,Pat ] 
  - name: "Scope" 
    description: "Specifies the scope(s) of the new personal access token. When omitted, defaults to full access (\"app_token\"). " 
    globbing: false 
    position: 1 
    type: "string[]" 
  - name: "ValidTo" 
    description: "Specifies the expiration date of the new personal access token. When omitted, defaults to 30 days from now. " 
    globbing: false 
    position: 2 
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
outputs: 
  - type: "Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/PersonalAccessToken/New-TfsPersonalAccessToken"
aliases: 
examples: 
---
