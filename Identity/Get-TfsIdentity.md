---
title: Get-TfsIdentity
breadcrumbs: [ "Identity" ]
parent: "Identity"
description: "Gets one or more identities that represents either users or groups in Azure DevOps. "
remarks: 
parameterSets: 
  "_All_": [ Current, Identity, QueryMembership, Server ] 
  "Get Identity":  
    Identity: 
      type: "object"  
      position: "0"  
      required: true  
    QueryMembership: 
      type: "QueryMembership"  
    Server: 
      type: "object"  
  "Get current user":  
    Current: 
      type: "SwitchParameter"  
      required: true  
    Server: 
      type: "object" 
parameters: 
  - name: "Identity" 
    description: "Specifies the user or group to be retrieved. Supported values are: User/group name, email, or ID " 
    required: true 
    globbing: false 
    position: 0 
    type: "object" 
  - name: "QueryMembership" 
    description: "Specifies how group membership information should be processed when the returned identity is a group. \"Direct\" fetches direct members (both users and groups) of the group. \"Expanded\" expands contained groups recursively and returns their contained users. \"None\" is the fastest option as it fetches no membership information. When omitted, defaults to Direct. Possible values: None, Direct, Expanded, ExpandedUp, ExpandedDown" 
    globbing: false 
    type: "QueryMembership" 
    defaultValue: "Direct" 
  - name: "Current" 
    description: "Returns an identity representing the user currently logged in to the Azure DevOps / TFS instance " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.Identity.Identity" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/Get-TfsIdentity"
aliases: 
examples: 
---
