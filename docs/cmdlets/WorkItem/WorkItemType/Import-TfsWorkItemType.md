﻿---
title: Import-TfsWorkItemType
breadcrumbs: [ "WorkItem", "WorkItemType" ]
parent: "WorkItem.WorkItemType"
description: "Imports a work item type definition into a team project. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Path, Project, Server, Xml ] 
  "Import from XML":  
    Xml: 
      type: "string"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "Import from file":  
    Path: 
      type: "string"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "Xml" 
    description: "Specifies the XML definition of the work item type to import. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
  - name: "Path" 
    description: "Specifies the path to the file containing the XML definition of the work item type to import. " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
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
  - type: "System.String" 
    description: "Specifies the XML definition of the work item type to import. "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/WorkItemType/Import-TfsWorkItemType"
aliases: 
examples: 
---
