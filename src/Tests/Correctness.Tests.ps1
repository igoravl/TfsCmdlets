. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\_TestSetup.ps1"

InModuleScope TfsCmdlets {

    Describe "Correctness Tests: Parameters" {

        Context "Parameters with wildcards" {

            It "Has SupportsWildcards attribute" {
            }

        }
    }

    Describe "Correctness Tests: Functions" {

        Context "All functions" {
            
            $functions = Get-Command -Module TfsCmdlets

            It "Have CmdletBinding attribute" {
                foreach($f in $functions)
                {
                    "Testing $f.Name"
                    $f.CmdletBinding | Should Be $false
                }
            }

        }

        Context 'Get-* functions' {

            $functions = Get-Command -Verb Get -Module TfsCmdlets | ? OutputType -ne $null

            It 'Have OutputType set' {
                $functions.Length | Should Be 0
            }
        }

        Context 'State-changing functions' {

            $functions = Get-Command -Verb Dismount, Import, Mount, Move, Rename, Set, Start -Module TfsCmdlets

            It 'Have ConfirmImpact set at least to Medium' {

            }
        }

        Context 'Destructive functions' {

            $functions = Get-Command -Verb Dismount, Remove, Stop -Module TfsCmdlets

            It 'Have ConfirmImpact attribute set to High' {

            }

            It "Have the SupportsShouldProcess attribute" {
                foreach($f in $functions)
                {
                    "Testing $f.Name"
                    $f.CmdletBinding | Should Be $true
                }
            }
        }
    }
}