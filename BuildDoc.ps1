[CmdletBinding()]
Param
(
    $RootProjectDir,
    $ModuleDir,
    $DocsDir,
    $RootUrl,
    $Layout = 'cmdlet'
)

if (-not $RootProjectDir) { $RootProjectDir = $PSScriptRoot }
if (-not $ModuleDir) { $ModuleDir = (Join-Path $RootProjectDir 'out/module') }
if (-not $DocsDir) { $DocsDir = (Join-Path $RootProjectDir 'out/docs') }
if (-not $RootUrl) { $RootUrl = 'https://tfscmdlets.dev/Cmdlets/' }

$RootUrl = $RootUrl.TrimEnd('/')

$CommonParameters = @('ErrorAction', 'WarningAction', 'InformationAction', 
    'Verbose', 'Debug', 'ErrorVariable', 'WarningVariable', 'InformationVariable', 
    'OutVariable', 'OutBuffer', 'PipelineVariable')

## Table Builder Functions -----------------------------------------------------

function Table ([Parameter(Position = 0)]$Content) {

    $table = [PSCustomObject] @{
        Columns = (& $Content | Where-Object Type -eq 'Header' | Select-Object -ExpandProperty Cells)
        Rows    = (& $Content | Where-Object Type -eq 'Row').Cells
    }

    return $table
}

function Header ([Parameter(Position = 0)][ScriptBlock]$Cells) {
    return Row $Cells 'Header'
}

function Row ([Parameter(Position = 0)][ScriptBlock]$Cells, [Parameter(Position = 1)]$Type = 'Row') {
    $row = [PSCustomObject] @{
        Type  = $Type
        Cells = (& $Cells)
    }

    return $row
}

function HeaderCell ([Parameter(Position = 0)]$Name, 
    [Parameter(Position = 1)][ValidateSet('Left', 'Center', 'Right')]$Align = 'Left',
    $Width = 50) {

    return [PSCustomObject] @{
        Name      = $Name
        Alignment = $Align
        Width     = $Width
    }
}

function Cell ([Parameter(Position = 0)]$Content, 
    [Parameter(Position = 1)][ValidateSet('Left', 'Center', 'Right')]$Align = 'Left',
    $Width = 50) {

    return [PSCustomObject] @{
        Content   = $Content
        Alignment = $Align
        Width     = $Width
    }
}

## Markdown conversion Functions -----------------------------------------------------

Function PadCenter([string]$InputObject, [int]$Width, [string]$PadWith) {
    $spaces = $Width - $InputObject.Length
    $padLeft = $spaces / 2 + $InputObject.Length

    return $InputObject.PadLeft($padLeft, $PadWith).PadRight($Width, $PadWith)
}

Function SplitAndPad([Parameter(ValueFromPipeline = $true)][string]$InputObject, [int]$Width = 30, [string]$Alignment = 'Left') {

    $tokens = $InputObject -split ' '
    $currentLine = ''

    foreach ($token in $tokens) {

        if ("${currentLine}${token}".Length -gt $Width) {
            switch ($Alignment) {
                'Left' { Write-Output $currentLine.TrimEnd().PadRight($Width, ' '); break }
                'Right' { Write-Output $currentLine.TrimEnd().PadLeft($Width, ' '); break }
                'Center' { Write-Output (PadCenter -Input $currentLine.TrimEnd() -Width $Width -PadWith ' '); break }
            }

            $currentLine = ''
            continue
        }
        
        $currentLine += "$token "
    }

    if ($currentLine.Length) {
        switch ($Alignment) {
            'Left' { Write-Output $currentLine.TrimEnd().PadRight($Width, ' ') }
            'Right' { Write-Output $currentLine.TrimEnd().PadLeft($Width, ' ') }
            'Center' { Write-Output (PadCenter -Input $currentLine.TrimEnd() -Width $Width -PadWith ' ') }
        }
    }
}

function MakeHeaderDelimeter($cell) {

    switch ($cell.Alignment) {
        'Left' {
            return ':-----'
        }
        'Right' {
            return '-----:'
        }
        'Center' {
            return ':----:'
        }
    }
}

