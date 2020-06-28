---
title: Undo-TfsTeamProjectRemoval
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Undeletes one or more team projects."
remarks: 
parameterSets: 
  "_All_": [ Collection, Project ] 
  "__AllParameterSets":  
    Project: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object" 
parameters: 
  - name: "Project" 
    description: "Specifies the name of the Team Project to undelete." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project to undelete."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TeamProject/Undo-TfsTeamProjectRemoval"
aliases: 
examples: 
---
