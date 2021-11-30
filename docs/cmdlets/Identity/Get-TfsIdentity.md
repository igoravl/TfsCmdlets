---
title: Get-TfsIdentity
breadcrumbs: [ "Identity" ]
parent: "Identity"
description: "Gets one or more identities that represents either users or groups in Azure DevOps. This cmdlet resolves legacy identity information for use with older APIs such as the Security APIs. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Current, Identity, QueryMembership, Server ] 
  "Get Identity":  
    Identity: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    QueryMembership: 
      type: "QueryMembership"  
    Server: 
      type: "object"  
  "Get current user":  
    Current: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
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
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
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
