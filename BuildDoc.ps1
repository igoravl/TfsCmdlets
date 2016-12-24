param(
    [Parameter()]
    [String]
    $SourceDir,

    [Parameter()]
    [String]
    $OutputDir
)


## Table Descriptors -----------------------------------------------------------
function HeaderCell {
    [CmdletBinding()]
    param(
        [Parameter(Position=0, Mandatory=$true)]
        [String]
        $Name,

        [Parameter(Mandatory=$false)]
        [Switch]
        $Centre,

        [Parameter(Mandatory=$false)]
        [Switch]
        $Right
    )

    process {
        # Decode alignment
        $align = "Left"
        if ($Centre) {
            $align = "Centre"
        } elseif ($Right) {
            $align = "Right"
        }

        # Create a new object to return
        $cell = New-Object System.Object
        $cell | Add-Member -Type NoteProperty -Name "Name" -Value $Name
        $cell | Add-Member -Type NoteProperty -Name "Alignment" -Value $align
        $cell
    }
}

function Header {
    [CmdletBinding()]
    param(
        [Parameter(Position=0, Mandatory=$true)]
        [ScriptBlock]
        $Cells
    )

    process {
        # Create a new object
        $row = New-Object System.Object
        $row | Add-Member -Type NoteProperty -Name "Type" -Value "Header"
        $row | Add-Member -Type NoteProperty -Name "Cells" -Value (&$Cells)
        $row
    }
}

function Cell {
    [CmdletBinding()]
    param(
        [Parameter(Position=0)]
        [String]
        $Content = ''
    )

    process {
        $content
    }
}

function Row {
    [CmdletBinding()]
    param(
        [Parameter(Position=0, Mandatory=$true)]
        [ScriptBlock]
        $Cells
    )

    process {
        # Create a new object
        $row = New-Object System.Object
        $row | Add-Member -Type NoteProperty -Name "Type" -Value "Row"
        $row | Add-Member -Type NoteProperty -Name "Cells" -Value (&$Cells)
        $row
    }
}

