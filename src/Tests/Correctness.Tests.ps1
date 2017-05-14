. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\_TestSetup.ps1"

$allFunctions = Get-Command -Module TfsCmdlets

$topLevelFunctions = $allFunctions | Where-Object Name -Like '*-Tfs*'
$destructiveVerbs = 'Dismount|Remove|Stop'
$stateChangingVerbs = 'Import|Mount|Move|New|Rename|Set|Start'
$passthruVerbs = '^Connect|Copy|^Move|New|Rename'
$valueReturningVerbs = "Get|$passthruVerbs"
$cmdletBindingRegexExpr = '\[CmdletBinding.+\]'
$cmdletBindingRegex = [regex] $cmdletBindingRegexExpr


    Describe 'Correctness Tests' {

        $allFunctions | % {

            Context "$_" {

                $cmd = $_
                $cmdletBindingDefinition = $cmdletBindingRegex.Match($cmd.Definition).Value

                It 'Has [CmdletBinding()] annotation' {
                    $cmdletBindingDefinition | Should Match $cmdletBindingRegexExpr
                }

                if ($cmd.Verb -match $valueReturningVerbs)
                {
                    It 'Has [OutputType] set' {
                        $cmd.OutputType.Count | Should BeGreaterThan 0
                    }
                }

                if ($cmd.Verb -match $stateChangingVerbs)
                {
                    It 'Has ConfirmImpact set to at least Medium' {
                        $cmdletBindingDefinition | Should Match 'ConfirmImpact=.*(Medium|High)'
                    }
                }

                if ($cmd.Verb -match $destructiveVerbs)
                {
                    It 'Has ConfirmImpact set to High' {
                        $cmdletBindingDefinition | Should Match 'ConfirmImpact=[''"]High[''"]'
                    }
                }

                if ($cmd.Verb -match $passthruVerbs)
                {
                    It 'Has -Passthru argument' {
                        $cmd.Parameters.Keys.Contains('Passthru') | Should Be $true
                    }
                    It 'Checks $Passthru in code' {
                        $cmd.Definition -match 'if\s? \(\$Passthru\)'
                    }
                }
            }
        }
    }