function MdRow($cells) {
    $row = "|" + ($cells -join '|') + "|`r`n"
    return $row
}

Function AutoLink([Parameter(ValueFromPipeline = $true)]$doc, $cmdName) {

    Process {

        # Process links to MSDN class documentation

        $doc = $doc -replace '(?<ClassName>(Microsoft\.TeamFoundation(\.\w+)+)|(Microsoft\.VisualStudio(\.\w+)+)|(System(\.\w+)+))', '[${ClassName}](https://docs.microsoft.com/en-us/dotnet/api/${ClassName})'

        # Process links between TfsCmdlets functions

        $cmdList = Get-Command -Module TfsCmdlets | Where-Object Name -ne $cmdName | Select-Object -ExpandProperty Name

        foreach ($cmd in $cmdList) {
            $doc = $doc -replace "\b(?<CmdletName>$cmd)\b", "[$cmd]($(CommandUrl $cmd))"
        }

        return $doc
    }
}

Function CommandUrl($cmd) {
    $t = (Get-Command $cmd).ImplementingType
    $path = $t.FullName.SubString(19, $t.FullName.Length - $t.Name.Length - 20).Replace('.', '/')

    return "$RootUrl/$path/$cmd"
}

Function Syntax($help, $cmd)
{
    $showParameterSets = ($cmd.ParameterSets.Count -gt 1)

    Write-Output "`r`n"

    foreach($ps in $cmd.ParameterSets)
    {
        if($showParameterSets)
        {
            Write-Output "# $($ps.Name)`r`n"
        }

        Write-Output "$($cmd.Name)`r`n"

        foreach($p in $ps.Parameters | Where-Object Name -NotIn $CommonParameters)
        {
            $helpParam = ($help.parameters.parameter | Where-Object name -eq $p.Name)
            $paramOut = "-$($p.Name)"
            
            if($p.ParameterType.Name -ne 'SwitchParameter')
            {
                $paramOut += " <$($helpParam.parameterValue)>"
            }

            $required = ($helpParam.required -and [bool]::Parse($helpParam.required))

            if(-not $required)
            {
                $paramOut = "[$paramOut]"
            }

            Write-Output "    $paramOut`r`n"
        }
    }
}

Function Syntax_Old($help, $cmd) {
    $syntaxes = ($help.syntax | Out-String).Trim().Replace('`r', '').Replace('`n', '').Replace(' <SwitchParameter>', '').Split([Environment]::NewLine, [StringSplitOptions]::RemoveEmptyEntries)
    $i = 0
    $output = ''

    if($syntaxes | Where-Object { $_ -notlike "$($cmd.Name)*"})
    {
        $temp = @()
        for($j=0; $j -le $syntaxes.Count-2; $j += 2)
        {
            $temp += $syntaxes[$j] + $syntaxes[$j+1]
        }
        $syntaxes = $temp
    }

    foreach ($syntax in $syntaxes) {
        if ($syntaxes.Length -gt 1) {
            if ($i -gt 0) {
                $output += [Environment]::NewLine + [Environment]::NewLine 
            }

            $output += "# " + $($cmd.ParameterSets[$i++].Name + [Environment]::NewLine)
        }

        $tokens = $syntax.Substring(0, $syntax.Length-21) -split ' (?=\[?-)'
        $output += $tokens[0] + "`r`n"

        for($j = 1; $j -lt $tokens.Count; $j++)
        {
            $output += (' '*4) + $tokens[$j] + "`r`n"
        }

        $output += (' '*4) + "[<CommonParameter>]`r`n"

    }

    return $output
}

