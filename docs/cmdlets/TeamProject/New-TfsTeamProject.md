---
title: New-TfsTeamProject
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Creates a new team project. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Description, Passthru, ProcessTemplate, Project, SourceControl ] 
  "__AllParameterSets":  
    Project: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Description: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    ProcessTemplate: 
      type: "object"  
    SourceControl: 
      type: "string" 
parameters: 
  - name: "Project" 
    description: "Specifies the name of the new team project. " 
    required: true 
    globbing: false 
    position: 0 
    type: "object" 
  - name: "Description" 
    description: "Specifies a description for the new team project. " 
    globbing: false 
    type: "string" 
  - name: "SourceControl" 
    description: "Specifies the source control type to be provisioned initially with the team project. Supported types are \"Git\" and \"Tfvc\". " 
    globbing: false 
    type: "string" 
    defaultValue: "Git" 
  - name: "ProcessTemplate" 
    description: "Specifies the process template on which the new team project is based. Supported values are the process name or an instance of the Microsoft.TeamFoundation.Core.WebApi.Process class. " 
    globbing: false 
    type: "object" 
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
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProject/New-TfsTeamProject"
aliases: 
examples: 
---
