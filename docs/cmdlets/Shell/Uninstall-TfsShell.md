---
title: Uninstall-TfsShell
breadcrumbs: [ "Shell" ]
parent: "Shell"
description: "Removes Azure DevOps Shell shortcuts and Windows Terminal profile fragments. "
remarks: "Uninstall-TfsShell removes shortcuts and profile fragments previously created by Install-TfsShell. By default, it removes items from all targets (Start Menu, Desktop, and Windows Terminal). Use the -Target parameter for selective removal. "
parameterSets: 
  "_All_": [ Target ] 
  "__AllParameterSets":  
    Target: 
      type: "ShellTarget"  
      position: "0" 
parameters: 
  - name: "Target" 
    description: "Specifies the target locations to remove shortcuts and profile fragments from. Defaults to All (StartMenu, Desktop, and WindowsTerminal). Possible values: StartMenu, Desktop, WindowsTerminal, All" 
    globbing: false 
    position: 0 
    type: "ShellTarget" 
    defaultValue: "All"
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Shell/Uninstall-TfsShell"
aliases: 
examples: 
---
