---
title: Get-TfsTeamProject
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Gets information about one or more team projects."
remarks: "The Get-TfsTeamProject cmdlets gets one or more Team Project objects (an instance of Microsoft.TeamFoundation.Core.WebApi.TeamProject) from the supplied Team Project Collection."
parameterSets: 
  "_All_": [ Collection, Current, Deleted, Project ] 
  "Get by project":  
    Project: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
      position: "1"  
    Deleted: 
      type: "SwitchParameter"  
  "Get current":  
    Current: 
      type: "SwitchParameter"  
      required: true 
parameters: 
  - name: "Project" 
    description: "Specifies the name of a Team Project. Wildcards are supported. When omitted, all team projects in the supplied collection are returned." 
    globbing: false 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "Deleted" 
    description: "Lists deleted team projects present in the \"recycle bin\"" 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 1 
    type: "object" 
  - name: "Current" 
    description: "Returns the team project specified in the last call to Connect-TfsTeamProject (i.e. the \"current\" team project)" 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)."
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.TeamProject" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TeamProject/Get-TfsTeamProject"
aliases: 
examples: 
---