Function Parameters($cmd, $help, $top) {

    $params = ($cmd.Parameters.Values | Where-Object Name -NotIn $commonParameters)

    if (-not $params.Count) { return }

    Write-Output @"
### Parameters

| Parameter | Description |
|:----------|-------------|

"@

    foreach ($cmdParam in $params) {

        $paramName = ($cmdParam.Name | Out-String).Trim()

        switch ($paramName) {
            'WhatIf' {
                $paramDesc = 'Shows what would happen if the cmdlet runs. The cmdlet is not run.'
                break
            }
            'Confirm' {
                $paramDesc = 'Prompts you for confirmation before running the cmdlet.'
                break
            }
            default {
                $param = $help.Parameters.Parameter | Where-Object Name -eq $cmdParam.Name

                if ($param) {
                    $paramType = ($param.type.name | Out-String).Trim()
                    $paramDesc = ($param.description | Out-String).Trim()
                }
                else {
                    $paramType = ($cmdParam.Type | Out-String).Trim()
                }
            
                if ($paramType -eq 'SwitchParameter') {
                    $paramType = 'Switch'
                }
            
                if ($paramDesc) {
                    $paramDesc = ($paramDesc -split "`r?`n" | ForEach-Object { $_.Trim() }) -join ' '
                }
                else {
                    $paramDesc = '_N/A_'
                }
            }
        }

        Write-Output "| $paramName | $paramDesc |`r`n"
    }

    Write-Output "`r`n[Go to top]($top)`r`n"
}

## Get-Help Parsers ------------------------------------------------------------

function ConvertCommandHelp($help, $cmdList) {

    try {
        $cmd = Get-Command $help.Name -Module TfsCmdlets
    }
    catch {
        throw $_
    }

    $cmdName = $help.Name
    $cmd = (Get-Command $cmdName)
    $t = $cmd.ImplementingType
    $subModuleName = $t.FullName.SubString(19, $t.FullName.Length - $t.Name.Length - 20).Replace('.', '/')

    # Document template

    $top = "#$($cmdName.ToLower())"
    
    return @"
---
layout: $Layout
title: $cmdName
parent: $subModuleName
grand_parent: Cmdlets
---
## $cmdName
{: .no_toc}

$($help.Synopsis)

``````powershell
$(Syntax $help $cmd)
``````

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----
$(Description $cmd $help $top)$(Parameters $cmd $help $top)$(InputTypes $cmd $help $top)$(OutputTypes $cmd $help $top)$(Notes $cmd $help $top)$(Examples $cmd $help $top)$(Aliases $cmd $help $top)$(Related $cmd $help $top)

"@ | AutoLink -CmdName $cmdName

}

Function Description($cmd, $help, $top) {

    if (-not $help.description) { return }

    return @"

### Detailed Description 

$(($help.description | Select-Object -ExpandProperty Text) -join "`r`n`r`n")

[Go to top]($top)

"@
}

Function InputTypes($cmd, $help, $top) {

    if (-not $help.inputTypes) { return }

    return @"

### Inputs

The input type is the type of the objects that you can pipe to the cmdlet.

$($help.inputTypes.inputType.type.name -split "`n" | ForEach-Object {'* ' + $_})

[Go to top]($top)

"@
}

Function OutputTypes($cmd, $help, $top) {

    if (-not $cmd.OutputType) { return }

    return @"

### Outputs

The output type is the type of the objects that the cmdlet emits.

$($cmd.OutputType | Select-Object -ExpandProperty Name | ForEach-Object {"* $_`r`n"})
[Go to top]($top)

"@  
}

Function Notes($cmd, $help, $top) {

    if (-not $help.alertSet) { return }
    
    return @"

### Notes

$($help.alertSet.alert | Select-Object -ExpandProperty Text) -join "`r`n`r`n")

[Go to top]($top)

"@
}

Function Examples($cmd, $help, $top) {

    if (-not $help.examples) { return }

    $Examples = ''

    for ($i = 0; $i -lt $help.examples.example.Length; ++$i) {
        $example = $help.examples.example[$i]
        $Examples += "`r`n"
        $Examples += "#### Example $($i + 1)`r`n"
        $Examples += '```powershell' + "`r`n"
        $Examples += ($example.code | Out-String).Trim() + "`r`n"
        $Examples += '```' + "`r`n"
        $Examples += "`r`n"
        $Examples += ($example.remarks | Out-String).Trim() + "`r`n"
    }

    return @"

### Examples

$Examples

[Go to top]($top)

"@
}

