---
title: Disconnect-TfsConfigurationServer
breadcrumbs: [ "Connection" ]
parent: "Connection"
description: "Disconnects from the currently connected configuration server."
remarks: "The Disconnect-TfsConfigurationServer cmdlet removes the connection previously set by its counterpart Connect-TfsConfigurationServer. Therefore, cmdlets relying on a \"default server\" as provided by \"Get-TfsConfigurationServer -Current\" will no longer work after a call to this cmdlet, unless their -Server argument is provided or a new call to Connect-TfsConfigurationServer is made."
parameterSets: 
  "_All_": [  ] 
  "__AllParameterSets": 
parameters: 
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Connection/Disconnect-TfsConfigurationServer"
aliases: 
examples: 
---
