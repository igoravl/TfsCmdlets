[CmdletBinding()]
Param
(
    $RootProjectDir,
    $CommonProjectDir,
    $ModuleDir,
    $DocsDir,
    $RootUrl,
    $Layout = 'cmdlet'
)

Function _GenerateHelp($cmd, $moduleName, $modulePath) {
    $cmdName = $cmd.details.name
    $description = $cmd.details.description.InnerText
    $remarks = $cmd.description.InnerText
    $parent = $moduleName -split '\.'

    @"
---
title: $cmdName
breadcrumbs: [ $( ($parent | ForEach-Object { """$_""" }) -join ', ' ) ]
parent: $(_Text $moduleName)
description: $(_Text $description)
remarks: $(_Text $remarks)
parameterSets: $(_GenerateParameterSets $cmd)
parameters: $(_GenerateParameters $cmd)
inputs: $(_GenerateInput $cmd)
outputs: $(_GenerateOutput $cmd)
notes: $(_GenerateNotes $cmd)
relatedLinks: $(_GenerateLinks $cmd)
aliases: $(_GenerateAliases $cmd)
examples: $(_GenerateExamples $cmd)
---
"@ | Out-File (Join-Path $modulePath "$cmdName.md") -Encoding utf8

}

Function _GenerateParameterSets($cmd) {
    Write-Output "`n  `"_All_`": [ $(($cmd.parameters.parameter | `
        Where-Object {($_.aliases -split',') -notcontains $_.name} | `
        Select-Object -ExpandProperty name | `
        Sort-Object) -join ', ') ]"
    foreach ($syntax in $cmd.syntax.syntaxItem) {
        Write-Output "`n  $(_Text $syntax.parameterSet): "
        foreach($param in $syntax.parameter) {
            Write-Output "`n    $($param.name):"
            Write-Output "`n      type: $(_Text $param.parameterValue.InnerText) "
            if($param.position -ne 'named') {Write-Output "`n      position: $(_Text $param.position) "}
            if($param.required -eq 'true') {Write-Output "`n      required: true "}
        }
    }
}

Function _GenerateParameters($cmd) {
    foreach ($p in $cmd.parameters.parameter) {
        Write-Output "`r`n  - name: $(_Text $p.name)"
        Write-Output "`r`n    description: $(_Text $p.description.InnerText)"
        if($p.required -eq "true") {Write-Output "`r`n    required: $($p.required)"}
        Write-Output "`r`n    globbing: $($p.globbing)"
        if($p.pipelineInput -ne "false") {Write-Output "`r`n    pipelineInput: $(_Text $p.pipelineInput)"}
        if($p.position -ne "named") {Write-Output "`r`n    position: $($p.position)"}
        Write-Output "`r`n    type: $(_Text $p.parameterValue.InnerText)"
        if($p.aliases) {Write-Output "`r`n    aliases: [ $($p.aliases) ]"}
        if($p.defaultValue) {Write-Output "`r`n    defaultValue: $(_Text $p.defaultValue)"}
    }
}

Function _GenerateInput($cmd) {
    foreach ($i in $cmd.inputTypes.inputType) {
        Write-Output "`n  - type: $(_Text $i.type.name)"
        Write-Output "`n    description: $(_Text $i.description.InnerText)"
    }
}

Function _GenerateOutput($cmd) {
    foreach ($o in $cmd.returnValues.returnValue) {
        Write-Output "`n  - type: $(_Text $o.type.name)"
        Write-Output "`n    description: $(_Text $o.description.InnerText)"
    }
}

Function _GenerateNotes($cmd) { }

Function _GenerateLinks($cmd) {
    foreach ($link in $cmd.relatedLinks.navigationLink) {
        Write-Output "`n  - text: $(_Text $link.linkText)"
        Write-Output "`n    uri: $(_Text $link.uri)"
    }
}

Function _GenerateAliases($cmd) {
    $aliases = (Get-Alias | Where-Object ResolvedCommandName -eq $cmd.details.name).name

    if(-not $aliases) {return}

    Write-Output "[ $($aliases -join ', ') ]"
}

Function _GenerateExamples($cmd) {
    foreach ($ex in $cmd.examples.example) {
        Write-Output "`n  - title: $(_Text $ex.title)"
        Write-Output "`n    code: $(_Text $ex.code)"
        Write-Output "`n    remarks: $(_Text $ex.remarks.InnerText)"
    }
}

Function _Text($text) {
    if (-not $text) { return }

    return """$($text.Replace('\', '\\').Replace('"', '\"'))"""
}

### Main script ###

if (-not $RootProjectDir) { $RootProjectDir = $PSScriptRoot }
if (-not $CommonProjectDir) { $CommonProjectDir = (Join-Path $RootProjectDir 'CSharp/TfsCmdlets.Common/Cmdlets' ) }
if (-not $ModuleDir) { $ModuleDir = (Join-Path $RootProjectDir 'out/module') }
if (-not $DocsDir) { $DocsDir = (Join-Path $RootProjectDir 'out/docs') }
if (-not $RootUrl) { $RootUrl = 'https://tfscmdlets.dev/Cmdlets/' }

$RootUrl = $RootUrl.TrimEnd('/')

$CommonParameters = @('ErrorAction', 'WarningAction', 'InformationAction', 
    'Verbose', 'Debug', 'ErrorVariable', 'WarningVariable', 'InformationVariable', 
    'OutVariable', 'OutBuffer', 'PipelineVariable')

$doc = [xml] (Get-Content (Join-Path $ModuleDir 'TfsCmdlets.PSDesktop.dll-Help.xml'))

foreach ($cmd in $doc.helpItems.command) {
    $moduleName = $cmd.Module

    #if($moduleName -notin "Admin", "Connection") {continue}

    $modulePath = (Join-Path $DocsDir ($moduleName -replace '\.', '/'))

    if (-not (Test-Path $modulePath)) {
        New-Item $modulePath -ItemType Directory | Out-Null
    }

     _GenerateHelp $cmd $moduleName $modulePath
}

Get-ChildItem $CommonProjectDir/index.md -Recurse -Verbose | ForEach-Object {Copy-Item $_ -Destination "$DocsDir$($_.FullName.Substring($CommonProjectDir.Length))" -Force}
