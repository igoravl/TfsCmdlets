---
title: Get-TfsGroup
breadcrumbs: [ "Identity", "Group" ]
parent: "Identity.Group"
description: "Gets one or more Azure DevOps groups. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Group, Project, Recurse, Scope, Server ] 
  "__AllParameterSets":  
    Group: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Recurse: 
      type: "SwitchParameter"  
    Scope: 
      type: "GroupScope"  
    Server: 
      type: "object" 
parameters: 
  - name: "Group" 
    description: "Specifies the group to be retrieved. Supported values are: Group name or ID. Wildcards are supported. " 
    globbing: false 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "Scope" 
    description: "Specifies the scope under which to search for the group. When omitted, defaults to the Collection scope. Possible values: Server, Collection, Project" 
    globbing: false 
    type: "GroupScope" 
    defaultValue: "Collection" 
  - name: "Recurse" 
    description: "Searches recursively for groups in the scopes under the specified scope. " 
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
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.Graph.Client.GraphGroup" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/Group/Get-TfsGroup"
aliases: 
examples: 
---
