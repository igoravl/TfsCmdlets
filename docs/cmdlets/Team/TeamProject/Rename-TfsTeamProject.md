---
title: Rename-TfsTeamProject
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Renames a team project."
remarks: 
parameterSets: 
  "_All_": [ Collection, Force, NewName, Passthru, Project ] 
  "__AllParameterSets":  
    Project: 
      type: "object"  
      position: "0"  
    NewName: 
      type: "string"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter" 
parameters: 
  - name: "Project" 
    description: "Specifies the name of a Team Project to rename." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Force" 
    description: "Forces the renaming of the team project. When omitted, the command prompts for confirmation prior to renaming the team project." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "NewName" 
    description: "Specifies the new name of the item. Enter only a name - i.e., for items that support paths, do not enter a path and name." 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of a Team Project to rename."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TeamProject/Rename-TfsTeamProject"
aliases: 
examples: 
---
