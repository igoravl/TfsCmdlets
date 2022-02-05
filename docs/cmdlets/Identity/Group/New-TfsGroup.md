---
title: New-TfsGroup
breadcrumbs: [ "Identity", "Group" ]
parent: "Identity.Group"
description: "Creates a new Azure DevOps group. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Description, Group, Passthru, Project, Scope, Server ] 
  "__AllParameterSets":  
    Group: 
      type: "string"  
      position: "0"  
    Collection: 
      type: "object"  
    Description: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Scope: 
      type: "GroupScope"  
    Server: 
      type: "object" 
parameters: 
  - name: "Group" 
    description: "Specifies the name of the new group. " 
    globbing: false 
    position: 0 
    type: "string" 
  - name: "Description" 
    description: "Specifies a description for the new group. " 
    globbing: false 
    type: "string" 
  - name: "Scope" 
    description: "Specifies the scope under which to create the group. When omitted, defaults to the Collection scope. Possible values: Server, Collection, Project" 
    globbing: false 
    type: "GroupScope" 
    defaultValue: "Collection" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
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
outputs: 
  - type: "Microsoft.VisualStudio.Services.Graph.Client.GraphGroup" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/Group/New-TfsGroup"
aliases: 
examples: 
---
