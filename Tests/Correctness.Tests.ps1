. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\_TestSetup.ps1"

$allFunctions = Get-Command -Module TfsCmdlets | Where-Object CommandType -ne 'Alias' | Sort-Object { $_.Name }
#$allAliases = Get-Command -Module TfsCmdlets | Where-Object CommandType -eq 'Alias'

$destructiveVerbs = '^Dismount|^Remove|^Stop'
$stateChangingVerbs = '^Import|^Mount|^Move|^New|^Rename|^Set|^Start'
$passthruVerbs = '^Connect|^Copy|^Move|^New|^Rename'
$valueReturningVerbs = "^Get|$passthruVerbs"
$cmdletBindingRegexExpr = '\[CmdletBinding.+\]'
$cmdletBindingRegex = [regex] $cmdletBindingRegexExpr

$analyzerRules = Get-ScriptAnalyzerRule -Severity Warning

$allFunctions | Foreach-Object {

    Describe "$_" {

        $cmd = $_
        $cmdDefinitionPath = (Get-ChildItem (Join-Path $projectDir "$cmd.ps1") -Recurse).FullName

        Context 'Standard PSScriptAnalyzer Tests' {

            $result = Invoke-ScriptAnalyzer -Path $cmdDefinitionPath -IncludeRule $analyzerRules

            foreach($rule in $analyzerRules)
            {
                It "Should pass $rule" {
                    $failure = $result | Where-Object RuleName -eq $rule.RuleName
                    $locInfo = ($failure | Foreach-Object { "`n$($_.ScriptPath): $($_.Line)"}) -join ''
                    $locInfo | Should BeNullOrEmpty
                }
            }
        }

        Context 'Custom Correctness Tests' {

            $cmdletBindingDefinition = $cmdletBindingRegex.Match($cmd.Definition).Value

            It 'Functions should have the "Tfs/Azdo" standard prefix' {
                $cmd.Noun.Substring(0, 3) | Should Match 'Tfs|Azdo'
            }

            It 'Functions should have [CmdletBinding()] annotation' {
                $cmdletBindingDefinition | Should Match $cmdletBindingRegexExpr
            }

            if ($cmd.Verb -match $valueReturningVerbs)
            {
                It "Functions with verbs matching '$valueReturningVerbs' should have [OutputType] set" {
                    $cmd.OutputType.Count | Should BeGreaterThan 0
                }
            }

            if ($cmd.Verb -match $stateChangingVerbs)
            {
                It "Functions with verbs matching '$stateChangingVerbs' should have ConfirmImpact set to at least Medium" {
                    $cmdletBindingDefinition | Should Match 'ConfirmImpact=.*(Medium|High)'
                }
            }

            if ($cmd.Verb -match $destructiveVerbs)
            {
                It "Functions with verbs matching '$destructiveVerbs' should have ConfirmImpact set to High" {
                    $cmdletBindingDefinition | Should Match 'ConfirmImpact=[''"]High[''"]'
                }
            }

            if ($cmd.Verb -match $passthruVerbs)
            {
                It "Functions with verbs matching '$passthruVerbs' should have -Passthru argument" {
                    $cmd.Parameters.Keys.Contains('Passthru') | Should Be $true
                }
                It "Functions with verbs matching '$passthruVerbs' should check $Passthru in code" {
                    $cmd.Definition -match 'if\s? \(\$Passthru\)'
                }
            }

            $cmdDocs = Get-Help $cmd.Name

            It 'Pipeline parameters and input types should be properly set' {
                $pipelineBoundParam = ($cmd.Parameters.Values | Where-Object {$_.Attributes | Where-Object { $_.ValueFromPipeline -eq $true }})
                $inputDocs = $cmdDocs.inputTypes
                if ($pipelineBoundParam -or $hasInputDocs)
                {
                    $pipelineBoundParam | Should Not BeNullOrEmpty
                    $inputDocs | Should Not BeNullOrEmpty
                }
            }

            # It 'Functions should have minimal documentation (Synopsis, description, examples)' {
            #     $missingSections = @()
            #     if (-not $cmdDocs.Synopsis) { $missingSections += 'Synopsis' }
            #     if (-not $cmdDocs.Description) { $missingSections += 'Description' }
            #     if (-not $cmdDocs.Examples) { $missingSections += 'Examples' }
            #     ($missingSections -join ', ') | Should BeNullOrEmpty
            # }

            $parameterDocs = $cmdDocs.Parameters.parameter

            # It "Parameters should have a description" {
            #     $paramsWithoutDesc = ($parameterDocs | Where-Object Name -NotIn @('WhatIf', 'Confirm') |  Where-Object description -eq $null | Select-Object -ExpandProperty Name) -join ', '
            #     $paramsWithoutDesc | Should BeNullOrEmpty
            # }

            if (($pdoc.defaultvalue -match '\*') )
            {
                It " - Parameter $pName has a default value containing '*' and [SupportsWildcards()] attribute"
            }
        }
    }
}