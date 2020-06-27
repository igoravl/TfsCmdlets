---
title: Dismount-TfsTeamProjectCollection
breadcrumbs: [ "TeamProjectCollection" ]
parent: "TeamProjectCollection"
description: "Detaches a team project collection database from a Team Foundation Server installation."
remarks: "Before you move a collection, you must first detach it from the deployment of TFS on which it is running. It's very important that you do not skip this step. When you detach a collection, all jobs and services are stopped, and then the collection database is stopped. In addition, the detach process copies over the collection-specific data from the configuration database and saves it as part of the team project collection database. This configuration data is what allows the collection database to be attached to a different deployment of TFS. If that data is not present, you cannot attach the collection to any deployment of TFS except the one from which it originated. If detachment succeeds, this cmdlets returns the original database connection string. It is required to re-attach the collection to TFS."
parameterSets: 
  "_All_": [ Collection, Reason, Server, Timeout ] 
  "__AllParameterSets":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    Reason: 
      type: "string"  
    Server: 
      type: "object"  
    Timeout: 
      type: "TimeSpan" 
parameters: 
  - name: "Collection" 
    description: "Specifies the collection to detach." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Reason" 
    description: "Speficies a Servicing Message (optional), to provide a message for users who might try to connect to projects in this collection while it is offline." 
    globbing: false 
    type: "string" 
  - name: "Timeout" 
    description: "Specifies the maximum period of time this cmdlet should wait for the detach procedure to complete. By default, it waits indefinitely until the collection servicing completes." 
    globbing: false 
    type: "TimeSpan" 
    defaultValue: "10675199.02:48:05.4775807" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the collection to detach."
outputs: 
  - type: "System.String" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TeamProjectCollection/Dismount-TfsTeamProjectCollection" 
  - text: "https://www.visualstudio.com/en-us/docs/setup-admin/tfs/admin/move-project-collection#1-detach-the-collection" 
    uri: 
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Dismount-TfsTeamProjectCollection -Collection http://vsalm:8080/tfs/DefaultCollection -Reason \"Collection DefaultCollecton is down for maintenance\"" 
    remarks: "Detaches the project collection specified by the URL provided in the Collection argument, defining a Maintenance Message to be shown to users when they try to connect to that collection while it is detached"
---
