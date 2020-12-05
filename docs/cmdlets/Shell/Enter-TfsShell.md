---
title: Enter-TfsShell
breadcrumbs: [ "Shell" ]
parent: "Shell"
description: "Activates the Azure DevOps Shell "
remarks: 
parameterSets: 
  "_All_": [ DoNotClearHost, NoLogo, WindowTitle ] 
  "__AllParameterSets":  
    DoNotClearHost: 
      type: "SwitchParameter"  
    NoLogo: 
      type: "SwitchParameter"  
    WindowTitle: 
      type: "string" 
parameters: 
  - name: "WindowTitle" 
    description: "Specifies the shell window title. If omitted, defaults to \"Azure DevOps Shell\". " 
    globbing: false 
    type: "string" 
    defaultValue: "Azure DevOps Shell" 
  - name: "DoNotClearHost" 
    description: "Do not clear the host screen when activating the Azure DevOps Shell. When set, the prompt is enabled without clearing the screen. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "NoLogo" 
    description: "Do not show the version banner when activating the Azure DevOps Shell. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Shell/Enter-TfsShell"
aliases: 
examples: 
---
