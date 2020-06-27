---
title: Set-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Changes the contents of a Global List."
remarks: 
parameterSets: 
  "_All_": [ Add, Force, GlobalList, Remove ] 
  "Edit list items":  
    GlobalList: 
      type: "string"  
      required: true  
    Add: 
      type: "IEnumerable`1"  
    Force: 
      type: "SwitchParameter"  
    Remove: 
      type: "IEnumerable`1" 
parameters: 
  - name: "GlobalList" 
    description: "Specifies the name of the global list to be changed." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the global list to be changed.This is an alias of the GlobalList parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    type: "string" 
    aliases: [ Name ] 
  - name: "Add" 
    description: "Specifies a list of items to be added to the global list." 
    globbing: false 
    type: "IEnumerable`1" 
  - name: "Remove" 
    description: "Specifies a list of items to be removed from the global list." 
    globbing: false 
    type: "IEnumerable`1" 
  - name: "Force" 
    description: "Creates a new list if the specified one does not exist." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.String" 
    description: "Specifies the name of the global list to be changed."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/GlobalList/Set-TfsGlobalList"
aliases: 
examples: 
---
