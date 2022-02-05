---
title: Disconnect-TfsTeamProject
breadcrumbs: [ "TeamProject" ]
parent: "TeamProject"
description: "Disconnects from the currently connected team project. "
remarks: "The Disconnect-TfsTeamProject cmdlet removes the connection previously set by its counterpart Connect-TfsTeamProject. Therefore, cmdlets relying on a \"default team project\" as provided by \"Get-TfsTeamProject -Current\" will no longer work after a call to this cmdlet, unless their -Project argument is provided or a new call to Connect-TfsTeamProject is made. "
parameterSets: 
  "_All_": [  ] 
  "__AllParameterSets": 
parameters: 
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProject/Disconnect-TfsTeamProject"
aliases: 
examples: 
---
