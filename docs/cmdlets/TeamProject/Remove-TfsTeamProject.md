---
title: Remove-TfsTeamProject
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Deletes one or more team projects."
remarks: 
parameterSets: 
  "_All_": [ Collection, Force, Hard, Project ] 
  "__AllParameterSets":  
    Project: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Hard: 
      type: "SwitchParameter" 
parameters: 
  - name: "Project" 
    description: "Specifies the name of a Team Project to delete. Wildcards are supported." 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
  - name: "Hard" 
    description: "Deletes the team project permanently. When omitted, the team project is moved to a \"recycle bin\" and can be recovered either via UI or by using Undo-TfsTeamProjectRemoval." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Force" 
    description: "Forces the exclusion of the team project. When omitted, the command prompts for confirmation prior to deleting the team project." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of a Team Project to delete. Wildcards are supported."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TeamProject/Remove-TfsTeamProject"
aliases: 
examples: 
---
