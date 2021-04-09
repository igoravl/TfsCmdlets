---
title: Set-TfsTeamProject
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Changes the details of a team project. "
remarks: 
parameterSets: 
  "_All_": [ AvatarImage, Collection, Passthru, Project ] 
  "__AllParameterSets":  
    Project: 
      type: "object"  
      position: "0"  
    AvatarImage: 
      type: "string"  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter" 
parameters: 
  - name: "Project" 
    description: "Specifies the name of the Team Project. " 
    globbing: false 
    position: 0 
    type: "object" 
  - name: "AvatarImage" 
    description: "Specifies the name of a local image file to be uploaded and used as the team project avatar. To remove a previously set avatar, pass $null to this argument. " 
    globbing: false 
    type: "string" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.TeamProject" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProject/Set-TfsTeamProject"
aliases: 
examples: 
---