Function Aliases  ($cmd, $help, $top) { 

    $Aliases = (Get-Alias | Where-Object ResolvedCommandName -eq $cmdName | Select-Object -ExpandProperty Name)

    if (-not $Aliases) { return }

    return @"

### Aliases

The following abbreviations are aliases for this cmdlet:

$Aliases
    
[Go to top]($top)

"@    
}

Function Related  ($cmd, $help, $top) { 

    return @"

### Related Topics

$($help.relatedLinks.navigationLink | Where-Object linkText -ne 'Online version:' | ForEach-Object { "* $($_.linkText)$(if ($_.uri) { " [$($_.uri)]($($_.uri))" })`r`n"})

[Go to top]($top)
"@ 
}

# Main script

Import-Module (Join-Path $ModuleDir 'TfsCmdlets.psd1') -Force

if (-not (Test-Path $DocsDir)) { New-Item $DocsDir -ItemType Directory | Out-Null }

$i = 0
$top = ''
$cmds = Get-Command -Module TfsCmdlets
$cmdList = $cmds | Select-Object -ExpandProperty Name
$cmdCount = $cmds.Count
$origBufSize = $Host.UI.RawUI.BufferSize
$expandedBufSize = New-Object Management.Automation.Host.Size (1000, 1000)
$moduleAssembly = [AppDomain]::CurrentDomain.GetAssemblies() | Where-Object FullName -like 'TfsCmdlets.PS*'
$subModules = @{ }

foreach ($t in $moduleAssembly.GetTypes() | 
    Where-Object { $_.GetCustomAttributes([System.Management.Automation.CmdletAttribute], $true) } | 
    Sort-Object { $_.FullName } ) {

    $subModuleNamespace = $t.FullName.SubString(0, $t.FullName.Length - $t.Name.Length - 1)
    $subModulePath = $t.FullName.SubString(19, $t.FullName.Length - $t.Name.Length - 20).Replace('.', '/')

    if (-not $subModules.ContainsKey($subModuleNamespace)) {
        $subModules[$subModuleNamespace] = [PSCustomObject] @{
            Namespace = $subModuleNamespace
            Path      = $subModulePath
            Commands  = @()
        }
    }

    $attr = $t.GetCustomAttributes([System.Management.Automation.CmdletAttribute], $true)[0]

    $subModules[$subModuleNamespace].Commands += [PSCustomObject]@{
        Name = "$($attr.VerbName)-$($attr.NounName)"
        Type = $t
    }
}

foreach ($m in $subModules.Values) {
    $subModuleOutputDir = (Join-Path $DocsDir $m.Path)

    if (-not (Test-Path $subModuleOutputDir -PathType Container)) {
        Write-Verbose "Creating sub-module folder '$subModuleOutputDir'"
        New-Item $subModuleOutputDir -ItemType Directory | Out-Null
    }

    $subModuleCommands = $m.Commands.Name

    foreach ($c in $subModuleCommands) {
        $i++ 

        $cmd = Get-Command $c -Module TfsCmdlets

        Write-Progress -Activity "Generating help files" -Status "$($m.Path)/$($cmd.Name) ($i of $cmdCount)" -PercentComplete ($i / $cmdCount * 100)

        try {
            #$Host.UI.RawUI.BufferSize = $expandedBufSize

            # Generate the readme
            $readme = ConvertCommandHelp -Help (Get-Help $cmd) -CmdList $cmdList

            # Output to the appropriate stream
            Write-Verbose "Writing $OutputFile"
            $OutputFile = Join-Path $subModuleOutputDir "$c.md" 
            $utf8Encoding = New-Object System.Text.UTF8Encoding($false)
            [System.IO.File]::WriteAllLines($OutputFile, $readme, $utf8Encoding)
        }
        catch {
            Write-Warning $_
        }
    }

    Write-Progress -Activity "Generating help files" -Completed
    $Host.UI.RawUI.BufferSize = $origBufSize
}
