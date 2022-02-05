---
title: Disconnect-TfsOrganization
breadcrumbs: [ "Organization" ]
parent: "Organization"
description: "Disconnects from the currently connected Azure DevOps organization. "
remarks: "The Disconnect-TfsOrganization cmdlet removes the connection previously set by its counterpart Connect-TfsOrganization. Therefore, cmdlets relying on a \"default organization/collection\" as provided by \"Get-TfsOrganization -Current\" will no longer work after a call to this cmdlet, unless their -Collection argument is provided or a new call to Connect-TfsTeam is made. "
parameterSets: 
  "_All_": [  ] 
  "__AllParameterSets": 
parameters: 
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Organization/Disconnect-TfsOrganization"
aliases: 
examples: 
---
