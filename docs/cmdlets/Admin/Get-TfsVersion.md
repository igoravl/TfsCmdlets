---
title: Get-TfsVersion
breadcrumbs: [ "Admin" ]
parent: "Admin"
description: "Gets the version information about Team Foundation / Azure DevOps servers and Azure DevOps Services organizations."
remarks: "The Get-TfsVersion cmdlet retrieves version information from the supplied team project collection or Azure DevOps organization. Currently supported platforms are Team Foundation Server 2015+, Azure DevOps Server 2019+ and Azure DevOps Services. When available/applicable, detailed information about installed updates, deployed sprints and so on are also provided."
parameterSets: 
  "_All_": [ Collection ] 
  "__AllParameterSets":  
    Collection: 
      type: "object" 
parameters: 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)."
outputs: 
  - type: "TfsCmdlets.Util.ServerVersion" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Admin/Get-TfsVersion"
aliases: 
examples: 
---
