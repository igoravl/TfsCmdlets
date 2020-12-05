---
title: Invoke-TfsRestApi
breadcrumbs: [ "RestApi" ]
parent: "RestApi"
description: "Invoke an Azure DevOps REST API. "
remarks: "Invoke-TfsRestApi can automatically parse an example URL from https://docs.microsoft.com/en-us/rest/api/azure/devops/ and replace its various tokens (such as {organization}, {project} and {team}) as long as collection / project / team information are available via either the their respective arguments in this command or the corresponding Connect-Tfs* cmdlet. HTTP method and API version are also automatically extracted from the supplied example, when available. "
parameterSets: 
  "_All_": [ AdditionalHeaders, ApiVersion, AsTask, Body, Collection, Method, Path, Project, QueryParameters, Raw, RequestContentType, ResponseContentType, Team, UseHost ] 
  "__AllParameterSets":  
    Path: 
      type: "string"  
      position: "0"  
      required: true  
    AdditionalHeaders: 
      type: "Hashtable"  
    ApiVersion: 
      type: "string"  
    AsTask: 
      type: "SwitchParameter"  
    Body: 
      type: "string"  
    Collection: 
      type: "object"  
    Method: 
      type: "string"  
    Project: 
      type: "object"  
    QueryParameters: 
      type: "Hashtable"  
    Raw: 
      type: "SwitchParameter"  
    RequestContentType: 
      type: "string"  
    ResponseContentType: 
      type: "string"  
    Team: 
      type: "object"  
    UseHost: 
      type: "string" 
parameters: 
  - name: "Path" 
    description: "Specifies the path of the REST API to call. Tipically it is the portion of the URL after the name of the collection/organization, i.e. in the URL https://dev.azure.com/{organization}/_apis/projects?api-version=5.1 the path is \"/_apis/projects\". " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
  - name: "Method" 
    description: "Specifies the HTTP method to call the API endpoint. When omitted, defaults to \"GET\". " 
    globbing: false 
    type: "string" 
    defaultValue: "GET" 
  - name: "Body" 
    description: "Specifies the request body to send to the API endpoint. Tipically contains the JSON payload required by the API. " 
    globbing: false 
    type: "string" 
    aliases: [ Content ] 
  - name: "Content" 
    description: "Specifies the request body to send to the API endpoint. Tipically contains the JSON payload required by the API. This is an alias of the Body parameter." 
    globbing: false 
    type: "string" 
    aliases: [ Content ] 
  - name: "RequestContentType" 
    description: "Specifies the request body content type to send to the API. When omitted, defaults to \"application/json\". " 
    globbing: false 
    type: "string" 
    defaultValue: "application/json" 
  - name: "ResponseContentType" 
    description: "Specifies the response body content type returned by the API. When omitted, defaults to \"application/json\". " 
    globbing: false 
    type: "string" 
    defaultValue: "application/json" 
  - name: "AdditionalHeaders" 
    description: "Specifies a hashtable with additional HTTP headers to send to the API endpoint. " 
    globbing: false 
    type: "Hashtable" 
  - name: "QueryParameters" 
    description: "Specifies a hashtable with additional query parameters to send to the API endpoint. " 
    globbing: false 
    type: "Hashtable" 
  - name: "ApiVersion" 
    description: "Specifies the desired API version. When omitted, defaults to \"4.1\". " 
    globbing: false 
    type: "string" 
    defaultValue: "4.1" 
  - name: "UseHost" 
    description: "Specifies an alternate host name for APIs not hosted in \"dev.azure.com\", e.g. \"vsaex.dev.azure.com\" or \"vssps.dev.azure.com\". " 
    globbing: false 
    type: "string" 
  - name: "Raw" 
    description: "Returns the API response as an unparsed string. If omitted, JSON responses will be parsed, converted and returned as objects (via ConvertFrom-Json). " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "AsTask" 
    description: "Returns the System.Threading.Tasks.Task object used to issue the asynchronous call to the API. The caller is responsible for finishing the asynchronous call by e.g. accessing the Result property. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/RestApi/Invoke-TfsRestApi"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Invoke-TfsRestApi -Method GET -Path /_apis/projects -ApiVersion 4.1 -Collection DefaultCollection" 
    remarks: "Calls a REST API that lists all team projects in a TFS collection named DefaultCollection" 
  - title: "----------  EXAMPLE 2  ----------" 
    code: "PS> Invoke-TfsRestApi 'GET https://extmgmt.dev.azure.com/{organization}/_apis/extensionmanagement/installedextensions?api-version=5.1-preview.1'" 
    remarks: "Calls the API described by an example extracted from the docs.microsoft.com web site. HTTP method, host name and API version are all set based on the supplied values; Tokens {organization}, {project} and {team} are properly replaced with the corresponding values provided by the current connection context (via previous calls to Connect-TfsTeamProjectCollection, Connect-TfsTeamProject and/or Connect-TfsTeam)." 
  - title: "----------  EXAMPLE 3  ----------" 
    code: "PS> Invoke-TfsRestApi 'GET https://{instance}/{collection}/_apis/process/processes?api-version=4.1' -Collection http://vsalm:8080/tfs/DefaultCollection" 
    remarks: "Calls an API in a TFS instance, parsing the example provided by the docs.microsoft.com web site."
---
