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
                    $f.CmdletBinding | Should Be $true
                }
            }

        }

        Context "Remove-* functions" {

            $functions = Get-Command -Verb Remove -Module TfsCmdlets

            It "Has the SupportsShouldProcess attribute" {
                foreach($f in $functions)
                {
                    "Testing $f.Name"
                    $f.CmdletBinding | Should Be $true
                }
            }
        }
    }
}