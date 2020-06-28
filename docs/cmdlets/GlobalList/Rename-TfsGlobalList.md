---
title: Rename-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Changes either the name or the contents of a Global List."
remarks: 
parameterSets: 
  "_All_": [ GlobalList, NewName, Passthru ] 
  "__AllParameterSets":  
    GlobalList: 
      type: "string"  
      position: "0"  
      required: true  
    NewName: 
      type: "string"  
      position: "1"  
      required: true  
    Passthru: 
      type: "SwitchParameter" 
parameters: 
  - name: "GlobalList" 
    description: "Specifies the name of the global lsit to be renamed." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the global lsit to be renamed.This is an alias of the GlobalList parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "NewName" 
    description: "Specifies the new name of the item. Enter only a name - i.e., for items that support paths, do not enter a path and name." 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.String" 
    description: "Specifies the name of the global lsit to be renamed."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/GlobalList/Rename-TfsGlobalList"
aliases: 
examples: 
---
