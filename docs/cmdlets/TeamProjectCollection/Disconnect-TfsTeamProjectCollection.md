---
title: Disconnect-TfsTeamProjectCollection
breadcrumbs: [ "TeamProjectCollection" ]
parent: "TeamProjectCollection"
description: "Disconnects from the currently connected TFS team project collection or Azure DevOps organization. "
remarks: "The Disconnect-TfsTeamProjectCollection cmdlet removes the connection previously set by its counterpart Connect-TfsTeamProjectCollection. Therefore, cmdlets relying on a \"default collection\" as provided by \"Get-TfsTeamProjectCollection -Current\" will no longer work after a call to this cmdlet, unless their -Collection argument is provided or a new call to Connect-TfsTeam is made. "
parameterSets: 
  "_All_": [  ] 
  "__AllParameterSets": 
parameters: 
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProjectCollection/Disconnect-TfsTeamProjectCollection"
aliases: 
examples: 
---