function Describe-Table {
    [CmdletBinding()]
    param(
        [Parameter(Position=0, Mandatory=$true)]
        [ScriptBlock]
        $Content
    )

    process {
        $table = New-Object System.Object
        $rows  = @()

        # Build the table
        &$Content | foreach {
            $row = $_
            switch ($row.Type) {
                "Header" {
                    $table | Add-Member -Type NoteProperty -Name "Columns" `
                        -Value $row.Cells
                }

                "Row" {
                    $rows += ,$row.Cells
                }
            }
        }

        # Add the rows
        $table | Add-Member -Type NoteProperty -Name "Rows" -Value $rows

        # Return the constructed table
        return $table
    }
}


## Table Builder Functions -----------------------------------------------------
function ColumnCharWidths($table) {
    # Calculate the maximum length of the cell in each row of the data
    $lengths = for ($i = 0; $i -lt $table.Columns.Length; $i++) {
        ($table.Rows | foreach { $_[$i].Length } |
            Measure-Object -Maximum).Maximum
    }

    # Factor in the headings
    for ($i = 0; $i -lt $table.Columns.Length; $i++) {
        [Math]::Max($lengths[$i], $table.Columns[$i].Name.Length)
    }
}

function MakeHeaderDelimeter($alignment) {
    # Make the actual bunch of dashes
    $delim = '---'

    # Replace the end points
    if ($alignment -eq 'Left' -or $alignment -eq 'Centre') {
        $delim = ':' + $delim
    }

    if ($alignment -eq 'Centre' -or $alignment -eq 'Right') {
        $delim += ':'
    }

    # Return the delimeter
    return $delim
}

function MakeRow($cells, $widths) {
    return "| $($cells -join ' | ') |"
}

function Format-MarkdownTable {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        $Table
    )

    process {
        $mdtable  = @()

        # Add the header row
        $mdtable += MakeRow ($table.Columns | foreach { $_.Name })

        # Add the delimeter row
        $delims = for ($i = 0; $i -lt $table.Columns.Length; $i++) {
            MakeHeaderDelimeter $table.Columns[$i].Alignment
        }
        $mdtable += MakeRow $delims $lengths

        # Add each content row
        foreach ($row in $table.Rows) {
            $mdtable += MakeRow $row $lengths
        }

        return $mdtable
    }
}


Function AutoLink
{
    [CmdletBinding()]
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [string]
        $doc, 
        
        [string]
        $cmdName
    )
    # Process links to MSDN class documentation

    $doc = $doc -replace '(?<ClassName>(Microsoft\.TeamFoundation(\.\w+)+)|(System(\.\w+)+))', '[${ClassName}](https://msdn.microsoft.com/en-us/library/${ClassName})'

    # Process links between TfsCmdlets functions

    $cmdList = Get-Command -Module TfsCmdlets | ? Name -ne $cmdName | Select -ExpandProperty Name

    foreach($cmd in $cmdList)
    {
        $doc = $doc -replace "\b(?<CmdletName>$cmd)\b", '[${CmdletName}](${CmdletName})'
    }

    return $doc
}

Function GenerateSyntax($help, $cmd)
{
    $syntaxes = ($help.syntax | Out-String).Trim().Replace('`r', '').Replace('`n', '').Split([Environment]::NewLine, [StringSplitOptions]::RemoveEmptyEntries)
    $i = 0
    $output = ''

    foreach ($syntax in $syntaxes)
    {
        if ($syntaxes.Length -gt 1)
        {
            if ($i -gt 0)
            {
                $output += [Environment]::NewLine +  [Environment]::NewLine 
            }
            $output += "# " + $($cmd.ParameterSets[$i++].Name + [Environment]::NewLine)
        }

        $output += $syntax 
    }

    return $output
}

Function GenerateParameters($cmd)
{
    $commonParameters = @('ErrorAction', 'WarningAction', 'InformationAction', 'Verbose', 'Debug', 'ErrorVariable', 'WarningVariable', 'InformationVariable', 'OutVariable', 'OutBuffer', 'PipelineVariable')

    $paramTable = Describe-Table {
        Header {
            HeaderCell "Parameter"
#            HeaderCell "Type" -Centre
            HeaderCell "Description"
        }

        foreach ($cmdParam in $cmd.Parameters.Values | ? Name -NotIn $commonParameters) {

            $paramName = ($cmdParam.Name | Out-String).Trim()

            $param = Get-Help $cmdName -Parameter $cmdParam.Name

            if ($param) 
            {
                $paramType = ($param.type.name | Out-String).Trim()
                $paramDesc = ($param.description | Out-String).Trim()
            }
            else
            {
                $paramType = ($cmdParam.Type | Out-String).Trim()
            }

            if ($paramType -eq 'SwitchParameter') {
                $paramType = 'Switch'
            }

            if ($paramDesc) {
                # Sanitise the description
                $paramDesc = ($paramDesc -split "`r?`n" |
                              foreach { $_.Trim() }) -join ' '
            }
            else
            {
                $paramDesc = '_N/A_'
            }

            Row {
                Cell $paramName
 #               Cell $paramType
                Cell $paramDesc
            }
        }
    }

    if ($paramTable.Rows.Length) {
        return (Format-MarkdownTable $paramTable) -join "`r`n"
    }
}

## Get-Help Parsers ------------------------------------------------------------

function ConvertCommandHelp($help, $cmdList) {

    $cmd = Get-Command $help.Name -Module TfsCmdlets
    $mod = Get-Module TfsCmdlets
    $cmdName = $help.Name
    $Description = if ($help.description) { ($help.description | Select -ExpandProperty Text) -join "`r`n`r`n" }
    $Notes = if ($help.alertSet) { ($help.alertSet.alert  | Select -ExpandProperty Text) -join "`r`n`r`n" }
    $InputTypes = if ($help.inputTypes) { '* ' + ($help.inputTypes.inputType.type.name -replace  "`n", "`r`n* ") }
    $OutputTypes = if ($cmd.OutputType) { '* ' + ($cmd.OutputType | Select -ExpandProperty Name) -join "`r`n* " }
    $Aliases = (Get-Alias | ? ResolvedCommandName -eq $cmdName | Select -ExpandProperty Name)

    if ($help.examples) {
        $Examples = ''
        for ($i = 0; $i -lt $help.examples.example.Length; ++$i) {
            $example = $help.examples.example[$i]

            $Examples += "`r`n"
            $Examples += "### Example $($i + 1)`r`n"
            $Examples += '```' + "`r`n"
            $Examples += ($example.code | Out-String).Trim() + "`r`n"
            $Examples += '```' + "`r`n"
            $Examples += "`r`n"
            $Examples += ($example.remarks | Out-String).Trim() + "`r`n"
        }
    }


    # Document template

    return @"
<a id=`"top`"></a>
## $cmdName
$($help.Synopsis)

$(if ($Aliases) { @"
### Aliases
The following abbreviations are aliases for this cmdlet:

$($Aliases | % {"* $_"} )
"@
})

<a id=`"Syntax`"></a>
### Syntax

``````PowerShell
$(GenerateSyntax $help $cmd)
``````

### Index

[Detailed Description](#Description) | [Parameters](#Parameters)$(if($InputTypes){' | [Inputs](#Inputs)'})$(if($OutputTypes){' | [Outputs](#Outputs)'})$(if($Notes) { ' | [Notes](#Notes)'})$(if ($Examples) { ' | [Examples](#Examples)'}) | [Related Topics](#RelatedTopics)

<a id=`"Description`"></a>
### Detailed Description 

$Description

[Go to top](#top)

<a id=`"Parameters`"></a>
### Parameters

$(GenerateParameters $cmd)

[Go to top](#top)

$(if ($InputTypes) { @"
<a id=`"Inputs`"></a>
### Inputs
The input type is the type of the objects that you can pipe to the cmdlet.

$InputTypes

[Go to top](#Top)

"@ })
$(if ($InputTypes) { @"
<a id=`"Outputs`"></a>
### Outputs
The output type is the type of the objects that the cmdlet emits.

$OutputTypes

[Go to top](#Top)

"@ })
$(if ($Notes) { @"
<a id="Notes"></a>
### Notes
$($help.alertSet.alert.Text)

[Go to top](#Top)

"@ })
$(if ($Examples) { @"
<a id="Examples"></a>
### Examples
$Examples

[Go to top](#Top)

"@ })
### Related Topics

[Go to top](#Top)

"@ | AutoLink -CmdName $cmdName



    # Add examples

    # Process automatic hyperlinks
    $doc = AutoLink $doc $cmdName $cmdList

    return $doc
}


## Actual documentation generator ----------------------------------------------

Get-Module TfsCmdlets | Remove-Module
Import-Module (Join-Path $SourceDir 'TfsCmdlets.psd1' -Resolve) -Force -Scope Local

$subModules = Get-ChildItem $SourceDir -Directory | Select -ExpandProperty Name
$docsDir = Join-Path $OutputDir 'doc'

# Magic callback that does the munging
$callback = {
    if ($args[0].Groups[0].Value.StartsWith('\')) {
        # Escaped tag; strip escape character and return
        $args[0].Groups[0].Value.Remove(0, 1)
    } else {
        # Look up the help and generate the Markdown
        ConvertCommandHelp (Get-Help $args[0].Groups[1].Value) $cmdList
    }
}
$re = [Regex]"\\?{%\s*(.*?)\s*%}"

$cmds = Get-Command -Module TfsCmdlets #| ? Name -eq 'Get-TfsTeamProject'
$cmdList = $cmds | Select -ExpandProperty Name
$cmdCount = $cmds.Count
$i = 0

$origBufSize = $Host.UI.RawUI.BufferSize
$expandedBufSize = New-Object Management.Automation.Host.Size (1000, 1000)

foreach($m in $subModules)
{
    $subModuleCommands = Get-ChildItem (Join-Path $SourceDir $m) -Filter '*-Tfs*.ps1' | Select -ExpandProperty BaseName
    $subModuleOutputDir = Join-Path $OutputDir "doc\$m"

    if (-not (Test-Path $subModuleOutputDir -PathType Container))
    {
        md $subModuleOutputDir | Out-Null
    }

    foreach($c in $subModuleCommands)
    {
        $i++ 

        $cmd = Get-Command $c -Module TfsCmdlets

        Write-Verbose "Generating help for $m/$($cmd.Name) ($i of $cmdCount)"

        # $Host.UI.RawUI.BufferSize = $expandedBufSize

        # Generate the readme
        $readme = "{% $($cmd.Name) %}" | foreach { $re.Replace($_, $callback) }

        # Output to the appropriate stream
        $OutputFile = Join-Path $subModuleOutputDir "$c.md" 
        $utf8Encoding = New-Object System.Text.UTF8Encoding($false)
        [System.IO.File]::WriteAllLines($OutputFile, $readme, $utf8Encoding)

        Write-Verbose "Writing $OutputFile"

        # $Host.UI.RawUI.BufferSize = $origBufSize
    }
}

Get-Module TfsCmdlets | Remove-Module
